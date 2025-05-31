using CIDRS.Core.Modules.Polices.Extensions;
using CIDRS.Core.Modules.Polices.Models.Request;
using CIDRS.Domain.Models.Entity.Polices;
using CIDRS.Infrastructure;
using CIDRS.Shared.Common.Api.Models;
using CIDRS.Shared.Common.Extensions;
using CIDRS.Shared.Common.Pagination.Extensions;
using CIDRS.Shared.Common.Pagination.Models;
using CIDRS.Shared.Utility.EmailManipulator.Extensions;
using CIDRS.Shared.Utility.SmsManipulator.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIDRS.Core.Modules.Polices.Services
{
    public class PoliceService : IPoliceService
    {
        private readonly ApplicationDataContext _dataContext;
        private readonly ISmsManipulatorService _smsSender;

        public PoliceService(ApplicationDataContext dataContext, ISmsManipulatorService smsSender)
        {
            _dataContext = dataContext;
            _smsSender = smsSender;
        }
        public async Task<ResponseResult<Police>> CreatePoliceAsync(CreatePoliceRequest request)
        {
            var police = request.ToEntityModel();
            police.SetCreatedTime();
            police.EnsureId(GetPoliceId());

            await _dataContext.Polices.AddAsync(police);
            await _dataContext.SaveChangesAsync();

            await SendPoliceCreationSmsNotificationAsync(police);
            return new ResponseResult<Police>()
            {
                Errors = null,
                Result = police,
                Succeeded = true
            };

        }

        public async Task<ResponseResult<Police>> GetPoliceAsync(int id)
        {
            var police = await _dataContext.Polices.Include(x => x.PoliceStation).ThenInclude(x => x.MohAreaPoliceStations).FirstOrDefaultAsync(x => x.Id == id);
            if (police == null)
                return new ResponseResult<Police>()
                {
                    Errors = new[] { "No Police officer Found!" },
                    Result = null,
                    Succeeded = false
                };

            return new ResponseResult<Police>()
            {
                Errors = null,
                Result = police,
                Succeeded = true
            };

        }

        public async Task<ResponseResult<List<Police>>> PoliceLookupAsync(int mohAreaId)
        {
            var polices = await _dataContext.Polices.Include(x => x.PoliceStation)
                                                    .ThenInclude(x => x.MohAreaPoliceStations).Where(x => x.PoliceStation.MohAreaPoliceStations.Any(y => y.MohAreaId == mohAreaId)).ToListAsync();

            return new ResponseResult<List<Police>>()
            {
                Errors = null,
                Result = polices,
                Succeeded = true
            };
        }

        public async Task<PaginationVM<Police>> IndexPolicesAsync(PoliceSearchRequest searchRequest)
        {
            searchRequest.BasicSearchValue = searchRequest.BasicSearchValue?.Trim();
            IQueryable<Police> phis = GetBasicSearchResult(searchRequest);

            return await phis.PaginateAsync(searchRequest?.PaginationOption?.Page, searchRequest?.PaginationOption?.PageSize);

        }

        /// <summary>
        /// Get AdminId To create
        /// </summary>
        /// <returns></returns>
        private int GetPoliceId()
        {
            // Get Last StaffId
            var result = _dataContext.Polices.OrderBy(a => a.Id).LastOrDefault();

            if (result != null)
                return result.Id + 1;
            else
                return 1;

        }

        private async Task SendPoliceCreationSmsNotificationAsync(Police police)
        {
            try
            {
                string phiName = police.FullName;
                string fileName = "PoliceCreation.txt";

                var templatePath = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot\\Templates\\SMS", fileName);
                var content = await templatePath.Render(new string[] { phiName });

                await _smsSender.SendSmsAsync(police.Mobile, content);
            }
            catch 
            {

            }
        }

        private IQueryable<Police> GetBasicSearchResult(PoliceSearchRequest request)
        {
            IQueryable<Police> polices = _dataContext.Polices.Include(x => x.PoliceStation)
                                                            .ThenInclude(x => x.MohAreaPoliceStations)
                                                                .ThenInclude(x => x.MohArea);


            if (!string.IsNullOrWhiteSpace(request.BasicSearchValue))
            {
                request.BasicSearchValue = request.BasicSearchValue.ToLower().Trim();

                polices = polices.Where(x => x.FullName.ToLower().Contains(request.BasicSearchValue) ||
                                       x.PoliceStation.Name.ToLower().Contains(request.BasicSearchValue) ||
                                       x.Mobile.Contains(request.BasicSearchValue) ||
                                       x.Identifier.ToLower().Contains(request.BasicSearchValue));
            }

            

            if (request.MohAreaId != null)
            {
                polices = polices.Where(x => x.PoliceStation.MohAreaPoliceStations.Any(y => y.MohAreaId == request.MohAreaId));
            }
            polices = polices.OrderBy(e => e.FullName);

            return polices;
        }
    }
}
