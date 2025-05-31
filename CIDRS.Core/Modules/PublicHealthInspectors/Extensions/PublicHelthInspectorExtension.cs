using AutoMapper;
using CIDRS.Core.Common.Models;
using CIDRS.Core.Modules.PublicHealthInspectors.Commands;
using CIDRS.Core.Modules.PublicHealthInspectors.Models.Requests;
using CIDRS.Core.Modules.PublicHealthInspectors.ViewModels;
using CIDRS.Domain.Models.Entity.PublicHealthInspectors;
using CIDRS.Identity.Infrastructure;
using CIDRS.Identity.Models.Request;
using CIDRS.Identity.Models.Response;
using CIDRS.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CIDRS.Core.Modules.PublicHealthInspectors.Extensions
{
    public static class PublicHelthInspectorExtension
    {
        public static RegiterUserByAdminRequest ToRegiterUserByAdminRequest(this RegisterPhiRequest request)
        {
            return new RegiterUserByAdminRequest()
            {
                Email = request.Email,
                FullName = request.FullName,
                PhoenNumber = request.PhoenNumber,
                UserType = request.UserType
            };
        }

        public static PublicHealthInspector ToEntityModel(this RegisterPhiRequest request, string id)
        {
            return new PublicHealthInspector()
            {
                MohAreaId = request.MohAreaId,
                DistrictId = request.DistrictId,
                IdentityUserId = id,
                FullName = request.FullName,
                PhoneNumber = request.PhoenNumber
                
            };
        }


        public static RegisterPhiRequest ToCommand(this RegisterPhiCommand request)
        {
            return new RegisterPhiRequest()
            {
                DistrictId = request.DistrictId,
                Email = request.Email,
                FullName = request.FullName,
                MohAreaId = request.MohAreaId,
                 PhoenNumber = request.PhoenNumber,
                 UserType = CIDRS.Identity.Domain.Enums.ApplicationUserType.Phi
            };
        }

        public static PhiSearchRequest ToServiceRequest(this IndexPublicHealthInspctorCommand command)
        {
            return new PhiSearchRequest()
            {
                BasicSearchValue = command.BasicSearchValue,
                DistrictId = command.DistrictId,
                MohAreaId = command.MohAreaId,
                PaginationOption = command.PaginationOption
            };
        }

        public static async Task<PublicHealthInspectorVM> ToViewModelAsync (this PublicHealthInspector phi, IMapper mapper, IdentityDataContext identityContext, ApplicationDataContext _dataContext)
        {
            var absolutePhi = await _dataContext.PublicHealthInspectors.Include(x => x.District)
                                                                        .Include(x => x.MohArea)
                                                                            .SingleOrDefaultAsync(x => x.Id == phi.Id);
            var viewModel = mapper.Map<PublicHealthInspectorVM>(absolutePhi);
            var user = await identityContext.Users.SingleOrDefaultAsync(x => x.Id == phi.IdentityUserId);
            viewModel.User = mapper.Map<UserVM>(user);
           
            return viewModel;
        }
    }
}
