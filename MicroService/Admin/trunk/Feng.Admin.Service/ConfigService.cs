using Feng.Admin.Entity;
using Feng.Admin.IService;
using Feng.Admin.Model;
using Feng.Basic;
using Feng.Core;
using Feng.Core.Exceptions;
using Feng.Redis;
using SqlSugar;
using System;
using System.Threading.Tasks;

namespace Feng.Admin.Service
{
    public class ConfigService : IConfigService
    {
        private readonly SqlSugarClient _dbContext;
        private readonly IJsonHelper _jsonHelper;
        private readonly IUser _user;
        public ConfigService(SqlSugarClient dbContext,
            IJsonHelper jsonHelper,
            IUser user)
        {
            _dbContext = dbContext;
            _jsonHelper = jsonHelper;
            _user = user;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ReturnResult<PageList<dynamic>>> QueryAppConfigList(QueryAppConfigModel model)
        {
            var list = new PageList<dynamic>();
            var totalNumber = 0;
            // 环境库
            Enum.TryParse<Env>(model.Environment, out var env);
            var tableName = string.Empty;
            switch (env)
            {
                case Env.dev:
                    tableName = $"tb_appconfig_{Env.dev.ToString()}";
                    break;
                case Env.pro:
                    tableName = $"tb_appconfig_{Env.pro.ToString()}";
                    break;
                default:
                    throw new FengException("fc_001", "环境不存在");
            }
            // 执行
            var result = await _dbContext.Queryable<AppConfigInfo>().AS(tableName)
                                .WhereIF(!string.IsNullOrWhiteSpace(model.AppId), it => it.ConfigAppId == model.AppId)
                                .WhereIF(!string.IsNullOrWhiteSpace(model.NameSpace), it => it.ConfigNamespaceName == model.NameSpace)
                                .ToPageListAsync(model.Page, model.PageSize, totalNumber);

            result.Key.ForEach(k => {
                list.DataList.Add(k);
            });
            list.Page = model.Page;
            list.PageSize = model.PageSize;
            list.TotalCount = result.Value;

            return new ReturnResult<PageList<dynamic>>(list);

        }

        public async Task<ReturnResult<dynamic>> QueryAppList()
        {
            var list = await _dbContext.Queryable<AppInfo>()
                                .ToListAsync();
            return new ReturnResult<dynamic>(list);
        }

        public async Task<ReturnResult<PageList<dynamic>>> QueryAppProjectList(QueryAppProjectModel model)
        {
            var list = new PageList<dynamic>();
            var totalNumber = 0;
            var result = await _dbContext.Queryable<AppNamespaceInfo>()
                                .WhereIF(!string.IsNullOrWhiteSpace(model.AppId), it => it.AppId == model.AppId)
                                .ToPageListAsync(model.Page, model.PageSize, totalNumber);

            result.Key.ForEach(k => {
                list.DataList.Add(k);
            });
            list.Page = model.Page;
            list.PageSize = model.PageSize;
            list.TotalCount = result.Value;

            return new ReturnResult<PageList<dynamic>>(list);
        }

        public async Task<ReturnResult> SetAppConfigInfo(AppConfigModel model)
        {
            Enum.TryParse<Env>(model.Environment, out var env);
            var tableName = "tb_appconfig_test";
            switch (env)
            {
                case Env.dev:
                    tableName = $"tb_appconfig_{Env.dev.ToString()}";
                    break;
                case Env.pro:
                    tableName = $"tb_appconfig_{Env.pro.ToString()}";
                    break;
                default:
                    throw new FengException("fc_001", "环境不存在");
            }
            // 编辑或新增
            var item = new AppConfigInfo() {
                Id = model.Id,
                ConfigAppId = model.ConfigAppId,
                ConfigKey = model.ConfigKey,
                ConfigNamespaceName = model.ConfigNamespaceName,
                ConfigValue = model.ConfigValue,
                Remark = model.Remark,
                IsDeleted = model.IsDeleted
            };
            if (item.Id > 0)
            {
                // 基础字段不容许更新
                item.LastTime = DateTime.Now;
                item.Version = _dbContext.Queryable<AppConfigInfo>().AS(tableName).Max(it => it.Version) + 1;
                await _dbContext.Updateable(item).AS(tableName)
                                .IgnoreColumns(it => new { it.CreateTime })
                                .ExecuteCommandAsync();
            }
            else
            {
                item.CreateTime = DateTime.Now;
                item.LastTime = DateTime.Now;
                item.Version = _dbContext.Queryable<AppConfigInfo>().AS(tableName).Max(it => it.Version) + 1;
                await _dbContext.Insertable(item).AS(tableName)
                                .ExecuteCommandAsync();
            }
            return new ReturnResult();

        }

        public async Task<ReturnResult> SetAppInfo(AppModel model)
        {
            var item = new AppInfo() {
                Id = model.Id,
                AppId = model.AppId,
                Name = model.Name,
                Secret = model.Secret,
                Remark = model.Remark
            };
            if (item.Id > 0)
            {
                // 基础字段不容许更新
                await _dbContext.Updateable(item)
                                .ExecuteCommandAsync();
            }
            else
            {
                await _dbContext.Insertable(item)
                                .ExecuteCommandAsync();
            }
            return new ReturnResult();
        }

        public async Task<ReturnResult> SetAppProjectInfo(AppProjectModel model)
        {
            var item = new AppNamespaceInfo() {
                Id = model.Id,
                AppId = model.AppId,
                Name = model.Name,
                Comment = model.Comment,
                IsPublic = model.IsPublic
            };
            if (item.Id > 0)
            {
                // 基础字段不容许更新
                item.LastTime = DateTime.Now;
                item.LastUid = Convert.ToInt64(_user.Id);
                await _dbContext.Updateable(item)
                                .IgnoreColumns(it => new { it.CreateUid, it.CreateTime })
                                .ExecuteCommandAsync();
            }
            else
            {
                item.CreateTime = DateTime.Now;
                item.CreateUid = Convert.ToInt64(_user.Id);
                item.LastTime = DateTime.Now;
                item.LastUid = model.CreateUid;
                await _dbContext.Insertable(item)
                                .ExecuteCommandAsync();
            }

            return new ReturnResult();
        }
    }
}
