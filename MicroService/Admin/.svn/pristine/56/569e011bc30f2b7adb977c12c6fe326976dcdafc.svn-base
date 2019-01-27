using Feng.Basic;
using System;

namespace Feng.Admin.Model
{

    public class AppModel {
        public int Id { set; get; }
        public string Name { set; get; }
        public string AppId { set; get; }
        public string Secret { set; get; }
        public string Remark { set; get; }
    }
    
    public class AppProjectModel
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string AppId { set; get; }
        public bool IsPublic { set; get; }
        public string Comment { set; get; }
        public bool IsDeleted { set; get; }
        public DateTime CreateTime { set; get; }
        public long CreateUid { set; get; }
        public DateTime LastTime { set; get; }
        public long LastUid { set; get; }
    }
    public class QueryAppProjectModel:BasePage {
        public string AppId { set; get; }
    }


    public class AppConfigModel
    {
        public int Id { set; get; }
        public string ConfigAppId { set; get; }
        public string ConfigNamespaceName { set; get; }
        public string ConfigKey { set; get; }
        public string ConfigValue { set; get; }
        public string Remark { set; get; }
        public bool IsDeleted { set; get; }
        /// <summary>
        /// 环境
        /// </summary>
        public string Environment { set; get; }
    }

    public class QueryAppConfigModel:BasePage {
        public string AppId { set; get; }
        public string NameSpace { set; get; }
        public string Environment { set; get; }
    }

}
