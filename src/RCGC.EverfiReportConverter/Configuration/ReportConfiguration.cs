using System;
using System.Collections.Generic;
using System.Text;

namespace RCGC.EverfiReportConverter.Configuration
{
    public class ReportConfiguration
    {
        public String CSVReportPath { get; set; }
        public String ArchiveDirectory { get;  set; }
        public String ExcelTemplatePath { get;  set; }
        public String ReportSavePath { get;  set; }

        public String EOF { get; set; } = "\n";
        public int SkipBeginingCSVLines { get; set; } = 1;
    }
}
