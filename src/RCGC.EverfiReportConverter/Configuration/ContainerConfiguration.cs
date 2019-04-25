using Autofac;
using AutofacSerilogIntegration;
using Microsoft.Extensions.Configuration;
using RCGC.EverfiReportConverter.Core;
using Serilog;
using Serilog.Core;
using System.IO;

namespace RCGC.EverfiReportConverter.Configuration
{
    public static class ContainerConfiguration
    {
        public static IContainer Configure(IConfiguration configuration)
        {
            Log.Logger = LoggerConfig.GetLogConfig(configuration).CreateLogger();
            ContainerBuilder builder = new ContainerBuilder();
           
            builder.RegisterType<Application>().As<IApplication>();
            builder.RegisterLogger();
            return builder.Build();
        }       
    }
}
