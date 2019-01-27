using Autofac;
using Feng.DbContext;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using System;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using Module = Autofac.Module;

namespace Feng.CMS.Config
{
    public class DefaultModuleRegister : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // 业务应用注册
            Assembly _assembly = Assembly.Load("Feng.CMS.Service");
            builder.RegisterAssemblyTypes(_assembly)
                .Where(t => !t.IsAbstract && !t.IsInterface && t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            // 数据仓储泛型注册
            builder.RegisterGeneric(typeof(RepositoryBase<>)).As(typeof(IRepositoryBase<>))
                .InstancePerLifetimeScope();
            
        }
        
    }
}