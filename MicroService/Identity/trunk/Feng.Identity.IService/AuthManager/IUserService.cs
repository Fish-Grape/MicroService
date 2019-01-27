using Feng.Basic;
using Feng.Identity.Model.AuthManager;

namespace Feng.Identity.IService.AuthManager
{
    public interface IUserService
    {
        ReturnResult<PageList<UserModel>> GetUserList(string platKey,int pageIndex,int pageSize,string account,string username,int status);

        ReturnResult ModifyStatus(ModifyUserStatus model);
    }
}
