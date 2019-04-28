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
        public ReportConverter(ILogger log, ReportConfiguration config)
        {
            this.log = log;
            this.CSVReportPath = config.CSVReportPath;
            this.ArchivePath = config.ArchivePath;
            this.ExcelTemplatePath = config.ExcelTemplatePath;
            this.ReportSavePath = config.ReportSavePath;
        }
    }
}
