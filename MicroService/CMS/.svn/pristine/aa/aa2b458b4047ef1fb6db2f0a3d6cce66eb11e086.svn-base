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
        #region 前台接口
        public ReturnResult<List<AdvertModel>> QueryAdvertByPosition_Web(string appid, string posId)
        {
            List<AdvertModel> list = new List<AdvertModel>();
            //校验广告位是否禁用
            ad_position _Position =_dbContext.Queryable<ad_position>().
                Where(a => a.id == posId && a.status && a.appid == appid).First();
            if (_Position == null)
                return new ReturnResult<List<AdvertModel>>(list);

            List<advert> adList = _dbContext.Queryable<advert>()
                .Where(a => a.positionid == posId && a.status)
                .Where(a => a.appid == appid)
                .Where(a => a.begintime <= DateTime.Now && a.endtime >= DateTime.Now)
                .OrderBy(a => a.order, OrderByType.Asc)
                .ToList();

            if (adList.Count > 0)
            {
                adList.ForEach(x =>
                {
                    list.Add(new AdvertModel()
                    {
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
        #endregion
    }
}
