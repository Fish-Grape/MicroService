using System.Threading.Tasks;
using Feng.Admin.IService;
using Feng.Admin.Model;
using Feng.Basic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Feng.Admin.Controllers
{
    /// <summary>
    /// 菜单控制器
    /// </summary>
    [Produces("application/json")]
    public class MenuController : Controller
    {
        private readonly IMenuService _menuService;
        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        /// <summary>
        /// 查询平台菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize("permission")]
        [HttpGet("/Menu/QueryAllMenus")]
        public async Task<ReturnResult<dynamic>> QueryAllMenus([FromQuery]QueryAllMenusModel model)
        {
            return await _menuService.QueryAllMenus(model);
        }
        /// <summary>
        /// 设置平台菜单信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize("permission")]
        [HttpPost("/Menu/SetPlatform")]
        public async Task<ReturnResult> SetPlatform([FromBody]MenuModel model)
        {
            return await _menuService.SetPlatform(model);
        }
        /// <summary>
        /// 查询用户菜单
        /// </summary>
        /// <returns></returns>
        [Authorize("permission")]
        [HttpGet("/Menu/QueryUserMenus")]
        public async Task<ReturnResult<dynamic>> QueryUserMenus()
        {
            return await _menuService.QueryUserMenus();
        }

    }
}