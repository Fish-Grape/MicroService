using Feng.Basic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Identity.Model.Auth
{
    public class RegisterModel
    {
        /// <summary>
        /// 账号
        /// </summary>
        [NotEmpty("001", "账号不能为空")]
        public string UserName { set; get; }
        /// <summary>
        /// 密码
        /// </summary>
        [NotEmpty("002", "密码不能为空")]
        public string Password { set; get; }
        /// <summary>
        /// 确认密码
        /// </summary>
        [NotEmpty("003", "确认密码不能为空")]
        public string ConfirmPassword { set; get; }
        /// <summary>
        /// 手机号码
        /// </summary>
        [NotEmpty("004", "手机号不能为空")]
        [Mobile("005","请输入正确手机号码")]
        public string Mobile { get; set; }
        /// <summary>
        /// 手机验证码
        /// </summary>
        [NotEmpty("006", "手机验证码不能为空")]
        public string MobileCode { set; get; }
    }
}
