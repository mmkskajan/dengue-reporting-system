using CIDRS.Domain.Models.Entity.Masters;
using CIDRS.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIDRS.Core.Modules.Masters.Services
{
    public class MasterDataService : IMasterDataService
    {
        private readonly ApplicationDataContext _dataContext;

        public MasterDataService(ApplicationDataContext dataContext)
        {
            _dataContext = dataContext;  
        }

        public async Task<List<District>> GetDistrictsAsync()
        {
            return await _dataContext.Districts.ToListAsync();
        }

        public async Task<List<MohArea>> GetMohAreasAsync(int? districtId = null)
        {
            return districtId == null ? await _dataContext.MohAreas.ToListAsync() : await _dataContext.MohAreas.Where(x => x.DistrictId == districtId).ToListAsync();
        }

        public async Task<List<PoliceStation>> GetPoliceStationsAsync(int? mohAreaId = null)
        {
            return mohAreaId == null ? await _dataContext.PoliceStations.ToListAsync() : await _dataContext.MohAreaPoliceStations.Include(x => x.PoliceStation).Where(x => x.MohAreaId == mohAreaId).Select(x => x.PoliceStation).ToListAsync();
        }

    }
}
