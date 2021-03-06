﻿using Autofac;
using Microsoft.Extensions.Configuration;
using RCGC.EverfiReportConverter.Configuration;

namespace RCGC.EverfiReportConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            IContainer container = ContainerConfiguration.Configure(configuration);

            using (ILifetimeScope scope = container.BeginLifetimeScope())
            {
                IApplication application = scope.Resolve<IApplication>();
                application.Run();
            }
        }
    }
}
