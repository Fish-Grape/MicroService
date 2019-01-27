using Feng.Basic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Identity.Model.Auth
{
    public class LoginModel
    {
        /// <summary>
        /// 账号
        /// </summary>
        [NotEmpty("001", "账号不能为空")]
        public string UserName { set; get; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password { set; get; }
        /// <summary>
        /// 随机
        /// </summary>
        public string Guid { get; set; }
        /// <summary>
        /// 图形验证码
        /// </summary>
        public string ImgCode { get; set; }
    }

    public class LoginReturnModel {
        public dynamic Data { set; get; }
    }
}
