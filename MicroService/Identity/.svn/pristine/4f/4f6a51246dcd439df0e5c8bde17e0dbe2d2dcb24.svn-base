using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Feng.Authorize;
using Feng.Authorize.HostedService;
using Feng.Authorize.MySql;
using Feng.Basic.Extensions;
using Feng.Basic.Filters;
using Feng.Config.Extensions;
using Feng.Config.HostedService;
using Feng.DbContext;
using Feng.EventBus.Extensions;
using Feng.EventBus.RabbitMQ.Extensions;
using Feng.HostedService;
using Feng.Logging;
using Feng.Logging.Event;
using Feng.ServiceDiscovery.Consul;
using Feng.ServiceDiscovery.Consul.Extensions;
using Feng.ServiceDiscovery.Extensions;
using Feng.Swagger;
using Feng.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;

namespace Feng.AuthUser
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
            // 添加授权认证
            services.AddApiJwtAuthorize(Configuration).UseAuthoriser(services, builder => { builder.UseMySqlAuthorize(); });
            // 添加基础设施服务
            services.AddFeng();
            // 添加数据ORM
            services.AddSqlSugarDbContext();
            // 添加配置服务
            services.AddConfigService(Configuration);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            // 添加事件驱动
            services.AddEventBus(builder => { builder.UseRabbitMQ(); });
            // 添加服务发现
            services.AddServiceDiscovery(builder => { builder.UseConsul(); });
            // 添加事件队列日志
            services.AddEventLog();

            // 添加全局定时服务
            services.AddFengHostedService(builder => { builder.AddConfig().AddAuthorize(); });
            // 添加过滤器
            services.AddMvc(options =>
            {
                //options.Filters.Add(typeof(WebApiTracingFilterAttribute));
                options.Filters.Add(typeof(WebApiActionFilterAttribute));
            }).AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss.fff";
            });
            // 添加接口文档
            services.AddFengSwagger(Configuration);
            // 添加工具
            services.AddUtil();
            // 添加HttpClient工厂
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
            // 日志,事件驱动日志
            loggerFactory.AddFengLog(app, "Feng.AuthUser.Api");
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
        /// <summary>
        /// Autofac扩展注册
        /// </summary>
        public class AutofacModuleRegister : Autofac.Module
        {
            /// <summary>
            /// 装载autofac方式注册
            /// </summary>
            /// <param name="builder"></param>
            protected override void Load(ContainerBuilder builder)
            {
                // 业务应用注册
                Assembly _assembly = Assembly.Load("Feng.Identity.Service");
                builder.RegisterAssemblyTypes(_assembly)
                    .Where(t => !t.IsAbstract && !t.IsInterface && t.Name.EndsWith("Service"))
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();

                // 数据仓储泛型注册
                builder.RegisterGeneric(typeof(SqlSugarRepository<>)).As(typeof(IDbRepository<>))
                    .InstancePerLifetimeScope();
            }
        }
    }
}
