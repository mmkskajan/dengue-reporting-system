using AutoMapper;
using CIDRS.Core.Common.Models;
using CIDRS.Core.Modules.ChiefOccupants.Commands;
using CIDRS.Core.Modules.ChiefOccupants.Models.Request;
using CIDRS.Core.Modules.ChiefOccupants.ViewModels;
using CIDRS.Domain.Enums;
using CIDRS.Domain.Models.Entity.ChiefOccupants;
using CIDRS.Identity.Infrastructure;
using CIDRS.Identity.Models.Request;
using CIDRS.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIDRS.Core.Modules.ChiefOccupants.Extensions
{
    public static class ChiefOccupantExtension
    {
        public static UserRegistrationRequest ToUserRegistrationRequest(this RegisterChiefOccupantRequest request)
        {
            return new UserRegistrationRequest()
            {
                Email = request.Email,
                FullName = request.FullName,
                PhoenNumber = request.PhoenNumber,
                UserType = request.UserType,
                Password = request.Password
            };
        }

        public static ChiefOccupant ToEntityModel(this RegisterChiefOccupantRequest request, string id)
        {
            return new ChiefOccupant()
            {
                MohAreaId = request.MohAreaId,
                DistrictId = request.DistrictId,
                IdentityUserId = id,
                FullName = request.FullName,
                PhoneNumber = request.PhoenNumber,
                Address = request.Address
            };
        }

        public static RegisterChiefOccupantRequest ToServiceRequest(this RegisterChiefOccupantCommand request)
        {
            return new RegisterChiefOccupantRequest()
            {
                DistrictId = request.DistrictId,
                Email = request.Email,
                FullName = request.FullName,
                MohAreaId = request.MohAreaId,
                PhoenNumber = request.PhoenNumber,
                UserType = CIDRS.Identity.Domain.Enums.ApplicationUserType.ChiefOccupant,
                Address = request.Address,
                Password = request.Password
            };
        }

        public static ChiefOccupantSearchRequest ToServiceRequest(this IndexChiefOccupantCommand command)
        {
            return new ChiefOccupantSearchRequest()
            {
                BasicSearchValue = command.BasicSearchValue,
                DistrictId = command.DistrictId,
                MohAreaId = command.MohAreaId,
                PhiId = command.PhiId,
                PaginationOption = command.PaginationOption
            };
        }

        public static async Task<ChiefOccupantVM> ToViewModelAsync(this ChiefOccupant co, IMapper mapper, IdentityDataContext identityContext, ApplicationDataContext _dataContext)
        {
            var absoluteCo =await _dataContext.ChiefOccupants
                                                    .Include(x => x.MohArea)
                                                    .Include(x => x.District)
                                                    .Include(x => x.Penalties)
                                                    .Include(x => x.RespectivePhi)
                                                        .ThenInclude(x => x.District)
                                                    .Include(x => x.RespectivePhi)
                                                        .ThenInclude(x => x.MohArea)
                                                    .Include(x => x.RespectivePolice)
                                                        .ThenInclude(x => x.PoliceStation)
                                                    .Include(x => x.ReportingApplications)
                                                        .ThenInclude(x => x.SurroundingSets)
                                                        .SingleOrDefaultAsync(x => x.Id == co.Id);

            var viewModel = mapper.Map<ChiefOccupantVM>(absoluteCo);
            var user = await identityContext.Users.SingleOrDefaultAsync(x => x.Id == co.IdentityUserId);
            viewModel.User = mapper.Map<UserVM>(user);
            viewModel.User.IsActive = IsUserActiveAsync(absoluteCo, viewModel.User);
            return viewModel;
        }

        private static bool IsUserActiveAsync(ChiefOccupant chifOccupant, UserVM user)
        {
            return user.UserType != ApplicationUserType.ChiefOccupant || (user.UserType == ApplicationUserType.ChiefOccupant && chifOccupant.ReportingApplications.Any(x => x.Type == Domain.Enums.ApplicationType.Base && x.Status == Domain.Enums.ApplicationStatus.Completed));


        }
    }
}
