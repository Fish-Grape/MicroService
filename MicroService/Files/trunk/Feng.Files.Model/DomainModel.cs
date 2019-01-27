using System;

namespace Feng.Files.Model
{
    public class DomainModel:OpenModel
    {
        /// <summary>
        /// 已用空间数目
        /// </summary>
        public int existBucket { get; set; }
        /// <summary>
        /// 已用大小
        /// </summary>
        public long existSize { get; set; }

        /// <summary>
        /// 文件总数量
        /// </summary>
        public int existCount { get; set; }

    }

    /// <summary>
    /// 开通存储空间模型
    /// </summary>
    public class OpenModel
    {
        /// <summary>
        /// 空间总数目
        /// </summary>
        public int bucketCount { get; set; }

        /// <summary>
        /// 总空间大小
        /// </summary>
        public long bucketSize { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int status { get; set; }

    }

    public class ReturnOpenModel : OpenModel {
        public string AppId { get; set; }
    }

}
