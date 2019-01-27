using Feng.Admin.Model;
using Feng.Basic;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Feng.Admin.IService
{
    public interface IMicroService
    {
        /// <summary>
        /// 查询服务发现列表
        /// </summary>
        /// <returns></returns>
        Task<ReturnResult<dynamic>> QueryServiceList(QueryServiceModel model);
        Task<ReturnResult> SetServiceInfo(ServiceModel model);
        Task<ReturnResult> DeleteService(DeleteServiceModel model);
        Task<ReturnResult<dynamic>> GetApiGatewayConfiguration();
        Task<ReturnResult> SetApiGatewayConfiguration();
    }
}
