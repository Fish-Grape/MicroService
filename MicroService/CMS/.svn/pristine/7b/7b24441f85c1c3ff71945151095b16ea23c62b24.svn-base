using Feng.Basic;
using Feng.CMS.Entity;
using Feng.CMS.IService;
using Feng.CMS.Model.Navigation;
using Feng.Core;
using Microsoft.Extensions.Logging;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Feng.CMS.Service
{
    class Cms_MenuService : ICms_MenuService
    {
        public readonly SqlSugarClient _dbContext;
        private readonly IJsonHelper _jsonHelper;
        private readonly IUser _user;

        public Cms_MenuService(SqlSugarClient dbContext, IJsonHelper jsonHelper, IUser user)
        {
            _dbContext = dbContext;
            _jsonHelper = jsonHelper;
            _user = user;
        }

        public ReturnResult Change(string appid, ModifyCms_MenuModel x)
        {
            int count = _dbContext.Updateable<cms_menu>()
                .UpdateColumns(f => new cms_menu
                {
                    menu_name = x.menu_name,
                    navigate_url = x.navigate_url,
                    sort = x.sort,
                    pid = x.pid,
                    target=x.target,
                    classfy = x.classfy,
                })
                .Where(a => a.id == x.id)
                .Where(a => a.appid == appid)
                .ExecuteCommand();

            if (count > 0)
                return new ReturnResult();
            else
                return new ReturnResult((int)ErrorCodeEnum.Failed);
        }

        public ReturnResult Delete(string appid, int MenuID)
        {
            int count = _dbContext.Deleteable<cms_menu>()
                .Where(a => a.id == MenuID || a.pid==MenuID)
                .Where(a => a.appid == appid)
                .ExecuteCommand();

            if (count > 0)
                return new ReturnResult();
            else
                return new ReturnResult((int)ErrorCodeEnum.Failed);
        }

        public ReturnResult<List<Cms_MenuModel>> GetMenuList(string appid, string classFy)
        {
            List<Cms_MenuModel> page = new List<Cms_MenuModel>();
            var item = _dbContext.Queryable<cms_menu>().
                Where(a => a.appid == appid && a.classfy == classFy)
                .OrderBy(a => a.id, OrderByType.Asc)
                .ToList();
            if (item.Count <= 0)
            {
                return new ReturnResult<List<Cms_MenuModel>>(page);
            }
            var list = ReList(0, item);

            list.ForEach(x =>
            {
                page.Add(x);
            });
            return new ReturnResult<List<Cms_MenuModel>>(page);
        }

        private List<Cms_MenuModel> ReList(int pid, List<cms_menu> list)
        {
            var child = list.FindAll(x => x.pid == pid);
            if (child.Count <= 0)
                return null;
            List<Cms_MenuModel> result = new List<Cms_MenuModel>();

            child.ForEach(a =>
            {
                Cms_MenuModel model = new Cms_MenuModel()
                {
                    value=a.id,
                    label = a.menu_name,
                    navigate_url = a.navigate_url,
                    target = a.target,
                    sort = a.sort,
                    pid = a.pid,
                    classfy = a.classfy,
                    children = ReList(a.id, list)
                };
                result.Add(model);
            });
            return result;
        }

        #region 绑定菜单
        public void BandingMenu(List<Cms_MenuModel> litMenuModel, int iPid, ref String outMenuHtml)
        {
            IList<Cms_MenuModel> litMenuModelParent = litMenuModel.Where(n => n.pid == iPid).ToList<Cms_MenuModel>();
            if (litMenuModelParent.Count() < 1) return;

            foreach (Cms_MenuModel mo in litMenuModelParent)
            {
                outMenuHtml += " <li " + (mo.pid==0 ? "class='dropdown '" : "") + ">";
                outMenuHtml += "<a href=\"" + mo.navigate_url + "\" " + (mo.pid == 0 ? "class='dropdown-toggle' data-toggle='dropdown'" : "") + ">" + mo.label + "</a>";

                if (litMenuModel.Where(n => n.pid == mo.value).Count() > 0)
                {
                    outMenuHtml += "<ul class=\"dropdown-menu\" aria-labelledby=\"drop" + mo.value + "\">";
                    BandingMenu(litMenuModel, mo.value, ref outMenuHtml);
                    outMenuHtml += "</ul>";
                }
                outMenuHtml += "</li>";
            }
        }
        #endregion

        public ReturnResult<List<Cms_MenuModel>> GetMenuListByUserID(string appid, int UserID)
        {
            throw new NotImplementedException();
        }

        public Cms_MenuModel GetMenuViewByID(string appid, int ID)
        {
            Cms_MenuModel MenuModel = null;
            cms_menu x = _dbContext.Queryable<cms_menu>().Single(a => a.appid == appid && a.id==ID);
            if (x != null)
            {
                MenuModel=new Cms_MenuModel()
                {
                    value = x.id,
                    label = x.menu_name,
                    navigate_url = x.navigate_url,
                    sort = x.sort,
                    pid = x.pid,
                    target = x.target,
                    classfy = x.classfy
                };
            }
            return MenuModel;
        }

        public ReturnResult<ReturnCms_MenuModel> Insert(string appid, AddCms_MenuModel x)
        {
            if (x == null)
                return new ReturnResult<ReturnCms_MenuModel>((int)ErrorCodeEnum.Parameter_Missing, null, "参数异常!");

            cms_menu item = new cms_menu()
            {
                appid=appid,
                menu_name = x.menu_name,
                navigate_url = x.navigate_url,
                sort = x.sort,
                pid = x.pid,
                target = x.target,
                classfy = x.classfy
            };

            int id = _dbContext.Insertable(item).ExecuteReturnIdentity();

            return new ReturnResult<ReturnCms_MenuModel>(new ReturnCms_MenuModel() { id = id });
        }
    }
}
