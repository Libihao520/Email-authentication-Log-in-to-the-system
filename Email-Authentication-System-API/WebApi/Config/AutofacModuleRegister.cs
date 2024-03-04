using System.Reflection;
using Autofac;

namespace WebApi.Config;

public class AutofacModuleRegister : Autofac.Module
{
    //注册接口和实现之间的关系
    protected override void Load(ContainerBuilder builder)
    {
        //通过反射
        Assembly interfaceAssembly = Assembly.Load("Interface");
        Assembly serviceAssembly = Assembly.Load("Service");
        builder.RegisterAssemblyTypes(interfaceAssembly, serviceAssembly).AsImplementedInterfaces();
    }
}