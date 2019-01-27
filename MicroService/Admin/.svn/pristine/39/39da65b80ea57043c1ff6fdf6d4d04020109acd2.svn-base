using System.Threading.Tasks;
using Feng.Admin.IService;
using Feng.Admin.Model;
using Feng.Basic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Feng.Admin.Controllers
{
    /// <summary>
    /// 配置管理控制器
    /// </summary>
    [Produces("application/json")]
    public class ConfigController : Controller
    {

        private readonly IConfigService _configService;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configService"></param>
        public ConfigController(IConfigService configService)
        {
            _configService = configService;
        }

        /// <summary>
        /// 查看所有项目组
        /// </summary>
        /// <returns></returns>
        [Authorize("permission")]
        [HttpGet("/Config/QueryAppList")]
        public async Task<ReturnResult<dynamic>> QueryAppList()
        {
            return await _configService.QueryAppList();
        }
        /// <summary>
        /// 设置项目组信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize("permission")]
        [HttpPost("/Config/SetAppInfo")]
        public async Task<ReturnResult> SetAppInfo([FromBody]AppModel model)
        {
            return await _configService.SetAppInfo(model);
        }
        /// <summary>
        /// 查询配置项目
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize("permission")]
        [HttpGet("/Config/QueryAppProjectList")]
        public async Task<ReturnResult<PageList<dynamic>>> QueryAppProjectList([FromQuery]QueryAppProjectModel model)
        {
            return await _configService.QueryAppProjectList(model);
        }
        /// <summary>
        /// 设置配置项目
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize("permission")]
        [HttpPost("/Config/SetAppProjectInfo")]
        public async Task<ReturnResult> SetAppProjectInfo([FromBody]AppProjectModel model)
        {
            return await _configService.SetAppProjectInfo(model);
        }
        /// <summary>
        /// 查询配置信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize("permission")]
        [HttpGet("/Config/QueryAppConfigList")]
        public async Task<ReturnResult<PageList<dynamic>>> QueryAppConfigList([FromQuery]QueryAppConfigModel model)
        {
            return await _configService.QueryAppConfigList(model);
        }
        /// <summary>
        /// 设置配置信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize("permission")]
        [HttpPost("/Config/SetAppConfigInfo")]
        public async Task<ReturnResult> SetAppConfigInfo([FromBody]AppConfigModel model)
        {
            return await _configService.SetAppConfigInfo(model);
        }


    }
}