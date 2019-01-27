using Feng.Basic;
using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Identity.Model.Auth
{
    public class SendSmsCodeModel
    {
        /// <summary>
        /// 账号
        /// </summary>
        [NotEmpty("005", "手机号不能为空")]
        [Mobile("006", "请输入有效手机号")]
        public string Mobile { set; get; }
        ///// <summary>
        ///// 短信模板名称
        ///// </summary>
        //public string SmsTemplateName { get; set; } = "smscode";
        /// <summary>
        /// 随机
        /// </summary>
        public string Guid { get; set; }
        /// <summary>
        /// 图形信息
        /// </summary>
        public string ImgCode { get; set; }
    }
}
