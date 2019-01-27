using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Files.Model
{
    /// <summary>
    /// 上传模型
    /// </summary>
    public class UploadModel
    {
        public string AppId { get; set; }
        
    }

    /// <summary>
    /// 上传后返回模型
    /// </summary>
    public class UploadReturnModel
    {
        public string AppId { get; set; }

        public List<UploadFiles> files = null;
    }

    public class UploadFiles {
        /// <summary>
        /// etag
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 上传的key:name
        /// </summary>
        public string key { get; set; }
    }

}
