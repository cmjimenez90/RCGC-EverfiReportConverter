using Microsoft.Extensions.Configuration;
using Serilog;

namespace RCGC.EverfiReportConverter.Configuration
{
    public static class LoggerConfig
    {
        public static LoggerConfiguration GetLogConfig(IConfiguration configuration)
        {
            LoggerConfiguration config = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration);

            return config;
        }
    }
}
