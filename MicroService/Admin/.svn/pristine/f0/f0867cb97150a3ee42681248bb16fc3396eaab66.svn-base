using Feng.Admin.Entity;
using Feng.Admin.IService;
using Feng.Admin.Model;
using Feng.Basic;
using Feng.Core;
using Feng.Redis;
using SqlSugar;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Feng.Admin.Service
{
    public class MenuService : IMenuService
    {
        private readonly SqlSugarClient _dbContext;
        private readonly RedisClient _redisClient;
        private readonly IJsonHelper _jsonHelper;
        private readonly IUser _user;
        public MenuService(SqlSugarClient dbContext,
            RedisClient redisClient,
            IJsonHelper jsonHelper,
            IUser user)
        {
            _dbContext = dbContext;
            _redisClient = redisClient;
            _jsonHelper = jsonHelper;
            _user = user;
        }
        /// <summary>
        /// 查询平台菜单
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ReturnResult<dynamic>> QueryAllMenus(QueryAllMenusModel model)
        {
            var list = await _dbContext.Queryable<MenuInfo>()
                                 .WhereIF(model.PlatformId > 0, it => it.PlatformId == model.PlatformId)
                                 .ToListAsync();
            return new ReturnResult<dynamic>(list);
        }
        /// <summary>
        /// 查询登陆用户拥有菜单
        /// </summary>
        /// <returns></returns>
        public async Task<ReturnResult<dynamic>> QueryUserMenus()
        {
            var currentUid = Convert.ToInt64(_user.Id);

            var list = await _dbContext.Queryable<MenuInfo, RoleMenuInfo, UserRoleInfo>((t1, t2, t3) => new object[] {
                       JoinType.Inner, t1.Id == t2.MenuId,
                       JoinType.Inner , t2.RoleId == t3.RoleId
                       })
                       .Where((t1, t2, t3) => t1.State == 1 && t3.Uid == currentUid)
                       .OrderBy((t1, t2, t3) => t1.SortId, OrderByType.Asc)
                       .GroupBy((t1, t2, t3) => t1.Id)
                       .Select((t1, t2, t3) => new MenuModel { Icon = t1.Icon, Id = t1.Id, LinkUrl = t1.LinkUrl, Name = t1.Name, ParentId = t1.ParentId, PlatformId = t1.PlatformId, SortId = t1.SortId, State = t1.State , isShow=t1.IsShow })
                       .ToListAsync();

            var platformIdList = list.GroupBy(p => p.PlatformId)
                .Select(it => it.First().PlatformId)
                .ToList();
            var pidArr = platformIdList.ToArray();
            var platformList = await _dbContext.Queryable<PlatformInfo>()
                .Where(it => it.IsDel == false && pidArr.Contains(it.Id))
                .ToListAsync();

            return new ReturnResult<dynamic>(new { Menu = list, Platform = platformList });
        }
        /// <summary>
        /// 设置平台菜单信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ReturnResult> SetPlatform(MenuModel model)
        {
            var item = new MenuInfo() {
                Id = model.Id,
                Name = model.Name,
                Icon = model.Icon,
                LinkUrl = model.LinkUrl,
                PlatformId = model.PlatformId,
                ParentId = model.ParentId,
                SortId = model.SortId,
                State = model.State,
                IsShow = model.isShow
            };
            if (item.Id > 0)
            {
                await _dbContext.Updateable(item).ExecuteCommandAsync();
            }
            else
            {
                await _dbContext.Insertable(item).ExecuteCommandAsync();
            }
            return new ReturnResult();
        }
    }
}
