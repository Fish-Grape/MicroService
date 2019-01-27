using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Feng.Basic
{

    /// <summary>
	/// 通用json返回格式
	/// </summary>
	/// <typeparam name="T"></typeparam>
	[Serializable, DataContract(Name = "result")]
    public class ReturnResult<T> : ReturnResult
    {
        public ReturnResult()
            : this(default(T)) { }

        public ReturnResult(T data)
            : this(data == null ? (int)ErrorCodeEnum.Error_NoData : (int)ErrorCodeEnum.Success, data, data != null ? "success" : "no data") { }

        public ReturnResult(string message)
            : this(string.IsNullOrWhiteSpace(message) ? (int)ErrorCodeEnum.Success : (int)ErrorCodeEnum.Failed, default(T), string.IsNullOrWhiteSpace(message) ? "success" : message) { }

        public ReturnResult(int code, T data, string message)
            : base(code, message)
        {
            this.Data = data;
        }

        /// <summary>
        /// 内容实体
        /// </summary>
        [DataMember(Name = "data")]
        public T Data { get; set; }
    }

    /// <summary>
    /// 通用json返回格式
    /// </summary>
    [Serializable, DataContract(Name = "result")]
    public class ReturnResult
    {
        public ReturnResult()
            : this((int)ErrorCodeEnum.Success, "success") { }

        public ReturnResult(string message)
            : this(string.IsNullOrWhiteSpace(message) ? (int)ErrorCodeEnum.Success : (int)ErrorCodeEnum.Failed, string.IsNullOrWhiteSpace(message) ? "success" : message) { }

        public ReturnResult(int code, string message = "error")
        {
            this.Code = code;
            this.Message = message;
        }

        /// <summary>
        /// 状态码:0成功,其他自定义
        /// </summary>
        [DataMember(Name = "code")]
        public int Code { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        [DataMember(Name = "message")]
        public string Message { get; set; }
    }

    

}
