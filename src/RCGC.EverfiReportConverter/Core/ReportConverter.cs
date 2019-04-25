using Microsoft.Extensions.Configuration;
using RCGC.EverfiReportConverter.Configuration;
using Serilog;
using System;

namespace RCGC.EverfiReportConverter.Core
{
    public class ReportConverter
    {
        private readonly ILogger log;
        public String CSVReportPath { get; private set; }
        public String ArchivePath { get; private set; }
        public String ExcelTemplatePath { get; private set; }
        public String ReportSavePath { get; private set; }
        public DateTime TimeStamp { get; } = DateTime.Now;
        public ReportConverter(ILogger log, IConfiguration config)
        {
            this.log = log;
            LoadConfigFromAppSettings(config);
        }

        private void LoadConfigFromAppSettings(IConfiguration config)
        {
            ReportConfiguration reportConfig = new ReportConfiguration();
            config.GetSection("ReportConfiguration").Bind(reportConfig);
            this.CSVReportPath = reportConfig.CSVReportPath;
            this.ArchivePath = reportConfig.ArchivePath;
            this.ExcelTemplatePath = reportConfig.ExcelTemplatePath;
            this.ReportSavePath = reportConfig.ReportSavePath;
        }

     



    }
}
