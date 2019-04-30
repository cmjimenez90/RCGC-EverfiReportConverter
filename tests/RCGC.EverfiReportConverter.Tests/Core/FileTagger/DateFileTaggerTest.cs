using RCGC.EverfiReportConverter.Core.FileTagger;
using System;
using System.IO;
using System.Text.RegularExpressions;
using Xunit;

namespace RCGC.EverfiReportConverter.Tests.Core.FileTagger
{
    public class DateFileTaggerTest
    {
        [Fact]
        public void Tag_AppendsTimeStamp_ToEndOfFileName()
        {
            DateFileTagger fileTagger = new DateFileTagger();
            FileInfo inputFile = new FileInfo("../../../Utilities/TestData/TestFileToArchive.txt");
            DateTime timestamp = DateTime.Now;
            //Match String such as /Utilities/TestData/TestFileToArchive_20002021-142245.txt
            Regex pattern = new Regex("^.+_[\\d-]+\\..*");

            FileInfo result = fileTagger.Tag(inputFile, timestamp);
            String resultFullName = result.FullName;
            Assert.Matches(pattern, resultFullName);
        }
    }
}
