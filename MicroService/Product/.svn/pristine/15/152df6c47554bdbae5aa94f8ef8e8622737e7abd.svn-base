using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Feng.Authorize;
using Feng.Authorize.MySql;
using Feng.Basic.Extensions;
using Feng.Basic.Filters;
using Feng.Config.Extensions;
using Feng.DbContext;
using Feng.EventBus.Extensions;
using Feng.EventBus.RabbitMQ.Extensions;
using Feng.LoadBalancer.Extensions;
using Feng.Logging;
using Feng.Logging.Event;
using Feng.ServiceDiscovery.Consul;
using Feng.ServiceDiscovery.Extensions;
using Feng.Swagger;
using Feng.Tracing.Events;
using Feng.Tracing.Extensions;
using Feng.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using SqlSugar;
using Swashbuckle.AspNetCore.Swagger;

namespace Feng.Product
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// AutofacDI容器
        /// </summary>
        public IContainer AutofacContainer { get; private set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //添加认证+MySql权限认证
            services.AddApiJwtAuthorize(Configuration).UseAuthoriser(services, Configuration).UseMySqlAuthorize();
            // 添加基础设施服务
            services.AddFeng();

            // 添加数据ORM
            services.AddSQLSugarClient<SqlSugarClient>(config => {
                config.ConnectionString = Configuration.GetSection("SqlSugarClient")["ConnectionString"];
                config.DbType = DbType.MySql;
                config.IsAutoCloseConnection = false;
                config.InitKeyType = InitKeyType.Attribute;
            });
            //// 添加错误码服务
            //services.AddErrorCodeService(Configuration);
            // 添加配置服务
            services.AddConfigService(Configuration);

            // 添加事件驱动
            services.AddEventBus(option => { option.UseRabbitMQ(Configuration); });
            // 添加服务发现
            services.AddServiceDiscovery(option => { option.UseConsul(Configuration); });
            // 添加服务路由
            services.AddLoadBalancer();

            // 添加事件队列日志
            services.AddEventLog();
            // 添加链路追踪
            services.AddTracer(Configuration);
            services.AddEventTrace();

            // 添加过滤器
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(WebApiTracingFilterAttribute));
                options.Filters.Add(typeof(WebApiActionFilterAttribute));
            }).AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });
            // 添加接口文档+版本控制
            services.AddFengSwagger(Configuration);
            
            services.AddUtil();
            // 添加HttpClient管理
            services.AddHttpClient();
            // 添加autofac容器替换，默认容器注册方式缺少功能
            var autofac_builder = new ContainerBuilder();
            autofac_builder.Populate(services);
            autofac_builder.RegisterModule<AutofacModuleRegister>();
            AutofacContainer = autofac_builder.Build();
            return new AutofacServiceProvider(AutofacContainer);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory, IHostingEnvironment env, IApplicationLifetime appLifetime)
        {
            // 日志
            loggerFactory.AddFengLog(app, "Feng.Product.Api");
            // 文档
            app.UseFengSwagger(Configuration);
            // 公共配置
            CommonConfig(app);
            //依赖注入
            appLifetime.ApplicationStopped.Register(() => { AutofacContainer.Dispose(); });
        }

        /// <summary>
        /// 公共配置
        /// </summary>
        private void CommonConfig(IApplicationBuilder app)
        {
            // 全局错误日志
            app.UseErrorLog();
            // 认证授权
            app.UseAuthentication();
            // 静态文件
            app.UseStaticFiles();
            // 路由
            ConfigRoute(app);
            // 服务注册
            app.UseConsulRegisterService(Configuration);
        }
        /// <summary>
        /// 路由配置,支持区域
        /// </summary>
        private void ConfigRoute(IApplicationBuilder app)
        {
            app.UseMvc(routes => {
                routes.MapRoute("areaRoute", "view/{area:exists}/{controller}/{action=Index}/{id?}");
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
                routes.MapSpaFallbackRoute("spa-fallback", new { controller = "Home", action = "Index" });
            });
        }
    }
}
