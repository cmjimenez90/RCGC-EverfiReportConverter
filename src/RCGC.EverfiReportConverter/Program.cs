using Autofac;
using System;

namespace RCGC.EverfiReportConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            IContainer container = ContainerConfiguration.Configure();

            using (ILifetimeScope scope = container.BeginLifetimeScope())
            {
                IApplication application = scope.Resolve<IApplication>();
                application.Run(new string[0]);
            }
        }
    }
}
