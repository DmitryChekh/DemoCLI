using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace MoneyRockCLI
{
    public static class ContainerConfigure
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<CommandExecuter>().As<ICommandExecuter>();

            builder.RegisterAssemblyTypes(Assembly.Load(nameof(Utilities)))
                .Where(t => t.Namespace.Contains("Services"))
                .As(t => t.GetInterfaces().FirstOrDefault(i => i.Name == "I" + t.Name));

            return builder.Build();
        }
    }
}
