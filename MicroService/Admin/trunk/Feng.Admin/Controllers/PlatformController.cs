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
    /// 平台控制器
    /// </summary>
    [Produces("application/json")]
    public class PlatformController : Controller
    {
        private readonly IPlatformService _platformService;
        public PlatformController(IPlatformService platformService)
        {
            _platformService = platformService;
        }

        /// <summary>
        /// 查询平台列表
        /// </summary>
        /// <returns></returns>
        [Authorize("permission")]
        [HttpGet("/Platform/QueryPlatforms")]
        public async Task<ReturnResult<dynamic>> QueryPlatforms()
        {
            return await _platformService.QueryPlatforms();
        }
        /// <summary>
        /// 设置平台信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize("permission")]
        [HttpPost("/Platform/SetPlatform")]
        public async Task<ReturnResult> SetPlatform([FromBody]PlatformModel model)
        {
            return await _platformService.SetPlatform(model);
        }
    }
}