using Feng.Basic;
using Feng.Core.Config;
using Feng.Core.Exceptions;
using Feng.DbContext;
using Feng.Identity.Entity;
using Feng.Identity.IService;
using Feng.Identity.Model.Auth;
using Feng.Redis;
using Feng.Utils;
using Feng.Utils.Helpers;
using Microsoft.Extensions.Logging;
using SqlSugar;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Feng.Identity.Service
{
    public class AuthService : IAuthService
    {
        private readonly ITokenService _tokenService;
        private readonly IConfig _config;
        private readonly ILogger<AuthService> _logger;

        private readonly IDbRepository<UserInfo> _dbContext;
        private readonly RedisClient _redisClient;


        public AuthService(
            ITokenService tokenService,
            IConfig config, 
            ILogger<AuthService> logger,
            IDbRepository<UserInfo> dbContext,
            RedisClient redisClient) {
            _config = config;
            _logger = logger;
            _dbContext = dbContext;
            _redisClient = redisClient;
            _tokenService = tokenService;
        }



        public async Task<ReturnResult<LoginReturnModel>> LoginAsync(LoginModel model)
        {
            var rediscontect = _config.StringGet("RedisDefaultServer");
            var redis = _redisClient.GetDatabase(rediscontect, 5);
            var kv = await redis.StringGetAsync($"ImgCode:{model.Guid}");
            if (kv.IsNullOrEmpty || kv.ToString().ToLower() != model.ImgCode.ToLower())
                throw new FengException("GO_2003", "图形验证码错误");

            // 用户验证
            var userInfo = await _dbContext.GetFirstAsync(it => it.UserName == model.UserName);
            if (userInfo == null)
                throw new FengException("GO_4007", "账号不存在");
            if (userInfo.State != 1)
                throw new FengException("GO_4008", "账号状态异常");
            if (userInfo.Password != Encrypt.SHA256(model.Password + userInfo.Salt))
                throw new FengException("GO_4009", "账号或密码错误");

            _logger.LogInformation($"用户[{userInfo.RealName}]使用账号：[{model.UserName}]在[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}]登录成功!");

            // 用户角色
            var roleList = await _dbContext.DbContext.Queryable<RoleInfo, UserRoleInfo>((role, urole) => new object[] { JoinType.Inner, role.Id == urole.RoleId })
                 .Where((role, urole) => urole.Uid == userInfo.Id)
                 .Where((role, urole) => role.IsDel == false)
                 .Select((role, urole) => new { role.Key })
                 .ToListAsync();

            // token返回
            var token = _tokenService.CreateAccessToken(new UserTokenModel
            {
                Email = userInfo.Email,
                Id = userInfo.Id,
                Mobile = userInfo.Mobile,
                RealName = userInfo.RealName
            }, roleList.Select(it => it.Key).ToList());



            return new ReturnResult<LoginReturnModel>(new LoginReturnModel() {
                Data = new
                {
                    AccessToken = $"Bearer {token}",
                    Expire = DateTimeOffset.Now.AddHours(4).ToUnixTimeSeconds(),
                    RealName = userInfo.RealName.SafeString(),
                    Mobile = userInfo.Mobile.SafeString(),
                    userInfo.Id
                }
            });
            
        }

        public async Task<ReturnResult> RegisterAsync(RegisterModel model)
        {
            // 验证短信验证码
            await VerifySmsCodeAsync(model.Mobile, model.MobileCode);

            //用户角色，
            
            //_dbContext.InsertAsync(new UserInfo() {  });

            return new ReturnResult();

        }

        public async Task<ReturnResult> SendSmsCodeAsync(SendSmsCodeModel model)
        {
            await SendSmsCodeAsync(model.Mobile, "");

            return new ReturnResult();
        }

        /// <summary>
        /// 短息发送
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="smsTemplateName"></param>
        private async Task SendSmsCodeAsync(string mobile, string smsTemplateName)
        {
            var errCountKey = string.Format(CacheKeys.SmsCodeVerifyErr, mobile);
            var sendCountKey = string.Format(CacheKeys.SmsCodeSendIdentity, mobile);
            var regCodeKey = string.Format(CacheKeys.SmsCodeRegisterCode, mobile);

            var rediscontect = _config.StringGet("RedisDefaultServer");
            var redis = _redisClient.GetDatabase(rediscontect, 5);

            // 错误次数过多
            var errCount = await redis.StringGetAsync(errCountKey);
            if (!errCount.IsNullOrEmpty && errCount.SafeString().ToInt() > 5)
                throw new FengException("Feng_0005", "注册错误次数过多，请30分钟后再试");

            // 验证一分钟发一条
            if (await redis.KeyExistsAsync(sendCountKey))
                throw new FengException("Feng_2001", "一分钟只能发送一条短信");

            // 生成短信验证码
            string smsCode = Randoms.CreateRandomValue(6, true);
            // 发送短信
            //Dictionary<string, object> dic = new Dictionary<string, object>
            //{
            //    { "SmsCode", loginCode }
            //};
            // 短发推送
            //_eventBus.Publish(new SmsEvent(dic)
            //{
            //    ChannelType = 0,
            //    MobIp = Web.Ip,
            //    Sender = "6b4b881169e144da9ae93113c0ca41d4",
            //    SmsTemplateId = 2,
            //    SmsTemplateName = smsTemplateName,
            //    Source = "用户注册项目",
            //    Mob = mobile,
            //});
            

            // 验证码缓存
            await redis.StringSetAsync(regCodeKey, smsCode, new TimeSpan(0, 0, 0, 300));
            // 发送人缓存(60秒发一次)
            await redis.StringSetAsync(sendCountKey, smsCode, new TimeSpan(0, 0, 0, 60));
        }
        /// <summary>
        /// 验证短信验证码
        /// </summary>
        /// <param name="mobile"></param>
        /// <param name="smsCode"></param>
        /// <returns></returns>
        public async Task VerifySmsCodeAsync(string mobile, string smsCode)
        {
            
            var errCountKey = string.Format(CacheKeys.SmsCodeVerifyErr, mobile);
            var regCodeKey = string.Format(CacheKeys.SmsCodeRegisterCode, mobile);

            var rediscontect = _config.StringGet("RedisDefaultServer");
            var redis = _redisClient.GetDatabase(rediscontect, 5);

            // 错误次数判断
            var errCount = await redis.StringGetAsync(errCountKey);
            if (!errCount.IsNullOrEmpty && errCount.SafeString().ToInt() > 5)
                throw new FengException("Feng_0005", "登陆错误次数过多，请30分钟后再试");

            // 短信验证码验证
            var verifyValue = await redis.StringGetAsync(regCodeKey);
            if (string.IsNullOrWhiteSpace(verifyValue))
                throw new FengException("Feng_5014", "验证码不存在");

            if (verifyValue.SafeString().ToLower() != smsCode.SafeString().ToLower())
            {
                // 记录错误次数,30分钟
                await redis.StringSetAsync(errCountKey, (errCount.IsNullOrEmpty ? 1 : errCount.SafeString().ToInt() + 1), new TimeSpan(0, 30, 0));
                // 抛出异常
                throw new FengException("GO_5015", "验证码错误");
            }
            // 清除次数
            await redis.KeyDeleteAsync(errCountKey);
            await redis.KeyDeleteAsync(regCodeKey);
        }
    }
}
