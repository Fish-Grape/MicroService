using SqlSugar;
using System;
using System.Collections.Generic;
using System.Text;

namespace Feng.Identity.Entity
{
    [SugarTable("tb_thirdoauths")]
    public class ThirdOAuthInfo
    {
        /// <summary>
        /// uid 
        /// </summary>
        [SugarColumn(IsPrimaryKey = false, IsIdentity = false)]
        public long Uid { set; get; }
        public string OpenId { set; get; }
        public string UnionId { set; get; }
        public string AuthServer { set; get; }
        public string AppId { set; get; }
    }
}
