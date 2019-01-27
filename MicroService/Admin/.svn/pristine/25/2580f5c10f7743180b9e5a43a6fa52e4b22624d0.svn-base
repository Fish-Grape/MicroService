using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Feng.Admin.IService;
using Feng.Admin.Model;
using Feng.Basic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Feng.Admin.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [Produces("application/json")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userService"></param>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// 查询用户列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize("permission")]
        [HttpGet("/User/QueryUsers")]
        public async Task<ReturnResult<PageList<ReturnUserModel>>> QueryUsers([FromQuery]QueryUsersModel model)
        {
            return await _userService.QueryUsers(model);
        }

        /// <summary>
        /// 设置用户信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize("permission")]
        [HttpPost("/User/SetUser")]
        public async Task<ReturnResult> AddOrUpdateUser([FromBody]AddOrUpdateUserModel model)
        {
            return await _userService.AddOrUpdateUser(model);
        }

    }
}