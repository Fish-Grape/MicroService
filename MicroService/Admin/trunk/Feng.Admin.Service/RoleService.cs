using Feng.Admin.Entity;
using Feng.Admin.IService;
using Feng.Admin.Model;
using Feng.Admin.Service.Common;
using Feng.Basic;
using Feng.Core;
using Feng.Core.Config;
using Feng.Redis;
using Feng.Utils;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Feng.Admin.Service
{
    public class RoleService : IRoleService
    {
        private readonly SqlSugarClient _dbContext;
        private readonly RedisClient _redisClient;
        private readonly IJsonHelper _jsonHelper;
        private readonly IUser _user;
        private readonly IConfig _configCenter;
        public RoleService(SqlSugarClient dbContext,
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

        public async Task<ReturnResult<dynamic>> QueryAllRoles(QueryRolesByPlatformModel model)
        {
            var list = await _dbContext.Queryable<RoleInfo>()
                                 .WhereIF(!model.PlatformKey.IsEmpty(), it => it.PlatformKey == model.PlatformKey)
                                 .ToListAsync();
            return new ReturnResult<dynamic>(list);
        }

        public async Task<ReturnResult<dynamic>> QueryRoles(QueryRolesByPlatformModel model)
        {
            var list = new List<RoleInfo>();
            var redis = _redisClient.GetDatabase(_configCenter.StringGet(SysConfig.RedisConnectionKey), 2);

            if (string.IsNullOrWhiteSpace(model.PlatformKey))
            {
                // 查询所有角色
                var redisList = await redis.StringGetAsync(CacheKeys.RoleAllUseKey);
                if (!string.IsNullOrWhiteSpace(redisList))
                {
                    list = _jsonHelper.DeserializeObject<List<RoleInfo>>(redisList);
                }
                else
                {
                    list = await _dbContext.Queryable<RoleInfo>()
                                 .Where(it => it.IsDel == false)
                                 .WhereIF(!model.PlatformKey.IsEmpty(), it => it.PlatformKey == model.PlatformKey)
                                 .ToListAsync();
                    await redis.StringSetAsync(CacheKeys.RoleAllUseKey, _jsonHelper.SerializeObject(list));
                }
            }
            else
            {
                // 查询项目对应角色
                list = await _dbContext.Queryable<RoleInfo>()
                                 .Where(it => it.IsDel == false)
                                 .WhereIF(!model.PlatformKey.IsEmpty(), it => it.PlatformKey == model.PlatformKey)
                                 .ToListAsync();
            }
            return new ReturnResult<dynamic>(list);
        }

        public async Task<ReturnResult<dynamic>> QueryRoleInfo(QueryRolesByIdModel model)
        {
            var item = await _dbContext.Queryable<RoleInfo>()
                .Where(it => it.Id == model.RoleId)
                .FirstAsync();
            var apiList = await _dbContext.Queryable<RoleApiInfo>()
                .Where(it => it.RoleId == model.RoleId)
                .ToListAsync();
            var menuList = await _dbContext.Queryable<RoleMenuInfo>()
                .Where(it => it.RoleId == model.RoleId)
                .ToListAsync();
            return new ReturnResult<dynamic>(
                new {
                    RoleInfo = model,
                    MenuList = menuList,
                    ApiList = apiList
                });
        }

        public async Task<ReturnResult> SetRole(ModifyRolesModel model)
        {
            try
            {
                _dbContext.Ado.BeginTran();

                #region 基础信息更新
                var redis = _redisClient.GetDatabase(_configCenter.StringGet(SysConfig.RedisConnectionKey), 2);
                var item = new RoleInfo()
                {
                    Id = model.Id,
                    Name = model.Name,
                    Key = model.Key,
                    PlatformKey = model.PlatformKey,
                    Remark = model.Remark,
                    CreateTime = model.CreateTime,
                    IsDel = model.IsDel,
                    IsSys = model.IsSys,
                    UpdateTime = model.UpdateTime
                };
                if (item.Id > 0)
                {
                    item.UpdateTime = DateTime.Now;
                    await _dbContext.Updateable(item)
                                    .IgnoreColumns(it => new { it.PlatformKey, it.CreateTime, it.IsSys })
                                    .ExecuteCommandAsync();
                }
                else
                {
                    item.CreateTime = DateTime.Now;
                    item.IsDel = false;
                    item.Id = await _dbContext.Insertable(item)
                                               .ExecuteReturnIdentityAsync();
                }
                #endregion

                #region 菜单权限
                // 用户角色操作
                List<RoleMenuInfo> roleMenuList = new List<RoleMenuInfo>();
                foreach (var id in model.MenuIdList)
                {
                    // 防止重复数据
                    if (!roleMenuList.Exists(it => it.MenuId == id))
                    {
                        roleMenuList.Add(new RoleMenuInfo
                        {
                            MenuId = id,
                            RoleId = model.Id
                        });
                    }
                }
                // 删除用户当前角色
                await _dbContext.Deleteable<RoleMenuInfo>().Where(f => f.RoleId == model.Id).ExecuteCommandAsync();
                // 添加用户角色
                if (roleMenuList.Count > 0)
                    await _dbContext.Insertable(roleMenuList).ExecuteCommandAsync();
                #endregion

                #region 菜单权限
                // 用户角色操作
                List<RoleApiInfo> roleApiList = new List<RoleApiInfo>();
                foreach (var id in model.ApiIdList)
                {
                    // 防止重复数据
                    if (!roleApiList.Exists(it => it.ApiId == id))
                    {
                        roleApiList.Add(new RoleApiInfo
                        {
                            ApiId = id,
                            RoleId = model.Id
                        });
                    }
                }
                // 删除用户当前角色
                await _dbContext.Deleteable<RoleApiInfo>().Where(f => f.RoleId == model.Id).ExecuteCommandAsync();
                // 添加用户角色
                if (roleApiList.Count > 0)
                    await _dbContext.Insertable(roleApiList).ExecuteCommandAsync();
                #endregion

                #region 缓存更新
                await redis.KeyDeleteAsync(CacheKeys.RoleAllUseKey);
                // 应该立即更新缓存
                #endregion

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
