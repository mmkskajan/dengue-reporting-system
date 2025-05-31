using CIDRS.Core.Modules.Applications.Models.Request;
using CIDRS.Core.Modules.ChiefOccupants.Services;
using CIDRS.Core.Modules.WorkItems.Services;
using CIDRS.Domain.Models.Entity.Applications;
using CIDRS.Identity.Services.User;
using CIDRS.Infrastructure;
using CIDRS.Shared.Common.Api.Models;
using CIDRS.Shared.Common.Extensions;
using CIDRS.Shared.Utility.FileManipulator.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIDRS.Core.Modules.Applications.Services
{
    public class ReportingApplicationService : IReportingApplicationService
    {
        private readonly ApplicationDataContext _dataContext;
        private readonly IIdentityService _identityService;
        private readonly IChiefOccupantService _chiefOccupantService;
        private readonly IFileWriter _fileWriter;
        private readonly IWorkItemService _workItemService;
        private const string parentFolderName = "Images";
        public ReportingApplicationService(ApplicationDataContext dataContext, IIdentityService identityService, IChiefOccupantService chiefOccupantService, IFileWriter fileWriter, IWorkItemService workItemService)
        {
            _dataContext = dataContext;
            _identityService = identityService;
            _chiefOccupantService = chiefOccupantService;
            _fileWriter = fileWriter;
            _workItemService = workItemService;
        }

        public async Task<ResponseResult<ReportingApplication>> StartReportingApplication(int? chiefOccupantId = null, bool publicSurroundingComplainet = false)
        {

            if (chiefOccupantId != null)
            {
                var currentUser = await _identityService.GetCurrentUserAsync();
                var chiefOccupantResponse = await _chiefOccupantService.GetChiefOccupantById(chiefOccupantId.Value);
                if (!chiefOccupantResponse.Succeeded)
                    return GetErrorResult(chiefOccupantResponse.Errors.ToArray());

                var chiefOccupant = chiefOccupantResponse.Result;

                if (currentUser.UserType != CIDRS.Identity.Domain.Enums.ApplicationUserType.Phi)
                    return GetErrorResult("Current User is not authorized to start base application!");

                if (chiefOccupant?.RespectivePhi?.IdentityUserId != currentUser.Id)
                    return GetErrorResult("Current User is not allocated to this Chief Ocuupant as respective PHI!");

                if (chiefOccupant.ReportingApplications.Any(x => x.Type == Domain.Enums.ApplicationType.Base))
                {
                    var baseapplication = chiefOccupant.ReportingApplications.FirstOrDefault(x => x.Type == Domain.Enums.ApplicationType.Base);
                    return new ResponseResult<ReportingApplication>()
                    {
                        Errors = null,
                        Result = baseapplication,
                        Succeeded = true
                    };
                }                     

                var application = new ReportingApplication()
                {
                    ChiefOccupantId = chiefOccupant.Id,
                    ChiefOccupant = chiefOccupant,
                    Status = Domain.Enums.ApplicationStatus.Pending,
                    Type = Domain.Enums.ApplicationType.Base
                };
                application.SetCreatedTime();
                application.EnsureId(GetApplicationId());

                await _dataContext.ReportingApplications.AddAsync(application);
                await _dataContext.SaveChangesAsync();

                return new ResponseResult<ReportingApplication>()
                {
                    Errors = null,
                    Result = application,
                    Succeeded = true
                };

            }
            else
            {
                if (!publicSurroundingComplainet)
                {
                    var currentUser = await _identityService.GetCurrentUserAsync();
                    var chiefOccupant = await _chiefOccupantService.GetChiefOccupantByIdentityId(currentUser.Id);

                    if (currentUser.UserType != CIDRS.Identity.Domain.Enums.ApplicationUserType.ChiefOccupant)
                        return GetErrorResult("Current User is not authorized to start application!");

                    if (chiefOccupant.IdentityUserId != currentUser.Id)
                        return GetErrorResult("You are not allow to start application for others!");

                    //if (chiefOccupant.ReportingApplications.Any(x => x.Status == Domain.Enums.ApplicationStatus.Pending && x.Type != Domain.Enums.ApplicationType.PublicSurroundingComplaints))
                    //    return GetErrorResult("You have Pending application! Complete that first!");

                    if (!(chiefOccupant.ReportingApplications.Any(x => x.Type == Domain.Enums.ApplicationType.Base && x.Status == Domain.Enums.ApplicationStatus.Completed)))
                        return GetErrorResult("You don't have valid Base Application!");

                    var oldApplication = chiefOccupant.ReportingApplications.Where(x => x.Type == Domain.Enums.ApplicationType.HomeSurroundingAllerts && x.Status == Domain.Enums.ApplicationStatus.Pending).FirstOrDefault();
                    if(oldApplication != null)
                    return new ResponseResult<ReportingApplication>()
                    {
                        Errors = null,
                        Result = oldApplication,
                        Succeeded = true
                    };

                    var homeSurroundingApplications = chiefOccupant.ReportingApplications.Where(x => x.Type == Domain.Enums.ApplicationType.HomeSurroundingAllerts && x.Status == Domain.Enums.ApplicationStatus.Completed).ToList();
                    if (homeSurroundingApplications.Any())
                    {
                        homeSurroundingApplications.ForEach(x => x.SetArchivedTime());
                        homeSurroundingApplications.ForEach(x => _dataContext.ReportingApplications.Update(x));
                        await _dataContext.SaveChangesAsync();
                    }

                    var application = new ReportingApplication()
                    {
                        ChiefOccupantId = chiefOccupant.Id,
                        ChiefOccupant = chiefOccupant,
                        Status = Domain.Enums.ApplicationStatus.Pending,
                        Type = Domain.Enums.ApplicationType.HomeSurroundingAllerts
                    };
                    application.SetCreatedTime();
                    application.EnsureId(GetApplicationId());

                    await _dataContext.ReportingApplications.AddAsync(application);
                    await _dataContext.SaveChangesAsync();

                    return new ResponseResult<ReportingApplication>()
                    {
                        Errors = null,
                        Result = application,
                        Succeeded = true
                    };
                }
                else
                {
                    var currentUser = await _identityService.GetCurrentUserAsync();
                    var chiefOccupant = await _chiefOccupantService.GetChiefOccupantByIdentityId(currentUser.Id);

                    if (currentUser.UserType != CIDRS.Identity.Domain.Enums.ApplicationUserType.ChiefOccupant)
                        return GetErrorResult("Current User is not authorized to start application!");

                    if (chiefOccupant.IdentityUserId != currentUser.Id)
                        return GetErrorResult("You are not allow to start application for others!");

                    if (chiefOccupant.ReportingApplications.Any(x => x.Status == Domain.Enums.ApplicationStatus.Pending && x.Type == Domain.Enums.ApplicationType.PublicSurroundingComplaints))
                        return GetErrorResult("You have Pending application! Complete that first!");
                                       
                    var application = new ReportingApplication()
                    {
                        ChiefOccupantId = chiefOccupant.Id,
                        ChiefOccupant = chiefOccupant,
                        Status = Domain.Enums.ApplicationStatus.Pending,
                        Type = Domain.Enums.ApplicationType.PublicSurroundingComplaints
                    };
                    application.SetCreatedTime();
                    application.EnsureId(GetApplicationId());

                    await _dataContext.ReportingApplications.AddAsync(application);
                    await _dataContext.SaveChangesAsync();

                    return new ResponseResult<ReportingApplication>()
                    {
                        Errors = null,
                        Result = application,
                        Succeeded = true
                    };
                }
            }

        }

        public async Task<ResponseResult<ReportingApplication>> GetReportingApplication(int id)
        {
            var application = await _dataContext.ReportingApplications.Include(x => x.ChiefOccupant)
                                                                .Include(x => x.SurroundingSets)
                                                                    .ThenInclude(x => x.RelativeSurroundingSet)
                                                                .FirstOrDefaultAsync( x => x.Id == id);
            if (application == null)
                return GetErrorResult("No Reporting Application for given Id!!");

            return new ResponseResult<ReportingApplication>()
            {
                Errors = null,
                Result = application,
                Succeeded = true
            };
        }

        public async Task<ResponseResult<ReportingApplication>> AddSurroundingSet(BaseSurroundingSetRequest baseSurroundingSet, int chiefOccupantId)
        {
            var currentUser = await _identityService.GetCurrentUserAsync();
            var chiefOccupantResult = await _chiefOccupantService.GetChiefOccupantById(chiefOccupantId);

            if (!chiefOccupantResult.Succeeded)
                return GetErrorResult("No Valid Chief Occupant!!");
            var chiefOccupant = chiefOccupantResult.Result;

            if (currentUser.UserType != CIDRS.Identity.Domain.Enums.ApplicationUserType.Phi)
                return GetErrorResult("Current User is not authorized to add surrounding sets!");

            if (chiefOccupant.RespectivePhi.IdentityUserId != currentUser.Id)
                return GetErrorResult("You are not allow to add surrounding sets!");

            if (!chiefOccupant.ReportingApplications.Any(x => x.Status == Domain.Enums.ApplicationStatus.Pending))
                return GetErrorResult("No valid application to add surrounding sets");

            if (!chiefOccupant.ReportingApplications.Any(x => x.Type == Domain.Enums.ApplicationType.Base && x.Status == Domain.Enums.ApplicationStatus.Pending))
                return GetErrorResult("You don't have valid Base Application!");

            if (baseSurroundingSet.Image == null)
                return GetErrorResult("No Image Found!!");

            var baseApplication = await _dataContext.ReportingApplications.Include(x => x.ChiefOccupant)
                                                                            .ThenInclude(x => x.RespectivePhi)
                                                                          .Include(x => x.ChiefOccupant)
                                                                            .ThenInclude(x => x.RespectivePolice)
                                                                          .Include(x => x.SurroundingSets)
                                                                            .ThenInclude(x => x.RelativeSurroundingSet).FirstOrDefaultAsync(x => x.ChiefOccupantId == chiefOccupantId && x.Type == Domain.Enums.ApplicationType.Base);

            var baseSurroundingSets = baseApplication.SurroundingSets.Select(x => x.Name).ToList();
            baseSurroundingSets.ForEach(x => x.ToLower());

            if (baseSurroundingSets.Contains(baseSurroundingSet.Name))
            {
                var existingSurroundingSet = baseApplication.SurroundingSets.FirstOrDefault(x => x.Name.ToLower() == baseSurroundingSet.Name.ToLower());
                var deleteResult = _fileWriter.DeleteFile(existingSurroundingSet.ImageUrl);

                var result = await SetBaseSurroundingSet(chiefOccupantId, baseSurroundingSet.Name, baseSurroundingSet.Image, existingSurroundingSet);

                if(result)
                {
                    existingSurroundingSet.SetUpdatedTime();
                    _dataContext.SurroundingSets.Update(existingSurroundingSet);
                    await _dataContext.SaveChangesAsync();

                    return new ResponseResult<ReportingApplication>()
                    {
                        Errors = null,
                        Result = baseApplication,
                        Succeeded = true
                    };
                }

                return GetErrorResult("Add Surrounding Set is faild!");

            }
            else
            {
                var newSurroundingSet = new SurroundingSet()
                {
                    Description = baseSurroundingSet.Description,
                    Latitude = baseSurroundingSet.Latitude,
                    Longitude = baseSurroundingSet.Longitude,
                    Name = baseSurroundingSet.Name,
                    Application = baseApplication
                    
                };


                var result = await SetBaseSurroundingSet(chiefOccupantId, baseSurroundingSet.Name, baseSurroundingSet.Image, newSurroundingSet);

                if (result)
                {
                    newSurroundingSet.SetCreatedTime();
                    baseApplication.SurroundingSets.Add(newSurroundingSet);
                    _dataContext.ReportingApplications.Update(baseApplication);
                    await _dataContext.SaveChangesAsync();

                    return new ResponseResult<ReportingApplication>()
                    {
                        Errors = null,
                        Result = baseApplication,
                        Succeeded = true
                    };
                }

                return GetErrorResult("Add Surrounding Set is faild!");
            }            
        }

        public async Task<ResponseResult<ReportingApplication>> AddSurroundingSet(BaseSurroundingSetRequest baseSurroundingSet)
        {
            var currentUser = await _identityService.GetCurrentUserAsync();
            var chiefOccupantResult = await _chiefOccupantService.GetChiefOccupantByIdentityId(currentUser.Id);

            if (chiefOccupantResult == null)
                return GetErrorResult("No Valid Chief Occupant!!");
            var chiefOccupant = chiefOccupantResult;

            if (currentUser.UserType != CIDRS.Identity.Domain.Enums.ApplicationUserType.ChiefOccupant)
                return GetErrorResult("Current User is not authorized to add surrounding sets!");

            if (chiefOccupant.IdentityUserId != currentUser.Id)
                return GetErrorResult("You are not allow to add surrounding sets!");

            if (!chiefOccupant.ReportingApplications.Any(x => x.Status == Domain.Enums.ApplicationStatus.Pending))
                return GetErrorResult("No valid application to add surrounding sets");

            if (!chiefOccupant.ReportingApplications.Any(x => x.Type == Domain.Enums.ApplicationType.PublicSurroundingComplaints && x.Status == Domain.Enums.ApplicationStatus.Pending))
                return GetErrorResult("You don't have valid Public surrounding Application!");

            if (baseSurroundingSet.Image == null)
                return GetErrorResult("No Image Found!!");

            var publicApplication = await _dataContext.ReportingApplications.Include(x => x.ChiefOccupant)
                                                                            .ThenInclude(x => x.RespectivePhi)
                                                                          .Include(x => x.ChiefOccupant)
                                                                            .ThenInclude(x => x.RespectivePolice)
                                                                          .Include(x => x.SurroundingSets)
                                                                            .ThenInclude(x => x.RelativeSurroundingSet).FirstOrDefaultAsync(x => x.ChiefOccupantId == chiefOccupant.Id && x.Type == Domain.Enums.ApplicationType.PublicSurroundingComplaints && x.Status == Domain.Enums.ApplicationStatus.Pending);

            var baseSurroundingSets = publicApplication.SurroundingSets.Select(x => x.Name).ToList();
            baseSurroundingSets.ForEach(x => x.ToLower());

            if (baseSurroundingSets.Contains(baseSurroundingSet.Name))
            {
                var existingSurroundingSet = publicApplication.SurroundingSets.FirstOrDefault(x => x.Name.ToLower() == baseSurroundingSet.Name.ToLower());
                var deleteResult = _fileWriter.DeleteFile(existingSurroundingSet.ImageUrl);

                var result = await SetBaseSurroundingSet(chiefOccupant.Id, baseSurroundingSet.Name, baseSurroundingSet.Image, existingSurroundingSet);

                if (result)
                {
                    existingSurroundingSet.SetUpdatedTime();
                    _dataContext.SurroundingSets.Update(existingSurroundingSet);
                    await _dataContext.SaveChangesAsync();

                    return new ResponseResult<ReportingApplication>()
                    {
                        Errors = null,
                        Result = publicApplication,
                        Succeeded = true
                    };
                }

                return GetErrorResult("Add Surrounding Set is faild!");

            }
            else
            {
                var newSurroundingSet = new SurroundingSet()
                {
                    Description = baseSurroundingSet.Description,
                    Latitude = baseSurroundingSet.Latitude,
                    Longitude = baseSurroundingSet.Longitude,
                    Name = baseSurroundingSet.Name,
                    Application = publicApplication

                };


                var result = await SetBaseSurroundingSet(chiefOccupant.Id, baseSurroundingSet.Name, baseSurroundingSet.Image, newSurroundingSet);

                if (result)
                {
                    newSurroundingSet.SetCreatedTime();
                    publicApplication.SurroundingSets.Add(newSurroundingSet);
                    _dataContext.ReportingApplications.Update(publicApplication);
                    await _dataContext.SaveChangesAsync();

                    return new ResponseResult<ReportingApplication>()
                    {
                        Errors = null,
                        Result = publicApplication,
                        Succeeded = true
                    };
                }

                return GetErrorResult("Add Surrounding Set is faild!");
            }
        }

        public async Task<List<SurroundingSet>> GetPendingBaseSurroundingSets(int? chiefOccupantId)
        {
            if(chiefOccupantId == null)
            {
                var currentUser = await _identityService.GetCurrentUserAsync();

                if (currentUser.UserType != CIDRS.Identity.Domain.Enums.ApplicationUserType.ChiefOccupant)
                    return new List<SurroundingSet>();

                chiefOccupantId = (await _chiefOccupantService.GetChiefOccupantByIdentityId(currentUser.Id)).Id;
            }
            


            var chiefOccupant = await _chiefOccupantService.GetChiefOccupantById(chiefOccupantId.Value);
            if (!chiefOccupant.Succeeded || chiefOccupant.Result == null)
                return new List<SurroundingSet>();
            

            var baseApplication = await _dataContext.ReportingApplications                                                                      
                                                                          .Include(x => x.SurroundingSets)
                                                                            .ThenInclude(x => x.RelativeSurroundingSet).FirstOrDefaultAsync(x => x.ChiefOccupantId == chiefOccupantId && x.Type == Domain.Enums.ApplicationType.Base);

            var application = chiefOccupant.Result.ReportingApplications.FirstOrDefault(x => x.Type == Domain.Enums.ApplicationType.HomeSurroundingAllerts && x.Status == Domain.Enums.ApplicationStatus.Pending);

            if (application == null)
                return new List<SurroundingSet>(); ;

            var relativeSurroundingSetIds = application.SurroundingSets.Select(x => x.RelativeSurroundingSet.Id).ToList();

            var basedSurroundingSets = baseApplication.SurroundingSets.ToList();

            var notCompletedSurroundingSets = basedSurroundingSets.Where(x => !relativeSurroundingSetIds.Contains(x.Id)).ToList();
            
            return notCompletedSurroundingSets;
        }

        private async Task<bool> SetBaseSurroundingSet(int chiefOccupantId, string name, string image, SurroundingSet surroundingSet)
        {
            var fileName = string.Format("{0}_{1}_{2}", "Base", chiefOccupantId, name);
            var folderName = Path.Combine("SurroundingSets", "Base");
            var uploadedFileRelativeUrl = await UploadImageAsync(fileName, folderName, image);

            if (uploadedFileRelativeUrl.Contains(fileName))
                surroundingSet.ImageUrl = uploadedFileRelativeUrl;

            return uploadedFileRelativeUrl.Contains(fileName);

        }

        private async Task<bool> SetSurroundingSet(int chiefOccupantId, string name, string image, SurroundingSet surroundingSet)
        {
            var fileName = string.Format("{0}_{1}", chiefOccupantId, name);
            var folderName = "SurroundingSets";
            var uploadedFileRelativeUrl = await UploadImageAsync(fileName, folderName, image);

            if (uploadedFileRelativeUrl.Contains(fileName))
                surroundingSet.ImageUrl = uploadedFileRelativeUrl;

            return uploadedFileRelativeUrl.Contains(fileName);

        }

        private async Task<string> UploadImageAsync(string fileName,string folderName,string file)
        {           
            var result = await _fileWriter.UploadBase64ImageAsync(fileName, folderName, file);
            string path = Path.Combine(parentFolderName, folderName,result);
            return path;
        }

        public async Task<ResponseResult<ReportingApplication>> AddSurroundingSet(SurroundingSetRequest surroundingSet)
        {
            var currentUser = await _identityService.GetCurrentUserAsync();
            var chiefOccupant = await _chiefOccupantService.GetChiefOccupantByIdentityId(currentUser.Id);

            if (currentUser.UserType != CIDRS.Identity.Domain.Enums.ApplicationUserType.ChiefOccupant)
                return GetErrorResult("Current User is not authorized to add surrounding sets!");

            if (chiefOccupant.IdentityUserId != currentUser.Id)
                return GetErrorResult("You are not allow to add surrounding sets!");

            if (!chiefOccupant.ReportingApplications.Any(x => x.Status == Domain.Enums.ApplicationStatus.Pending && x.Type == Domain.Enums.ApplicationType.HomeSurroundingAllerts))
                return GetErrorResult("No valid application to add surrounding sets");

            if (!chiefOccupant.ReportingApplications.Any(x => x.Type == Domain.Enums.ApplicationType.Base && x.Status == Domain.Enums.ApplicationStatus.Completed))
                return GetErrorResult("You don't have valid  Application!");

            if (surroundingSet.Image == null)
                return GetErrorResult("No Image Found!!");

            var baseApplication = await _dataContext.ReportingApplications.Include(x => x.ChiefOccupant)
                                                                          .Include(x => x.SurroundingSets)
                                                                            .ThenInclude(x => x.RelativeSurroundingSet).FirstOrDefaultAsync(x => x.ChiefOccupantId == chiefOccupant.Id && x.Type == Domain.Enums.ApplicationType.Base);

            var baseSurroundingSet = await _dataContext.SurroundingSets.FirstOrDefaultAsync(x => x.Id == surroundingSet.RelativeId && x.ReportingApplicationId == baseApplication.Id);

            if(baseSurroundingSet == null)
                return GetErrorResult("No Valid relative surrounding set Found!!");

            var application = await _dataContext.ReportingApplications.Include(x => x.ChiefOccupant)
                                                                            .ThenInclude(x => x.RespectivePhi)
                                                                          .Include(x => x.ChiefOccupant)
                                                                            .ThenInclude(x => x.RespectivePolice)
                                                                          .Include(x => x.SurroundingSets)
                                                                            .ThenInclude(x => x.RelativeSurroundingSet).FirstOrDefaultAsync(x => x.ChiefOccupantId == chiefOccupant.Id && x.Type == Domain.Enums.ApplicationType.HomeSurroundingAllerts && x.ArchivedAt == null);

            var surroundingSets = application.SurroundingSets;

            

            if (surroundingSets.Any(x => x.RelativeId == baseSurroundingSet.Id))
            {
                var existingSurroundingSet = application.SurroundingSets.FirstOrDefault(x => x.RelativeId == baseSurroundingSet.Id);
                var deleteResult = _fileWriter.DeleteFile(existingSurroundingSet.ImageUrl);

                var result = await SetBaseSurroundingSet(chiefOccupant.Id, baseSurroundingSet.Name, surroundingSet.Image, existingSurroundingSet);

                if (result)
                {
                    existingSurroundingSet.SetUpdatedTime();
                    _dataContext.SurroundingSets.Update(existingSurroundingSet);
                    await _dataContext.SaveChangesAsync();

                    return new ResponseResult<ReportingApplication>()
                    {
                        Errors = null,
                        Result = baseApplication,
                        Succeeded = true
                    };
                }

                return GetErrorResult("Add Surrounding Set is faild!");

            }
            else
            {
                var newSurroundingSet = new SurroundingSet()
                {
                    Description = surroundingSet.Description,
                    Latitude = surroundingSet.Latitude,
                    Longitude = surroundingSet.Longitude,
                    Name = baseSurroundingSet.Name,
                    Application = baseApplication,
                    RelativeSurroundingSet= baseSurroundingSet

                };


                var result = await SetSurroundingSet(chiefOccupant.Id, baseSurroundingSet.Name, surroundingSet.Image, newSurroundingSet);

                if (result)
                {
                    newSurroundingSet.SetCreatedTime();
                    application.SurroundingSets.Add(newSurroundingSet);
                    _dataContext.ReportingApplications.Update(application);
                    await _dataContext.SaveChangesAsync();

                    return new ResponseResult<ReportingApplication>()
                    {
                        Errors = null,
                        Result = application,
                        Succeeded = true
                    };
                }

                return GetErrorResult("Add Surrounding Set is faild!");
            }
        }

        public async Task<ResponseResult<ReportingApplication>> CompleteAsync(int? chiefOccupantId = null, bool isPublicSurroundingApplication = false)
        {
            if (chiefOccupantId != null)
            {
                var currentUser = await _identityService.GetCurrentUserAsync();
                var chiefOccupantResponse = await _chiefOccupantService.GetChiefOccupantById(chiefOccupantId.Value);
                if (!chiefOccupantResponse.Succeeded)
                    return GetErrorResult(chiefOccupantResponse.Errors.ToArray());

                var chiefOccupant = chiefOccupantResponse.Result;

                if (currentUser.UserType != CIDRS.Identity.Domain.Enums.ApplicationUserType.Phi)
                    return GetErrorResult("Current User is not authorized to Complete base application!");

                if (chiefOccupant.RespectivePhi.IdentityUserId != currentUser.Id)
                    return GetErrorResult("Current User is not allocated to this Chief Ocuupant as respective PHI!");

                if (chiefOccupant.ReportingApplications.Any(x => x.Type == Domain.Enums.ApplicationType.Base && x.Status == Domain.Enums.ApplicationStatus.Completed))
                    return GetErrorResult("No Valid Application to complete!");

                var baseApplication = chiefOccupant.ReportingApplications.FirstOrDefault(x => x.Type == Domain.Enums.ApplicationType.Base);

                if (baseApplication == null)
                    return GetErrorResult("No valid base Application");

                if (!baseApplication.SurroundingSets.Any())
                    return GetErrorResult("No Surrounding Sets Added Yet!!");

                var approveResponse = await _workItemService.ApproveCORegistrationAsync(chiefOccupant.WorkItem.Id);

                if (!approveResponse.Succeeded)
                    return GetErrorResult(approveResponse.Errors.ToArray());

                baseApplication.Status = Domain.Enums.ApplicationStatus.Completed;
                _dataContext.ReportingApplications.Update(baseApplication);                

                await _dataContext.SaveChangesAsync();

                return new ResponseResult<ReportingApplication>()
                {
                    Errors = null,
                    Result = baseApplication,
                    Succeeded = true
                };

            }
            else
            {
                if (isPublicSurroundingApplication)
                {
                    var currentUser = await _identityService.GetCurrentUserAsync();
                    var chiefOccupant = await _chiefOccupantService.GetChiefOccupantByIdentityId(currentUser.Id);

                    if (currentUser.UserType != CIDRS.Identity.Domain.Enums.ApplicationUserType.ChiefOccupant)
                        return GetErrorResult("Current User is not authorized to Complete application!");

                    if (chiefOccupant.IdentityUserId != currentUser.Id)
                        return GetErrorResult("You are not allow to Complete application for others!");

                    if (!chiefOccupant.ReportingApplications.Any(x => x.Status == Domain.Enums.ApplicationStatus.Pending))
                        return GetErrorResult("No valid application to Complete!");

                    var baseApplication = chiefOccupant.ReportingApplications.FirstOrDefault(x => x.Type == Domain.Enums.ApplicationType.PublicSurroundingComplaints && x.Status == Domain.Enums.ApplicationStatus.Pending);

                    if (baseApplication == null)
                        return GetErrorResult("No valid base Application");

                    if (!baseApplication.SurroundingSets.Any())
                        return GetErrorResult("No Surrounding Sets Added Yet!!");

                    var workItemResult = await _workItemService.CreateWorkItemAsync(Domain.Enums.WorkItemType.ReportingApplication, baseApplication.Id);
                    var workItemAssignResult = await _workItemService.ReAsssignAsync(workItemResult.Result.Id, chiefOccupant.PhiId.Value);



                    baseApplication.Status = Domain.Enums.ApplicationStatus.Completed;
                    _dataContext.ReportingApplications.Update(baseApplication);

                    await _dataContext.SaveChangesAsync();

                    return new ResponseResult<ReportingApplication>()
                    {
                        Errors = null,
                        Result = baseApplication,
                        Succeeded = true
                    };
                }
                else
                {
                    var currentUser = await _identityService.GetCurrentUserAsync();
                    var chiefOccupant = await _chiefOccupantService.GetChiefOccupantByIdentityId(currentUser.Id);

                    if (currentUser.UserType != CIDRS.Identity.Domain.Enums.ApplicationUserType.ChiefOccupant)
                        return GetErrorResult("Current User is not authorized to Complete application!");

                    if (chiefOccupant.IdentityUserId != currentUser.Id)
                        return GetErrorResult("You are not allow to Complete application for others!");

                    if (!chiefOccupant.ReportingApplications.Any(x => x.Status == Domain.Enums.ApplicationStatus.Pending))
                        return GetErrorResult("No valid application to Complete!");

                    var baseApplication = chiefOccupant.ReportingApplications.FirstOrDefault(x => x.Type == Domain.Enums.ApplicationType.Base);

                    if (baseApplication == null)
                        return GetErrorResult("No valid base Application");

                    var application = chiefOccupant.ReportingApplications.FirstOrDefault(x => x.Type == Domain.Enums.ApplicationType.HomeSurroundingAllerts && x.Status == Domain.Enums.ApplicationStatus.Pending);

                    if (application == null)
                        return GetErrorResult("No valid Application");

                    var relativeSurroundingSetIds = application.SurroundingSets.Select(x => x.RelativeSurroundingSet.Id).ToList();

                    var basedSurroundingSets = baseApplication.SurroundingSets.ToList();

                    var notCompletedSurroundingSets = basedSurroundingSets.Where(x => !relativeSurroundingSetIds.Contains(x.Id)).ToList();

                    if (notCompletedSurroundingSets.Any())
                    {
                        var notCompleted = notCompletedSurroundingSets.Select(x => string.Format("Surrounding Set: {0} is not uploaded yet!", x.Name)).ToArray();
                        return GetErrorResult(notCompleted);
                    }

                    var workItemResult = await _workItemService.CreateWorkItemAsync(Domain.Enums.WorkItemType.ReportingApplication, application.Id);
                    var workItemAssignResult = await _workItemService.ReAsssignAsync(workItemResult.Result.Id, chiefOccupant.PhiId.Value);



                    application.Status = Domain.Enums.ApplicationStatus.Completed;
                    _dataContext.ReportingApplications.Update(application);

                    await _dataContext.SaveChangesAsync();

                    return new ResponseResult<ReportingApplication>()
                    {
                        Errors = null,
                        Result = application,
                        Succeeded = true
                    };
                }
            }
        }

        private ResponseResult<ReportingApplication> GetErrorResult(params string[] errors)
        {
            return new ResponseResult<ReportingApplication>()
            {
                Errors = errors,
                Result = null,
                Succeeded = false
            };
        }

        /// <summary>
        /// Get AdminId To create
        /// </summary>
        /// <returns></returns>
        private int GetApplicationId()
        {
            // Get Last StaffId
            var result = _dataContext.ReportingApplications.OrderBy(a => a.Id).LastOrDefault();

            if (result != null)
                return result.Id + 1;
            else
                return 1;

        }

    }
}
