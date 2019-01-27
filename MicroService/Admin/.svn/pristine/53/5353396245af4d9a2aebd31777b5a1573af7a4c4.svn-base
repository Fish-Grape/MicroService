using Feng.Admin.Model;
using Feng.Basic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Feng.Admin.IService
{
    public interface IMenuService
    {
        /// <summary>
        /// 查询平台菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ReturnResult<dynamic>> QueryAllMenus(QueryAllMenusModel model);
        /// <summary>
        /// 设置平台菜单信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ReturnResult> SetPlatform(MenuModel model);
        /// <summary>
        /// 查询登陆用户拥有菜单
        /// </summary>
        /// <returns></returns>
        Task<ReturnResult<dynamic>> QueryUserMenus();
    }
}
