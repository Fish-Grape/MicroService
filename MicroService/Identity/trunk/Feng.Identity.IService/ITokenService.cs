using Feng.Identity.Model.Auth;
using System.Collections.Generic;

namespace Feng.Identity.IService
{
    public interface ITokenService
    {
        /// <summary>
        /// 创建jwt token
        /// </summary>
        /// <param name="userInfo"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        string CreateAccessToken(UserTokenModel userInfo, List<string> roles);
    }
}
