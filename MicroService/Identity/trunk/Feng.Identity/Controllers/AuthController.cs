using System.Threading.Tasks;
using Feng.Basic;
using Feng.Identity.IService;
using Feng.Identity.Model.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Feng.Identity.Controllers
{
    /// <summary>
    /// 认证授权控制器
    /// </summary>
    [Produces("application/json")]
    public class AuthController : Controller
    {
        /// <summary>
        /// 业务实现
        /// </summary>
        private readonly IAuthService _authService;
        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="authService"></param>
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        /// <summary>
        /// 账户登陆 - 密码登陆
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("/Login")]
        public async Task<ReturnResult<LoginReturnModel>> Login([FromBody]LoginModel model)
        {
            return await _authService.LoginAsync(model);
        }

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("/Register")]
        public async Task<ReturnResult> RegisterAsync([FromBody]RegisterModel model)
        {
            return await _authService.RegisterAsync(model);
        }
        /// <summary>
        /// 发送短信验证码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("/SendSmsCode")]
        public async Task<ReturnResult> SendSmsCode([FromBody]SendSmsCodeModel model)
        {
            return await _authService.SendSmsCodeAsync(model);
        }


    }
}