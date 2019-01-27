using Feng.Basic;
using Feng.CMS.IService;
using Feng.CMS.Model.Navigation;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Feng.CMS.Controllers.web.v1
{
    /// <summary>
    /// 前台
    /// </summary>
    [ApiVersion("1.0")]
    [Route("{area}/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class Cms_MenuWebController : Controller
    {
        public readonly ICms_MenuService MenuService;

        public Cms_MenuWebController(ICms_MenuService menuService)
        {
            MenuService = menuService;
        }

        /// <summary>
        /// 获取导航菜单
        /// </summary>
        /// <param name="classFy"></param>
        /// <returns></returns>
        [HttpGet, Route("querylist")]
        public ReturnResult<List<Cms_MenuModel>> GetCms_Menu(string classFy)
        {
            if (string.IsNullOrEmpty(Utils.Extensions.Plat) || string.IsNullOrEmpty(classFy))
                return new ReturnResult<List<Cms_MenuModel>>((int)ErrorCodeEnum.Parameter_Missing, null, "请求参数异常!");

            return MenuService.GetMenuList(Utils.Extensions.Plat, classFy);
        }
    }
}