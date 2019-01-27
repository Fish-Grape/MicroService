using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Files.Entity
{
    public class domain_file
    {
        /// <summary>
        /// Desc:编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public object id { get; set; }

        /// <summary>
        /// Desc:关联归属编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public object domainid { get; set; }

        /// <summary>
        /// Desc:关联空间编号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public object bucketid { get; set; }

        /// <summary>
        /// Desc:关联文件编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public object fileid { get; set; }
        /// <summary>
        /// Desc:原始文件名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string name { get; set; }

        /// <summary>
        /// Desc:扩展名
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string ext { get; set; }
        /// <summary>
        /// Desc:类型
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int cat { get; set; }
        /// <summary>
        /// Desc:访问数
        /// Default:0
        /// Nullable:False
        /// </summary>           
        public int visit { get; set; }

        /// <summary>
        /// Desc:添加日期
        /// Default:CURRENT_TIMESTAMP
        /// Nullable:False
        /// </summary>           
        public DateTime addtime { get; set; }
    }
}
