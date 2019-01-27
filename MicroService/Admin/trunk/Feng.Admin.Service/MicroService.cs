using Feng.Admin.Entity;
using Feng.Admin.IService;
using Feng.Admin.Model;
using Feng.Basic;
using Feng.Core;
using Feng.Core.Config;
using Feng.Core.ServiceDiscovery;
using Feng.DbContext;
using Feng.Utils.Helpers;
using Microsoft.AspNetCore.Http.Internal;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Feng.Admin.Service
{
    public class MicroService : IMicroService
    {
        private readonly IServiceDiscovery _serviceDiscovery;
        private readonly IRepositoryBase<ApiGatewayConfigurationInfo> _apigatewayRepository;
        private readonly IConfig _config;

        public MicroService(
            IServiceDiscovery serviceDiscovery, 
            IConfig config,
            IRepositoryBase<ApiGatewayConfigurationInfo> apigatewayRepository)
        {
            _serviceDiscovery = serviceDiscovery;
            _config = config;
            _apigatewayRepository = apigatewayRepository;
        }
        /// <summary>
        /// 服务移除
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ReturnResult> DeleteService(DeleteServiceModel model)
        {
            await _serviceDiscovery.DeregisterServiceAsync(model.ServiceId);
            return new ReturnResult();
        }
        /// <summary>
        /// 查询ApiGatewayConfiguration
        /// </summary>
        /// <returns></returns>
        public async Task<ReturnResult<dynamic>> GetApiGatewayConfiguration()
        {
            var key = _config.StringGet("ApiGatewayConfigurationKey");
            var value = await _serviceDiscovery.KeyValueGetAsync(key);
            return new ReturnResult<dynamic>(JObject.Parse(value));
        }
        /// <summary>
        /// 查询服务发现服务列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ReturnResult<dynamic>> QueryServiceList(QueryServiceModel model)
        {
            var result = new object();
            if (model.State == 0)
            {
                result = await _serviceDiscovery.FindAllServicesAsync();
            }
            if (model.State == 1)
            {
                if (string.IsNullOrWhiteSpace(model.Name))
                {
                    result = await _serviceDiscovery.FindServiceInstancesAsync();
                }
                else
                {
                    result = await _serviceDiscovery.FindServiceInstancesAsync(model.Name);
                }
            }
            return new ReturnResult<dynamic>(result);
        }

        /// <summary>
        /// 设置ApiGatewayConfiguration
        /// </summary>
        /// <returns></returns>
        public async Task<ReturnResult> SetApiGatewayConfiguration()
        {
            try
            {
                _apigatewayRepository.DbContext.Ado.BeginTran();

                #region body
                var req = Web.HttpContext.Request;
                req.EnableRewind();
                var originBody = req.Body;
                req.Body.Position = 0;
                var bodyStr = await new StreamReader(req.Body).ReadToEndAsync();
                req.Body.Position = 0;
                req.Body = originBody;
                #endregion

                var key = _config.StringGet("ApiGatewayConfigurationKey");

                var config = await _apigatewayRepository.GetFirstAsync(it => it.ConfigurationKey == key);
                if (config == null)
                    await _apigatewayRepository.InsertAsync(new ApiGatewayConfigurationInfo { ConfigurationKey = key, Configuration = bodyStr });
                else
                    await _apigatewayRepository.UpdateAsync(new ApiGatewayConfigurationInfo { Id = config.Id, ConfigurationKey = config.ConfigurationKey, Configuration = bodyStr });

                await _serviceDiscovery.KeyValuePutAsync(key, bodyStr);

                _apigatewayRepository.DbContext.Ado.CommitTran();
            }
            catch (Exception ex)
            {
                _apigatewayRepository.DbContext.Ado.RollbackTran();
                throw new Exception("网关配置更新失败", ex);
            }

            return new ReturnResult();
        }
        /// <summary>
        /// 服务注册
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ReturnResult> SetServiceInfo(ServiceModel model)
        {
            var result = await _serviceDiscovery.RegisterServiceAsync(serviceName: model.Name,
                version: model.Version,
                uri: new Uri($"http://{model.HostAndPort.Host}:{model.HostAndPort.Port}"),
                healthCheckUri: new Uri(model.HealthCheckUri),
                tags: model.Tags);
            return new ReturnResult();
        }
    }
}
