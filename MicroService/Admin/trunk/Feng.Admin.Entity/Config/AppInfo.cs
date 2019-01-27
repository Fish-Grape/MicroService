using SqlSugar;

namespace Feng.Admin.Entity
{
    [SugarTable("tb_app")]
    public class AppInfo
    {
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { set; get; }
        public string Name { set; get; }
        public string AppId { set; get; }
        public string Secret { set; get; }
        public string Remark { set; get; }
    }
}
