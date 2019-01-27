using Feng.Basic;
using Feng.CMS.Model.Navigation;
using System;
using System.Collections.Generic;

namespace Feng.CMS.IService
{
    public interface IMenu_ClassfyService
    {
        /// <summary>
        /// 根据导航分类ID获取导航分类资料
        /// </summary>
        /// <param name="ID">导航分类Id</param
        /// <returns></returns>
        ReturnResult<Menu_ClassfyModel> GetClassfyViewByID(string appid,int ID);

        /// <summary>
        /// 获取全部导航分类接口
        /// </summary>
        /// <returns>ReturnResult<IList<Menu_ClassfyModel>></returns>
        ReturnResult<List<Menu_ClassfyModel>> GetClassfyList(string appid);

        /// <summary>
        /// 新增导航分类
        /// </summary>
        /// <param name="moMenu_ClassfyModel">导航分类属性</param>
        /// <returns>返回参数</returns>
        ReturnResult<ReturnMenu_ClassfyModel> Insert(string appid, AddMenu_ClassfyModel moMenu_ClassfyModel);

        /// <summary>
        /// 修改导航分类
        /// </summary>
        /// <param name="moMenu_ClassfyModel">系统导航分类属性</param>
        /// <returns>返回参数</returns>
        ReturnResult Change(string appid, ModifyMenu_ClassfyModel moMenu_ClassfyModel);

        /// <summary>
        /// 删除导航分类
        /// </summary>
        /// <param name="MenuId">导航分类ID</param>
        /// <returns>返回参数</returns>
        ReturnResult Delete(string appid, int id);
    }
}
