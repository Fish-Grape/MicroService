using Feng.Admin.Entity;
using Feng.Admin.IService;
using Feng.Admin.Model;
using Feng.Basic;
using Feng.Core;
using Feng.Redis;
using Feng.Utils;
using SqlSugar;
using System;
using System.Threading.Tasks;

namespace Feng.Admin.Service
{
    public class ApiService : IApiService
    {
        private readonly SqlSugarClient _dbContext;
        private readonly RedisClient _redisClient;
        private readonly IJsonHelper _jsonHelper;
        private readonly IUser _user;
        public ApiService(SqlSugarClient dbContext,
            RedisClient redisClient,
            IJsonHelper jsonHelper,
            IUser user)
        {
            _dbContext = dbContext;
            _redisClient = redisClient;
            _jsonHelper = jsonHelper;
            _user = user;
        }

        public async Task<ReturnResult<PageList<dynamic>>> QueryApis(QueryApiModel model)
        {
            var list = new PageList<dynamic>();
            var totalNumber = 0;
            var result = await _dbContext.Queryable<ApiInfo>()
                                 .WhereIF(!model.ProjectKey.IsEmpty(), it => it.ProjectName == model.ProjectKey)
                                 .ToPageListAsync(model.Page, model.PageSize, totalNumber);

            result.Key.ForEach(k => {
                list.DataList.Add(k);
            });
            list.Page = model.Page;
            list.PageSize = model.PageSize;
            list.TotalCount = result.Value;

            return new ReturnResult<PageList<dynamic>>(list);

        }

        public async Task<ReturnResult> SetApi(ApiModel model)
        {
            var item = new ApiInfo() {
                Id = model.Id,
                ProjectName = model.ProjectName,
                AllowScope = model.AllowScope,
                Controller = model.Controller,
                ControllerName = model.ControllerName,
                CreateTime = model.CreateTime,
                Disabled = model.Disabled,
                Message = model.Message,
                Method = model.Method,
                UpdateTime = model.UpdateTime,
                Url = model.Url
            };
            if (item.Id > 0)
            {
                item.UpdateTime = DateTime.Now;
                await _dbContext.Updateable(item)
                    .IgnoreColumns(it => new { it.CreateTime })
                    .ExecuteCommandAsync();
            }
            else
            {
                item.UpdateTime = DateTime.Now;
                item.CreateTime = DateTime.Now;
                await _dbContext.Insertable(item)
                    .ExecuteCommandAsync();
            }
            return new ReturnResult();
        }
    }
}
