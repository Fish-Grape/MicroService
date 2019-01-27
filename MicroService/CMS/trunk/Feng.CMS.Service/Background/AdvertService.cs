using Feng.Basic;
using Feng.CMS.Entity;
using Feng.CMS.IService;
using Feng.CMS.Model;
using Feng.Core;
using Feng.DbContext;
using Feng.Utils.Helpers;
using SqlSugar;
using System;
using System.Collections.Generic;

namespace Feng.CMS.Service
{
    public partial class AdvertService : IAdvertService
    {
        public readonly SqlSugarClient _dbContext;
        private readonly IJsonHelper _jsonHelper;
        private readonly IUser _user;
        public AdvertService(
            SqlSugarClient dbContext,
            IJsonHelper jsonHelper,
            IUser user) {
            _dbContext = dbContext;
            _jsonHelper = jsonHelper;
            _user = user;
        }
        #region 后台接口
        public ReturnResult<ReturnAdvertModel> AddAdvert(string appid, AddAdvertModel model)
        {
            if (model == null)
                return new ReturnResult<ReturnAdvertModel>((int)ErrorCodeEnum.Parameter_Missing, null, "参数异常!");

            advert item = new advert()
            {
                appid = appid,
                title = model.title,
                ad_type = model.adType,
                ad_content = model.adContent,
                positionid = model.adPosId,
                visit_url = model.visitUrl,
                begintime = model.startDate,
                endtime = model.endDate,
                order = model.displayOrder,
                status = model.status,
                extfield1=model.extfield1,
                extfield2=model.extfield2,
                extfield3=model.extfield3,
                extfield4=model.extfield4
            };

            int id = _dbContext.Insertable(item).ExecuteReturnIdentity();

            return new ReturnResult<ReturnAdvertModel>(new ReturnAdvertModel() { id = id });
        }

        public ReturnResult<ReturnAdvertPositionModel> AddAdvertPosition(string appid, AddAdvertPositionModel model)
        {
            if (model == null)
                return new ReturnResult<ReturnAdvertPositionModel>((int)ErrorCodeEnum.Parameter_Missing, null, "参数异常!");

            ad_position item = new ad_position()
            {
                appid = appid,
                title = model.title,
                description = model.description,
                order = model.displayOrder,
                status = model.status,
                id = Id.ObjectId()
            };

            int count = _dbContext.Insertable(item).ExecuteCommand();

            if (count > 0)
                return new ReturnResult<ReturnAdvertPositionModel>(new ReturnAdvertPositionModel() { id = item.id });
            else
                return new ReturnResult<ReturnAdvertPositionModel>(new ReturnAdvertPositionModel() { id = "" }); ;
        }

        public ReturnResult ModifyAdvert(string appid, AdvertModel model)
        {
            if (model == null)
                return new ReturnResult((int)ErrorCodeEnum.Parameter_Missing, "参数异常!");

            advert item = new advert()
            {
                id=model.id,
                appid = appid,
                title = model.title,
                ad_type = model.adType,
                ad_content = model.adContent,
                visit_url = model.visitUrl,
                begintime = model.startDate,
                endtime = model.endDate,
                order = model.displayOrder,
                status = model.status,
                positionid = model.adPosId,
                extfield1 = model.extfield1,
                extfield2 = model.extfield2,
                extfield3 = model.extfield3,
                extfield4 = model.extfield4
            };

            int count = _dbContext.Updateable(item).ExecuteCommand();

            if(count>0)
                return new ReturnResult();
            else
                return new ReturnResult((int)ErrorCodeEnum.Failed);


        }

        public ReturnResult ModifyAdvertPosition(string appid, AdvertPositionModel model)
        {
            if (model == null)
                return new ReturnResult((int)ErrorCodeEnum.Parameter_Missing, "参数异常!");

            ad_position item = new ad_position()
            {
                appid = appid,
                id = model.id,
                title = model.title,
                description = model.description,
                order = model.displayOrder,
                status = model.status
            };

            int count = _dbContext.Updateable(item).ExecuteCommand();

            if (count > 0)
                return new ReturnResult();
            else
                return new ReturnResult((int)ErrorCodeEnum.Failed);
        }

        public ReturnResult ModifyAdvertPositionStatus(string appid, string id, bool status)
        {
            int count = _dbContext.Updateable<ad_position>()
                .UpdateColumns(f => new ad_position { status = status })
                .Where(a => a.id == id)
                .Where(a => a.appid == appid)
                .ExecuteCommand();

            if (count > 0)
                return new ReturnResult();
            else
                return new ReturnResult((int)ErrorCodeEnum.Failed);

        }

        public ReturnResult ModifyAdvertStatus(string appid, int id, bool status)
        {
            int count = _dbContext.Updateable<advert>()
                .UpdateColumns(f => new advert { status = status })
                .Where(a => a.id == id)
                .Where(a => a.appid == appid)
                .ExecuteCommand();

            if (count > 0)
                return new ReturnResult();
            else
                return new ReturnResult((int)ErrorCodeEnum.Failed);
        }

        public ReturnResult<List<AdvertModel>> QueryAdvertByPosition(string appid, string posId)
        {
            List<AdvertModel> list = new List<AdvertModel>();
            List<advert> adList = _dbContext.Queryable<advert>()
                .Where(a => a.positionid == posId)
                .Where(a => a.appid == appid)
                .OrderBy(a=>a.order,OrderByType.Asc)
                .ToList();

            if (adList.Count > 0) {
                adList.ForEach(x =>
                {
                    list.Add(new AdvertModel() {
                        id = x.id,
                        adPosId = x.positionid,
                        title = x.title,
                        visitUrl = x.visit_url,
                        adType = x.ad_type,
                        adContent = x.ad_content,
                        startDate = x.begintime,
                        endDate = x.endtime,
                        displayOrder = x.order,
                        status = x.status,
                        extfield1 = x.extfield1,
                        extfield2 = x.extfield2,
                        extfield3 = x.extfield3,
                        extfield4 = x.extfield4
                    });
                });
            }

            return new ReturnResult<List<AdvertModel>>(list);
        }

        public ReturnResult<PageList<AdvertPositionModel>> QueryAllAdvertPosition(string appid, int pageSize, int pageIndex, string title, int status)
        {
            PageList<AdvertPositionModel> page = new PageList<AdvertPositionModel>();
            int totalNumber = 0;
            List<ad_position> list = _dbContext.Queryable<ad_position>()
                .Where(a => a.appid == appid)
                .WhereIF(!string.IsNullOrEmpty(title),a=>a.title.Contains(title))
                .WhereIF(status>=0,a=>a.status==SqlFunc.IIF(status==0,false,true))
                .OrderBy(a=>a.order,OrderByType.Asc)
                .ToPageList(pageIndex, pageSize, ref totalNumber);

            page.TotalCount = totalNumber;
            page.PageSize = pageSize;
            page.Page = pageIndex;

            if (list.Count <= 0) {
                return new ReturnResult<PageList<AdvertPositionModel>>(page);
            }

            list.ForEach(x => {
                AdvertPositionModel model = new AdvertPositionModel()
                {
                    id = x.id,
                    title = x.title,
                    description = x.description,
                    displayOrder = x.order,
                    status = x.status
                };
                page.DataList.Add(model);
            });
            

            return new ReturnResult<PageList<AdvertPositionModel>>(page);
        }

        public ReturnResult RemoveAdvert(string appid, int id)
        {
            int count = _dbContext.Deleteable<advert>()
                .Where(a => a.id == id)
                .Where(a => a.appid == appid)
                .ExecuteCommand();

            if (count > 0)
                return new ReturnResult();
            else
                return new ReturnResult((int)ErrorCodeEnum.Failed);
        }

        public ReturnResult RemoveAdvertPosition(string appid, string id)
        {
            int count = _dbContext.Queryable<advert>()
                .Where(a => a.positionid == id)
                .Where(a => a.appid == appid)
                .Count();

            if (count <= 0)
            {
                int row = _dbContext.Deleteable<ad_position>()
                .Where(a => a.id == id)
                .Where(a => a.appid == appid)
                .ExecuteCommand();
                if (row > 0)
                    return new ReturnResult();
                else
                    return new ReturnResult((int)ErrorCodeEnum.Failed);
            }
            else {
                return new ReturnResult((int)ErrorCodeEnum.Failed, "请先移除类目下内容!");
            }
            
        }
#endregion
    }
}
