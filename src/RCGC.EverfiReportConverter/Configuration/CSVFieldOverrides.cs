using System;
using System.Collections.Generic;
using System.Text;

namespace RCGC.EverfiReportConverter.Configuration
{
    public class CSVFieldOverrides
    {
        public Dictionary<string, string> Fields { get; set; } = new Dictionary<string, string>();
    }
}
