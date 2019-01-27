﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Feng.Authorize;
using Feng.Basic.Extensions;
using Feng.Basic.Filters;
using Feng.Config.Extensions;
using Feng.EventBus.Extensions;
using Feng.EventBus.RabbitMQ.Extensions;
using Feng.Files.IService;
using Feng.Files.Service;
using Feng.LoadBalancer.Extensions;
using Feng.Logging;
using Feng.Logging.Event;
using Feng.Mongodb;
using Feng.ServiceDiscovery.Consul;
using Feng.ServiceDiscovery.Extensions;
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
using Swashbuckle.AspNetCore.Swagger;

namespace Feng.Files
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //添加认证+MySql权限认证
            services.AddApiJwtAuthorize(Configuration);
            //services.AddApiJwtAuthorize(Configuration);.UseAuthoriser(services, Configuration).UseMySqlAuthorize();
            // 添加基础设施服务
            services.AddFeng();
            //添加NoSql数据库
            services.AddMongoService(Configuration);
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
            //业务注入

            services.AddScoped<IDomainService, DomainService>();
            services.AddScoped<IUploadService, UploadService>();
            services.AddScoped<IFileService, FileService>();

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
            // 添加接口文档
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(Configuration["Project:DocName"], new Info
                {
                    Title = Configuration["Project:Title"],
                    Version = Configuration["Project:Version"],
                    Description = Configuration["Project:Description"],
                    Contact = new Contact
                    {
                        Name = Configuration["Project:Contact:Name"],
                        Email = Configuration["Project:Contact:Email"]
                    }
                });
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, Configuration["Project:XmlFile"]));
                // Swagger验证部分
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme { In = "header", Description = "请输入带有Bearer的Token", Name = "Authorization", Type = "apiKey" });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> { { "Bearer", Enumerable.Empty<string>() } });
            });
            // 添加工具
            services.AddUtil();
            // 添加HttpClient管理
            services.AddHttpClient();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory, IHostingEnvironment env)
        {
            // 日志
            loggerFactory.AddFengLog(app, Configuration.GetValue<string>("Project:Name"));
            // 文档
            ConfigSwagger(app);
            // 公共配置
            CommonConfig(app);
        }

        /// <summary>
        /// 配置Swagger
        /// </summary>
        private void ConfigSwagger(IApplicationBuilder app)
        {
            app.UseSwagger(c =>
            {
                c.RouteTemplate = "doc/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/doc/{Configuration["Project:DocName"]}/swagger.json",
                    $"{Configuration["Project:Title"]} {Configuration["Project:Version"]}");
            });
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