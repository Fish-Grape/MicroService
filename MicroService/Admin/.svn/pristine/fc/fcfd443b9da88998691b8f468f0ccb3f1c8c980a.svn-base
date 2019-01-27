using Feng.Admin.Entity;
using Feng.Admin.IService;
using Feng.Admin.Model;
using Feng.Admin.Service.Common;
using Feng.Basic;
using Feng.Core;
using Feng.Core.Config;
using Feng.Redis;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Feng.Admin.Service
{
    public class PlatformService : IPlatformService
    {
        private readonly SqlSugarClient _dbContext;
        private readonly RedisClient _redisClient;
        private readonly IJsonHelper _jsonHelper;
        private readonly IUser _user;
        private readonly IConfig _configCenter;
        public PlatformService(SqlSugarClient dbContext,
            RedisClient redisClient,
            IJsonHelper jsonHelper,
            IUser user, 
            IConfig configCenter)
        {
            _dbContext = dbContext;
            _redisClient = redisClient;
            _jsonHelper = jsonHelper;
            _user = user;
            _configCenter = configCenter;
        }
        /// <summary>
        /// 查询平台列表
        /// </summary>
        /// <returns></returns>
        public async Task<ReturnResult<dynamic>> QueryPlatforms()
        {
            var redis = _redisClient.GetDatabase(_configCenter.StringGet(SysConfig.RedisConnectionKey), 2);
            var redisList = await redis.StringGetAsync(CacheKeys.PlatformKey);

            if (!string.IsNullOrWhiteSpace(redisList))
            {
                return new ReturnResult<dynamic>(_jsonHelper.DeserializeObject<List<PlatformInfo>>(redisList));
            }
            else
            {
                var list = await _dbContext.Queryable<PlatformInfo>()
                    .OrderBy(it => it.SortId, OrderByType.Asc)
                    .ToListAsync();
                await redis.StringSetAsync(CacheKeys.PlatformKey, _jsonHelper.SerializeObject(list));
                return new ReturnResult<dynamic>(list);
            }
        }
        /// <summary>
        /// 设置平台信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ReturnResult> SetPlatform(PlatformModel model)
        {
            try
            {
                _dbContext.Ado.BeginTran();

                var item = new PlatformInfo() {
                    Id = model.Id,
                    Name = model.Name,
                    Key = model.Key,
                    Author = model.Author,
                    Icon = model.Icon,
                    Developer = model.Developer,
                    Remark = model.Remark,
                    SortId = model.SortId,
                    IsDel = model.IsDel,
                    AddTime = model.AddTime
                }; 
                if (item.Id > 0)
                {
                    await _dbContext.Updateable(item)
                        .ExecuteCommandAsync();
                }
                else
                {
                    item.AddTime = DateTime.Now;
                    item.IsDel = false;
                    await _dbContext.Insertable(item).ExecuteCommandAsync();
                }

                var redis = _redisClient.GetDatabase(_configCenter.StringGet(SysConfig.RedisConnectionKey), 2);
                await redis.KeyDeleteAsync(CacheKeys.PlatformKey);

                _dbContext.Ado.CommitTran();

            }
            catch (Exception ex)
            {
                _dbContext.Ado.RollbackTran();
                throw new Exception("事务执行失败", ex);
            }
            return new ReturnResult();
        }
    }
}
