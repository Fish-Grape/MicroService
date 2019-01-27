using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Feng.Basic
{
    public class BasePage
    {
        public BasePage()
        {
            this.Page = 1;
            this.PageSize = 20;
        }
        /// <summary>
        /// 当前分页
        /// </summary>
        public int Page { set; get; }
        /// <summary>
        /// 每页数量
        /// </summary>
        public int PageSize { set; get; }
    }

    [Serializable, DataContract(Name = "page_list")]
    public class PageList<T>
    {
        public PageList()
        {
            this.DataList = new List<T>();
        }
        /// <summary>
        /// 记录总条数
        /// </summary>
        [DataMember(Name = "total_count")]
        public int TotalCount { get; set; }

        /// <summary>
        /// 当前页码
        /// </summary>
        [DataMember(Name = "page")]
        public int Page { get; set; }

        /// <summary>
        /// 每页数量
        /// </summary>
        [DataMember(Name = "page_size")]
        public int PageSize { get; set; }

        /// <summary>
        /// 分页数据
        /// </summary>
        [DataMember(Name = "data_list")]
        public List<T> DataList { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        [DataMember(Name = "page_count")]
        public int PageCount
        {
            get
            {
                return (int)Math.Ceiling(TotalCount * 1.0 / this.PageSize);
            }
        }
    }

}
