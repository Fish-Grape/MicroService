using Feng.Basic;
using Feng.DbContext;
using Feng.Identity.Entity;
using Feng.Identity.IService.AuthManager;
using Feng.Identity.Model.AuthManager;
using Microsoft.Extensions.Logging;
using SqlSugar;

namespace Feng.Identity.Service.AuthManager
{
    public class UserService : IUserService
    {
        private readonly ILogger<AuthService> _logger;

        private readonly IDbRepository<UserInfo> _userContext;
        

        public UserService(ILogger<AuthService> logger, 
            IDbRepository<UserInfo> userContext) {
            _logger = logger;
            _userContext = userContext;
        }

        public ReturnResult<PageList<UserModel>> GetUserList(string platKey, int pageIndex, int pageSize, string account, string username, int status)
        {
            int totalNumber = 0;

            var list = _userContext.DbContext.Queryable<RoleInfo, UserRoleInfo, UserInfo>((a, b, c) => new object[] {
                             JoinType.Left,a.Id==b.RoleId,
                             JoinType.Left,b.Uid==c.Id
                        })
                        .Where(a => a.PlatformKey == platKey)
                        .WhereIF(!string.IsNullOrEmpty(account), (a, b, c) => c.UserName.Contains(account) || c.Mobile.Contains(account) || c.Email.Contains(account))
                        .WhereIF(status >= 0, (a, b, c) => c.State == status)
                        .Select((a, b, c) => new UserModel
                        {
                            UserId = c.Id,
                            Account = c.UserName,
                            UserName = c.RealName,
                            Email = c.Email,
                            Mobile = c.Mobile,
                            RoleName = a.Name,
                            RegTime = c.CreateTime,
                            State = c.State
                        })
                        .ToPageList(pageIndex, pageSize, ref totalNumber);

            if (list != null && list.Count > 0) 
                return new ReturnResult<PageList<UserModel>>(new PageList<UserModel>() { DataList = list, Page = pageIndex, PageSize = pageSize, TotalCount = totalNumber });

            return new ReturnResult<PageList<UserModel>>((int)ErrorCodeEnum.Error_NoData, null, "没有找到数据");
        }

        public ReturnResult ModifyStatus(ModifyUserStatus model)
        {
            if (model == null)
                return new ReturnResult((int)ErrorCodeEnum.Parameter_Missing, "参数异常");

            bool flag = _userContext.Update(u=>new UserInfo{ State = model.status }, x => x.Id == model.id);

            if (flag)
                return new ReturnResult();


            return new ReturnResult((int)ErrorCodeEnum.Error_SqlData, "修改状态失败");
        }
    }
}
