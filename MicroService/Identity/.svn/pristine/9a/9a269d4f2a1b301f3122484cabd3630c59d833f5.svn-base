using Feng.Authorize;
using Feng.Core.Config;
using Feng.Identity.IService;
using Feng.Identity.Model.Auth;
using Feng.Utils;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Feng.Identity.Service
{
    public class TokenService : ITokenService
    {
        private readonly IConfig _config;
        private readonly ITokenBuilder _tokenBuilder;

        public TokenService(IConfig config, ITokenBuilder tokenBuilder)
        {
            _config = config;
            _tokenBuilder = tokenBuilder;
        }

        public string CreateAccessToken(UserTokenModel userInfo, List<string> roles)
        {
            // 用户基本信息
            var claims = new List<Claim> {
                new Claim("Uid", userInfo.Id.ToString()),
                new Claim("Name", userInfo.RealName.SafeString()),
                new Claim("MobilePhone", userInfo.Mobile.SafeString()),
                new Claim("Email", userInfo.Email.SafeString())
            };
            // 角色数据
            foreach (var info in roles)
            {
                claims.Add(new Claim("scope", info));
            }
            var expires = _config.StringGet("TokenExpires", "4");
            var token = _tokenBuilder.BuildJwtToken(claims, DateTime.UtcNow, DateTime.Now.AddHours(Convert.ToInt32(expires)));
            // accessToken
            return token.TokenValue;
        }
    }
}
