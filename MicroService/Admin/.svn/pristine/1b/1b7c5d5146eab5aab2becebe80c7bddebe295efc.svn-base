using SqlSugar;

namespace Feng.Admin.Entity
{
    /// <summary>
    /// 项目实体类
    /// </summary>
    [SugarTable("tb_projects")]
    public class ProjectInfo
    {
        /// <summary>
        /// 编号
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { set; get; }
        /// <summary>
        /// 项目Key
        /// </summary>
        public string Key { set; get; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { set; get; }
        
        /// <summary>
        /// 路由前缀
        /// </summary>
        public string RouteKey { set; get; }
    }
}
