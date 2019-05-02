using Autofac;
using AutofacSerilogIntegration;
using Microsoft.Extensions.Configuration;
using Serilog;


namespace RCGC.EverfiReportConverter.Configuration
{
    public static class ContainerConfiguration
    {
        public static IContainer Configure(IConfiguration configuration)
        {
            Log.Logger = LoggerConfig.GetLogConfig(configuration).CreateLogger();

            ContainerBuilder builder = new ContainerBuilder();
            builder.Register(config =>
            {
                AppConfiguration appConfiguration = new AppConfiguration();
                CSVFieldOverrides csvFieldOverrides = new CSVFieldOverrides();
                configuration.GetSection("AppConfiguration").Bind(appConfiguration);
                configuration.GetSection("CSVFieldOverrides").Bind(csvFieldOverrides);
                return new Application(appConfiguration,csvFieldOverrides, Log.Logger);
            }).As<IApplication>();
            builder.RegisterLogger();
            return builder.Build();
        }       
    }
}
