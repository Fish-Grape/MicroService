using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Feng.Basic;
using Feng.Core.Config;
using Feng.Core.Exceptions;
using Feng.Identity.IService;
using Feng.Identity.Model.Tool;
using Feng.Redis;
using Feng.Utils;
using Feng.Utils.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Feng.Identity.Controllers
{
    /// <summary>
    /// 工具控制器
    /// </summary>
    public class ToolController : Controller
    {
        private readonly RedisClient _redisClient;
        private readonly IConfig _config;
        private readonly IToolService _toolService;
        public ToolController(IConfig config, RedisClient redisClient, IToolService toolService)
        {
            _config = config;
            _redisClient = redisClient;
            _toolService = toolService;
        }

        /// <summary>
        /// 图形验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet("/ValidateCode")]
        public IActionResult ValidateCode(string guid, int width = 100, int height = 32)
        {
            if (guid.IsEmpty())
                throw new FengException("f_001", "请输入用户标识");
            var rediscontect = _config.StringGet("RedisDefaultServer");
            var redis = _redisClient.GetDatabase(rediscontect, 5);
            var code = Randoms.CreateRandomValue(4, false);
            redis.StringSet($"ImgCode:{guid}", code, new TimeSpan(0, 0, 0, 300));
            var st = VerifyCode.CreateByteByImgVerifyCode(code, width, height);
            return File(st, "image/jpeg");
        }
        /// <summary>
        /// 获取所有省份
        /// </summary>
        /// <returns></returns>
        [HttpGet("/province")]
        public async Task<ReturnResult<List<RegionModel>>> GetProvince() {
            return await _toolService.GetProvince();
        }
        /// <summary>
        /// 根据省会编号获取所有城市
        /// </summary>
        /// <returns></returns>
        [HttpGet("/city")]
        public async Task<ReturnResult<List<RegionModel>>> GetCityByProvinceId(short provinceId) {
            return await _toolService.GetCityByProvinceId(provinceId);
        }
        /// <summary>
        /// 根据城市编号获取所有区域
        /// </summary>
        /// <returns></returns>
        [HttpGet("/region")]
        public async Task<ReturnResult<List<RegionModel>>> GetRegionByCityId(short cityId)
        {
            return await _toolService.GetRegionByCityId(cityId);
        }

        /// <summary>
        /// 根据区域编号获取省市
        /// </summary>
        /// <returns></returns>
        [HttpGet("/allregion")]
        public async Task<ReturnResult<AllRegionModel>> GetAllRegionByRegionId(short regionId)
        {
            return await _toolService.GetAllRegionByRegionId(regionId);
        }
    }
}