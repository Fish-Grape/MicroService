using Feng.Basic;
using Feng.CMS.Model.Navigation;
using System;
using System.Collections.Generic;

namespace Feng.CMS.IService
{
    public interface ICms_MenuService
    {

        /// <summary>
        /// 根据菜单ID获取菜单资料
        /// </summary>
        /// <param name="ID">菜单Id</param
        /// <returns></returns>
        Cms_MenuModel GetMenuViewByID(string appid,int ID);

        /// <summary>
        /// 获取全部菜单接口
        /// </summary>
        /// <returns>ReturnResult<IList<Cms_MenuModel>></returns>
        ReturnResult<List<Cms_MenuModel>> GetMenuList(string appid, string classFy);

        /// <summary>
        /// 根据用户Id获得菜单
        /// </summary>
        /// <param name="UserID">用户Id</param>
        ReturnResult<List<Cms_MenuModel>> GetMenuListByUserID(string appid, int UserID);

        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="moCms_MenuModel">菜单属性</param>
        /// <returns>返回参数</returns>
        ReturnResult<ReturnCms_MenuModel> Insert(string appid, AddCms_MenuModel moCms_MenuModel);

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="moCms_MenuModel">系统菜单属性</param>
        /// <returns>返回参数</returns>
        ReturnResult Change(string appid, ModifyCms_MenuModel moCms_MenuModel);

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="MenuId">菜单ID</param>
        /// <returns>返回参数</returns>
        ReturnResult Delete(string appid, int MenuID);
        /// <summary>
        /// 绑定菜单
        /// </summary>
        /// <param name="litMenuModel"></param>
        /// <param name="strParentMenuCode"></param>
        /// <param name="outMenuHtml"></param>
        void BandingMenu(List<Cms_MenuModel> litMenuModel, int iPid, ref String outMenuHtml);
    }
}
