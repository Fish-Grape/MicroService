using Autofac;
using Feng.DbContext;
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
            Assembly _assembly = Assembly.Load("Feng.Order.Service");
            builder.RegisterAssemblyTypes(_assembly)
                .Where(t => !t.IsAbstract && !t.IsInterface && t.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            // 数据仓储泛型注册
            builder.RegisterGeneric(typeof(RepositoryBase<>)).As(typeof(IRepositoryBase<>))
                .InstancePerLifetimeScope();
        }

        /// <summary>
        /// 反射获取指定类型的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assemblyName">程序集</param>
        /// <param name="fullName">全类名</param>
        /// <param name="parameters">参数</param>
        /// <returns></returns>
        public object GetObjByReflect(string assemblyName, string fullName, object[] parameters = null)
        {
            Type type = GetAssembly(assemblyName).GetType(fullName);
            if (parameters == null || parameters.Count() == 0)
                return Activator.CreateInstance(type);
            else
                return Activator.CreateInstance(type, parameters);
        }

        public Assembly GetAssembly(string assemblyName)
        {
            Assembly assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(AppContext.BaseDirectory + $"{assemblyName}.dll");
            return assembly;
        }
    }
}