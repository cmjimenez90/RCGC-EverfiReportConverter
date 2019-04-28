using RCGC.EverfiReportConverter.Core;
using Serilog;
using System;


namespace RCGC.EverfiReportConverter
{
    class Application : IApplication
    {
        private readonly ReportConverter reportConverter;
        private readonly ILogger logger;
        public Application(ReportConverter reportConverter, ILogger logger)
        {
            this.reportConverter = reportConverter;
            this.logger = logger;
        }

        public void Run()
        {         
            Console.ReadKey();
        }
    }
}
