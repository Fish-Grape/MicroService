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
    /// Api资源控制器
    /// </summary>
    [Produces("application/json")]
    public class ApiController : Controller
    {
        private readonly IApiService _apiService;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="apiService"></param>
        public ApiController(IApiService apiService)
        {
            _apiService = apiService;
        }

        /// <summary>
        /// 查询Api资源
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize("permission")]
        [HttpGet("/Api/QueryApiList")]
        public async Task<ReturnResult<PageList<dynamic>>> QueryApiList([FromQuery]QueryApiModel model)
        {
            return await _apiService.QueryApis(model);
        }
        /// <summary>
        /// 设置Api资源
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize("permission")]
        [HttpPost("/Api/SetApi")]
        public async Task<ReturnResult> SetApi([FromBody]ApiModel model)
        {
            return await _apiService.SetApi(model);
        }

    }
}