
namespace Feng.Admin.Model
{
    public class ServiceModel
    {
        public string Name { set; get; }
        public string Version { set; get; }
        public string[] Tags { set; get; }
        public HostAndPortDto HostAndPort { set; get; }
        public string HealthCheckUri { set; get; }
    }

    public class HostAndPortDto
    {
        public string Host { set; get; }
        public string Port { set; get; }
    }

    public class QueryServiceModel {
        /// <summary>
        /// 状态
        /// </summary>
        public int State { set; get; }
        /// <summary>
        /// 服务名称
        /// </summary>
        public string Name { set; get; }
    }

    public class DeleteServiceModel {
        public string ServiceId { set; get; }
    }
}
