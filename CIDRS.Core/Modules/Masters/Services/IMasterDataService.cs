using CIDRS.Domain.Models.Entity.Masters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CIDRS.Core.Modules.Masters.Services
{
    public interface IMasterDataService
    {
        Task<List<District>> GetDistrictsAsync();
        Task<List<MohArea>> GetMohAreasAsync(int? districtId = null);
        Task<List<PoliceStation>> GetPoliceStationsAsync(int? mohAreaId = null);
    }
}
