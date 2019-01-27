using Feng.Basic;
using Feng.Identity.Model.Tool;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Feng.Identity.IService
{
    public interface IToolService
    {
        Task<ReturnResult<List<RegionModel>>> GetProvince();

        Task<ReturnResult<List<RegionModel>>> GetCityByProvinceId(short provinceId);

        Task<ReturnResult<List<RegionModel>>> GetRegionByCityId(short cityId);

        Task<ReturnResult<AllRegionModel>> GetAllRegionByRegionId(short regionId);
    }
}
