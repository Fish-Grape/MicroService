using System;

namespace Feng.Files.Entity
{
    public class domain
    {
        /// <summary>
        /// Desc:编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public object id { get; set; }

        /// <summary>
        /// Desc:空间总数量
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int bucketcount { get; set; }

        /// <summary>
        /// Desc:空间总大小
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public long bucketsize { get; set; }

        /// <summary>
        /// Desc:已用空间
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int existbucket { get; set; }

        /// <summary>
        /// Desc:已用大小
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public long existsize { get; set; }

        /// <summary>
        /// Desc:已存在文件数量
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int existcount { get; set; }

        /// <summary>
        /// Desc:添加时间
        /// Default:CURRENT_TIMESTAMP
        /// Nullable:False
        /// </summary>           
        public DateTime addtime { get; set; }

        /// <summary>
        /// Desc:状态(0,1)
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int status { get; set; }
    }
}
