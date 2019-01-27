using Feng.Basic;
using Feng.Core;
using Feng.Core.Config;
using Feng.DbContext;
using Feng.Identity.Entity;
using Feng.Identity.IService;
using Feng.Identity.Model.Tool;
using Feng.Redis;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Feng.Identity.Service
{
    public class ToolService : IToolService
    {
        private readonly IDbRepository<RegionInfo> _dbContext;
        private readonly RedisClient _redisClient;
        private readonly IConfig _config;
        private readonly IJsonHelper _jsonHelper;
        private readonly IDatabase _redis;

        public ToolService(IDbRepository<RegionInfo> dbContext, IConfig config, RedisClient redisClient, IJsonHelper jsonHelper) {
            _dbContext = dbContext;
            _config = config;
            _redisClient = redisClient;
            _jsonHelper = jsonHelper;

            var rediscontect = _config.StringGet("RedisDefaultServer");
            if(!string.IsNullOrEmpty(rediscontect))
                _redis = _redisClient.GetDatabase(rediscontect, 5);
        }
        public async Task<ReturnResult<AllRegionModel>> GetAllRegionByRegionId(short regionId)
        {
            if (regionId <= 0)
                return new ReturnResult<AllRegionModel>((int)ErrorCodeEnum.Parameter_Missing, null, "无效参数!");

            var item = await _dbContext.GetFirstAsync(x => x.regionid == regionId);

            if (item == null)
                return new ReturnResult<AllRegionModel>((int)ErrorCodeEnum.Error_SqlData, null, "没有找到数据!");

            return new ReturnResult<AllRegionModel>(new AllRegionModel()
            {
                ProvinceId = item.provinceid,
                ProvinceName = item.provincename.Trim(),
                CityId = item.cityid,
                CityName = item.cityname.Trim(),
                RegionId = item.regionid,
                RegionName = item.name.Trim()
            });
        }

        public async Task<ReturnResult<List<RegionModel>>> GetCityByProvinceId(short provinceId)
        {
            if (provinceId <= 0)
                return new ReturnResult<List<RegionModel>>((int)ErrorCodeEnum.Parameter_Missing, null, "无效参数!");

            List<RegionInfo> list = null;
            List<RegionModel> rList = null;
            bool isCache = true;

            if (_redis != null)
            {
                var item = await _redis.StringGetAsync($"region_province_{provinceId}");
                if (item.IsNullOrEmpty)
                    isCache = false;
                else
                    rList = _jsonHelper.DeserializeObject<List<RegionModel>>(item);
            }
            if (rList == null || rList.Count <= 0)
            {
                list = await _dbContext.GetListAsync(x => x.parentid == provinceId);
                if (list != null && list.Count > 0)
                {
                    rList = new List<RegionModel>();
                    list.ForEach(x =>
                    {
                        rList.Add(new RegionModel()
                        {
                            RegionId = x.regionid,
                            RegionName = x.name.Trim()
                        });
                    });
                }
            }
            if (!isCache)
                await _redis.StringSetAsync($"region_province_{provinceId}", _jsonHelper.SerializeObject(rList));

            return new ReturnResult<List<RegionModel>>(rList);
        }

        public async Task<ReturnResult<List<RegionModel>>> GetProvince()
        {
            List<RegionInfo> list = null;
            List<RegionModel> rList = null;
            bool isCache = true;

            if (_redis != null)
            {
                var item = await _redis.StringGetAsync("region_province_0");
                if (item.IsNullOrEmpty)
                    isCache = false;
                else
                    rList = _jsonHelper.DeserializeObject<List<RegionModel>>(item);
            }
            if (rList == null || rList.Count <= 0)
            {
                list = await _dbContext.GetListAsync(x => x.parentid==0);
                if (list != null && list.Count > 0)
                {
                    rList = new List<RegionModel>();
                    list.ForEach(x =>
                    {
                        rList.Add(new RegionModel()
                        {
                            RegionId = x.regionid,
                            RegionName = x.name.Trim()
                        });
                    });
                }
            }
            if (!isCache)
                await _redis.StringSetAsync("region_province_0", _jsonHelper.SerializeObject(rList));

            return new ReturnResult<List<RegionModel>>(rList);
        }

        public async Task<ReturnResult<List<RegionModel>>> GetRegionByCityId(short cityId)
        {
            if (cityId <= 0)
                return new ReturnResult<List<RegionModel>>((int)ErrorCodeEnum.Parameter_Missing, null, "无效参数!");

            List<RegionInfo> list = null;
            List<RegionModel> rList = null;
            bool isCache = true;

            if (_redis != null)
            {
                var item = await _redis.StringGetAsync($"region_city_{cityId}");
                if (item.IsNullOrEmpty)
                    isCache = false;
                else
                    rList = _jsonHelper.DeserializeObject<List<RegionModel>>(item);
            }
            if (rList == null || rList.Count <= 0)
            {
                list = await _dbContext.GetListAsync(x => x.parentid == cityId);
                if (list != null && list.Count > 0)
                {
                    rList = new List<RegionModel>();
                    list.ForEach(x =>
                    {
                        rList.Add(new RegionModel()
                        {
                            RegionId = x.regionid,
                            RegionName = x.name.Trim()
                        });
                    });
                }
            }
            if (!isCache)
                await _redis.StringSetAsync($"region_city_{cityId}", _jsonHelper.SerializeObject(rList));

            return new ReturnResult<List<RegionModel>>(rList);
        }
    }
}
