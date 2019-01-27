using Feng.Basic;
using Feng.Identity.Model.Auth;
using System.Threading.Tasks;

namespace Feng.Identity.IService
{
    public interface IAuthService
    {
        Task<ReturnResult<LoginReturnModel>> LoginAsync(LoginModel model);

        Task<ReturnResult> RegisterAsync(RegisterModel model);

        Task<ReturnResult> SendSmsCodeAsync(SendSmsCodeModel model);
    }
}
