using Feng.Admin.Entity;
using Feng.Admin.IService;
using Feng.Admin.Model;
using Feng.Basic;
using Feng.Core;
using Feng.Redis;
using Feng.Utils;
using Feng.Utils.Helpers;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feng.Admin.Service
{
    public class UserService : IUserService
    {
        /// <summary>
        /// 数据库操作
        /// </summary>
        private readonly SqlSugarClient _dbContext;
        private readonly RedisClient _redisClient;
        private readonly IJsonHelper _jsonHelper;
        private readonly IUser _user;
        private readonly IRoleService _roleService;

        public UserService(
            SqlSugarClient dbContext,
            RedisClient redisClient,
            IJsonHelper jsonHelper,
            IUser user,
            IRoleService roleService) {
            _dbContext = dbContext;
            _redisClient = redisClient;
            _jsonHelper = jsonHelper;
            _user = user;
            _roleService = roleService;
        }

        /// <summary>
        /// 添加修改用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ReturnResult> AddOrUpdateUser(AddOrUpdateUserModel model)
        {
            try
            {
                _dbContext.Ado.BeginTran();

                var item = new UserInfo()
                {
                    Id = model.Id,
                    UserName = model.UserName,
                    Password = model.Password,
                    RealName = model.RealName,
                    Mobile = model.Mobile,
                    Email = model.Email,
                    Salt = model.Salt,
                    State = model.State,
                    UpdateTime = model.UpdateTime,
                    CreateTime = model.CreateTime
                };
                if (item.Id > 0)
                {
                    item.UpdateTime = DateTime.Now;
                    if (!item.Password.IsEmpty())
                    {
                        item.Salt = Randoms.CreateRandomValue(8, false);
                        item.Password = Encrypt.SHA256(item.Password + item.Salt);
                        // 基础字段不容许更新
                        await _dbContext.Updateable(item)
                                        .IgnoreColumns(it => new { it.UserName, it.Mobile, it.CreateTime })
                                        .ExecuteCommandAsync();
                    }
                    else
                    {
                        // 基础字段不容许更新
                        await _dbContext.Updateable(item)
                                        .IgnoreColumns(it => new { it.UserName, it.Password, it.Salt, it.Mobile, it.CreateTime })
                                        .ExecuteCommandAsync();
                    }
                }
                else
                {
                    item.CreateTime = DateTime.Now;
                    item.UpdateTime = DateTime.Now;
                    item.Salt = Randoms.CreateRandomValue(8, false);
                    item.Password = Encrypt.SHA256(item.Password + item.Salt);
                    item.Id = Convert.ToInt64($"{Time.GetUnixTimestamp()}{ Randoms.CreateRandomValue(3, true) }");
                    await _dbContext.Insertable(item).ExecuteCommandAsync();
                }
                // 用户角色操作
                List<UserRoleInfo> userRoleList = new List<UserRoleInfo>();
                foreach (var id in model.RoleIdList)
                {
                    // 防止重复数据
                    if (!userRoleList.Exists(it => it.RoleId == id))
                    {
                        userRoleList.Add(new UserRoleInfo
                        {
                            Uid = item.Id,
                            RoleId = id
                        });
                    }
                }
                // 删除用户当前角色
                await _dbContext.Deleteable<UserRoleInfo>().Where(f => f.Uid == item.Id).ExecuteCommandAsync();
                // 添加用户角色
                if (userRoleList.Count > 0)
                    await _dbContext.Insertable(userRoleList).ExecuteCommandAsync();

                _dbContext.Ado.CommitTran();
            }
            catch(Exception ex) {
                _dbContext.Ado.RollbackTran();
                throw new Exception("用户事务执行失败", ex);
            }
            return new ReturnResult();
        }

        /// <summary>
        /// 查询用户列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ReturnResult<PageList<ReturnUserModel>>> QueryUsers(QueryUsersModel model)
        {
            var result = new PageList<ReturnUserModel>();
            var list = new List<ReturnUserModel>();
            var totalNumber = 0;
            if (model.RoleId > 0)
            {
                var query = await _dbContext.Queryable<UserInfo, UserRoleInfo>((u, urole) => new object[] { JoinType.Inner, u.Id == urole.Uid })
                .Where((u, urole) => urole.RoleId == model.RoleId)
                .WhereIF(model.State > -1, (u, urole) => u.State == model.State)
                .WhereIF(!model.RealName.IsEmpty(), (u, urole) => u.RealName == model.RealName)
                .WhereIF(!model.UserName.IsEmpty(), (u, urole) => u.UserName == model.UserName)
                .WhereIF(!model.Mobile.IsEmpty(), (u, urole) => u.Mobile == model.Mobile)
                .Select((u, urole) => new ReturnUserModel { Id = u.Id, Mobile = u.Mobile, RealName = u.RealName, State = u.State, UpdateTime = u.UpdateTime, UserName = u.UserName, Email = u.Email })
                .ToPageListAsync(model.Page, model.PageSize, totalNumber);
                list = query.Key;
                totalNumber = query.Value;
            }
            else if (!model.PlatformKey.IsEmpty())
            {
                // 项目角色Id数组
                var prlist = _dbContext.Queryable<RoleInfo>().Where(it => it.PlatformKey == model.PlatformKey && it.IsDel == false).Select(it => new { it.Id }).ToList();
                var roleIdArr = prlist.Select(it => it.Id).ToArray();
                // 查询
                var query = await _dbContext.Queryable<UserInfo, UserRoleInfo>((u, urole) => new object[] { JoinType.Inner, u.Id == urole.Uid })
                .Where((u, urole) => roleIdArr.Contains(urole.RoleId))
                .WhereIF(model.State > -1, (u, urole) => u.State == model.State)
                .WhereIF(!model.RealName.IsEmpty(), (u, urole) => u.RealName == model.RealName)
                .WhereIF(!model.UserName.IsEmpty(), (u, urole) => u.UserName == model.UserName)
                .WhereIF(!model.Mobile.IsEmpty(), (u, urole) => u.Mobile == model.Mobile)
                .GroupBy((u, urole) => u.Id)
                .Select((u, urole) => new ReturnUserModel { Id = u.Id, Mobile = u.Mobile, RealName = u.RealName, State = u.State, UpdateTime = u.UpdateTime, UserName = u.UserName, Email = u.Email })
                .ToPageListAsync(model.Page, model.PageSize, totalNumber);
                list = query.Key;
                totalNumber = query.Value;
            }
            else
            {
                var query = await _dbContext.Queryable<UserInfo>()
                .WhereIF(model.State > -1, f => f.State == model.State)
                .WhereIF(!model.RealName.IsEmpty(), f => f.RealName == model.RealName)
                .WhereIF(!model.UserName.IsEmpty(), f => f.UserName == model.UserName)
                .WhereIF(!model.Mobile.IsEmpty(), f => f.Mobile == model.Mobile)
                .Select(u => new ReturnUserModel { Id = u.Id, Mobile = u.Mobile, RealName = u.RealName, State = u.State, UpdateTime = u.UpdateTime, UserName = u.UserName, Email = u.Email })
                .ToPageListAsync(model.Page, model.PageSize, totalNumber);
                list = query.Key;
                totalNumber = query.Value;
            }
            var canUseRoleList = await _roleService.QueryRoles(new QueryRolesByPlatformModel());
            var canUseRole = canUseRoleList.Data as List<RoleInfo>;
            result.Page = model.Page;
            result.PageSize = model.PageSize;
            result.TotalCount = totalNumber;
            result.DataList = list;
            result.DataList.ForEach(m =>
            {
                var useRole = _dbContext.Queryable<UserRoleInfo>()
                             .Where(it => it.Uid == m.Id)
                             .Select(it => new { Id = it.RoleId })
                             .ToList();
                var idList = useRole.GroupBy(p => p.Id).Select(it => it.First().Id).ToList();
                m.RoleList = canUseRole.Where(it => idList.Contains(it.Id)).Select(it => new { Id = it.Id, ProjectName = it.PlatformKey, Name = it.Name }).ToList();
            });

            return new ReturnResult<PageList<ReturnUserModel>>(result);
        }
    }
}
