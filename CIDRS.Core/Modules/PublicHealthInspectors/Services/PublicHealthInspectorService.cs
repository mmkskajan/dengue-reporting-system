using CIDRS.Core.Common.Models;
using CIDRS.Core.Modules.PublicHealthInspectors.Extensions;
using CIDRS.Core.Modules.PublicHealthInspectors.Models.Requests;
using CIDRS.Domain.Models.Entity.PublicHealthInspectors;
using CIDRS.Identity.Models.Response;
using CIDRS.Identity.Services.User;
using CIDRS.Infrastructure;
using CIDRS.Shared.Common.Api.Models;
using CIDRS.Shared.Common.Extensions;
using CIDRS.Shared.Common.Pagination.Extensions;
using CIDRS.Shared.Common.Pagination.Models;
using CIDRS.Shared.Utility.SmsManipulator.Extensions;
using CIDRS.Shared.Utility.SmsManipulator.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIDRS.Core.Modules.PublicHealthInspectors.Services
{
    public class PublicHealthInspectorService : IPublicHealthInspectorService
    {
        private readonly IIdentityService _identityService;
        private readonly ApplicationDataContext _dataContext;
        private readonly ISmsManipulatorService _smsSender;

        public PublicHealthInspectorService(IIdentityService identityService, ApplicationDataContext dataContext, ISmsManipulatorService smsSender)
        {
            _identityService = identityService;
            _dataContext = dataContext;
            _smsSender = smsSender;
        }

        public async Task<ResponseResult<PublicHealthInspector>> RegisterPublicHealthInspectorsAsync(RegisterPhiRequest request)
        {
            request.UserType = CIDRS.Identity.Domain.Enums.ApplicationUserType.Phi;
            var userRequest = request.ToRegiterUserByAdminRequest();
            var userRegistrationResponse = await _identityService.RegisterUserByAdminAsync(userRequest);

            if (!userRegistrationResponse.Status)
            {
                return new ResponseResult<PublicHealthInspector>()
                {
                    Errors = userRegistrationResponse.Errors,
                    Succeeded = false,
                    Result = null
                };
            }
            else
            {
                var phiRequest = request.ToEntityModel(userRegistrationResponse.User.Id);
                phiRequest.SetCreatedTime();
                phiRequest.EnsureId(GetPhiId());

                await _dataContext.PublicHealthInspectors.AddAsync(phiRequest);
                await _dataContext.SaveChangesAsync();

                await SendAdminRegistrationSmsNotificationAsync(userRegistrationResponse.User);
                return new ResponseResult<PublicHealthInspector>()
                {
                    Errors = null,
                    Result = phiRequest,
                    Succeeded = true
                };

            }

        }

        public async Task<ResponseResult<PublicHealthInspector>> ViewPublicHealthInspectorsAsync(int id)
        {


            var phi = await _dataContext.PublicHealthInspectors.Include(x => x.District)
                                                            .Include(x => x.MohArea)
                                                                .FirstOrDefaultAsync(x => x.Id == id);

            if (phi == null)
                return new ResponseResult<PublicHealthInspector>()
                {
                    Errors = new[] { "No Public Health Inspector Found!" },
                    Result = null,
                    Succeeded = false
                };

            return new ResponseResult<PublicHealthInspector>()
            {
                Errors = null,
                Result = phi,
                Succeeded = true
            };



        }

        public async Task<PublicHealthInspector> ViewPublicHealthInspectorsAsync(string identityUserId)
        {


            var phi = await _dataContext.PublicHealthInspectors.Include(x => x.District)
                                                            .Include(x => x.MohArea)
                                                                .FirstOrDefaultAsync(x => x.IdentityUserId == identityUserId);

            return phi;



        }

        public async Task<PaginationVM<PublicHealthInspector>> IndexPublicHealthInspectorsAsync(PhiSearchRequest searchRequest)
        {
            searchRequest.BasicSearchValue = searchRequest.BasicSearchValue?.Trim();
            IQueryable<PublicHealthInspector> phis = GetBasicSearchResult(searchRequest);

            return await phis.PaginateAsync(searchRequest?.PaginationOption?.Page, searchRequest?.PaginationOption?.PageSize);

        }

        public async Task<ResponseResult<List<PublicHealthInspector>>> PhiLookupAsync(int districtId, int mohAreaId)
        {
            var phis = await _dataContext.PublicHealthInspectors.Where(x => x.DistrictId == districtId && x.MohAreaId == mohAreaId).ToListAsync();

            return new ResponseResult<List<PublicHealthInspector>>()
            {
                Errors = null,
                Result = phis,
                Succeeded = true
            };
        }

        #region Private Methods
        private async Task SendAdminRegistrationSmsNotificationAsync(ApplicationUserVM user)
        {
            string phiName = user.FullName;
            string fileName = "PhiRegistration.txt";

            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\Templates\\SMS", fileName);
            var content = await templatePath.Render(new string[] { phiName });

            await _smsSender.SendSmsAsync(user.PhoneNumber, content);
        }

        /// <summary>
        /// Get AdminId To create
        /// </summary>
        /// <returns></returns>
        private int GetPhiId()
        {
            // Get Last StaffId
            var result = _dataContext.PublicHealthInspectors.OrderBy(a => a.Id).LastOrDefault();

            if (result != null)
                return result.Id + 1;
            else
                return 1;

        }


        private IQueryable<PublicHealthInspector> GetBasicSearchResult(PhiSearchRequest request)
        {
            IQueryable<PublicHealthInspector> phis = _dataContext.PublicHealthInspectors
                                                                    .Include(x => x.MohArea)
                                                                    .Include(x => x.District);


            if (!string.IsNullOrWhiteSpace(request.BasicSearchValue))
            {
                request.BasicSearchValue = request.BasicSearchValue.ToLower().Trim();

                phis = phis.Where(x => x.FullName.ToLower().Contains(request.BasicSearchValue) ||
                                       x.PhoneNumber.Contains(request.BasicSearchValue) ||
                                       x.Identifier.ToLower().Contains(request.BasicSearchValue));
            }

            if (request.DistrictId != null)
            {
                phis = phis.Where(x => x.DistrictId == request.DistrictId);
            }

            if (request.MohAreaId != null)
            {
                phis = phis.Where(x => x.MohAreaId == request.MohAreaId);
            }
            phis = phis.OrderBy(e => e.FullName);

            return phis;
        }

        #endregion
    }
}
