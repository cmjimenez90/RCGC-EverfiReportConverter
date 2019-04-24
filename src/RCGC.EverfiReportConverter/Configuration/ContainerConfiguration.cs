using Autofac;
using AutofacSerilogIntegration;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;

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
