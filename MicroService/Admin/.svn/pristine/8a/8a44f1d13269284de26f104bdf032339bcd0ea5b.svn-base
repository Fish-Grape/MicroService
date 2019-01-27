using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Feng.Admin.IService;
using Feng.Admin.Model;
using Feng.Basic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Feng.Admin.Controllers
{
    /// <summary>
    /// 微服务配置
    /// </summary>
    [Produces("application/json")]
    public class MicroController : Controller
    {
        private readonly IMicroService _microService;

        public MicroController(IMicroService microService)
        {
            _microService = microService;
        }

        /// <summary>
        /// 查询服务发现全部服务
        /// </summary>
        /// <returns></returns>
        [HttpGet("/Microservice/QueryServiceList")]
        [Authorize("permission")]
        public async Task<ReturnResult<dynamic>> QueryServiceList([FromQuery]QueryServiceModel model)
        {
            return await _microService.QueryServiceList(model);
        }

        /// <summary>
        /// 服务注册
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("/Microservice/SetServiceInfo")]
        [Authorize("permission")]
        public async Task<ReturnResult> SetServiceInfo([FromBody]ServiceModel model)
        {
            return await _microService.SetServiceInfo(model);
        }
        /// <summary>
        /// 服务移除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("/Microservice/DeleteService")]
        [Authorize("permission")]
        public async Task<ReturnResult> DeleteService([FromBody]DeleteServiceModel model)
        {
            return await _microService.DeleteService(model);
        }
        /// <summary>
        /// 查询网关配置
        /// </summary>
        /// <returns></returns>
        [HttpGet("/Microservice/GetApiGatewayConfiguration")]
        [Authorize("permission")]
        public async Task<ReturnResult<dynamic>> GetApiGatewayConfiguration()
        {
            return await _microService.GetApiGatewayConfiguration();
        }
        /// <summary>
        /// 设置网关配置
        /// </summary>
        /// <returns></returns>
        [HttpPost("/Microservice/SetApiGatewayConfiguration")]
        [Authorize("permission")]
        public async Task<ReturnResult> SetApiGatewayConfiguration()
        {
            return await _microService.SetApiGatewayConfiguration();
        }

    }
}