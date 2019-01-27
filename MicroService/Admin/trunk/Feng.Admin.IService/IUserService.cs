using Feng.Admin.Model;
using Feng.Basic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Feng.Admin.IService
{
    public interface IUserService
    {
        /// <summary>
        /// 查询用户列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<ReturnResult<PageList<ReturnUserModel>>> QueryUsers(QueryUsersModel model);

        /// <summary>
        /// 添加或修改用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ReturnResult> AddOrUpdateUser(AddOrUpdateUserModel model);
    }
}
