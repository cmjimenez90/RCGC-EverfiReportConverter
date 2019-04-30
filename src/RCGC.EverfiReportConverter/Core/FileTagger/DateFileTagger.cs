using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RCGC.EverfiReportConverter.Core.FileTagger
{
    public class DateFileTagger : IFileTagger<DateTime>
    {
        public FileInfo Tag(FileInfo fileToTag, DateTime tag)
        {
            int TimestampStartLocation = fileToTag.FullName.Length - fileToTag.Extension.Length;
            String newFileName = fileToTag.FullName.Insert(TimestampStartLocation, FormatTimeStamp(tag));
            return new FileInfo(newFileName);
        }
        private String FormatTimeStamp(DateTime timestamp)
        {
            return timestamp.ToString("_yyyyMMdd-HHmmss");
        }

    }
}
