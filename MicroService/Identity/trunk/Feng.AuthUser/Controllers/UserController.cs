using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Feng.Basic;
using Feng.Core.Config;
using Feng.Identity.IService.AuthManager;
using Feng.Identity.Model.AuthManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Feng.AuthUser.Controllers
{
    /// <summary>
    /// 认证用户
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    public class UserController : ControllerBase
    {
        public readonly IUserService _userService;
        public IConfiguration _config { get; }

        public UserController(IUserService userService, IConfiguration config)
        {
            _userService = userService;
            _config = config;
        }
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="account"></param>
        /// <param name="username"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet,Route("list")]
        [Authorize("permission")]
        public ReturnResult<PageList<UserModel>> GetUserList(int pageIndex, int pageSize, string account="", string username="", int status=-1) {
            if (!ValidPlat())
                return new ReturnResult<PageList<UserModel>>((int)ErrorCodeEnum.Error_Data_State, null, "请勿非法请求");

            return _userService.GetUserList(Utils.Extensions.Plat, pageIndex, pageSize, account, username, status);
        }
        /// <summary>
        /// 修改用户状态
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost,Route("status")]
        [Authorize("permission")]
        public ReturnResult ModifyStatus([FromBody]ModifyUserStatus model) {
            if (!ValidPlat())
                return new ReturnResult((int)ErrorCodeEnum.Error_Data_State, "请勿非法请求");

            return _userService.ModifyStatus(model);
        }

        private bool ValidPlat() {
            if (string.IsNullOrEmpty(Utils.Extensions.Plat))
                return false;

            if (Utils.Extensions.Plat != _config["JwtAuthorize:PlatKey"])
                return false;

            return true;
        }

    }
}