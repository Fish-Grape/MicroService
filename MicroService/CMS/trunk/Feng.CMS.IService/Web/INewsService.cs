using Feng.Basic;
using Feng.CMS.Model;
using System;
using System.Collections.Generic;

namespace Feng.CMS.IService
{
    public partial interface INewsService
    {
        ReturnResult<List<QueryNewsListModel>> QueryNewsList(string appid, int cateid);
    }
}
