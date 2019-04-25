using System;
using Xunit;
using System.IO;
using RCGC.EverfiReportConverter.Core;
using System.Text.RegularExpressions;
using Serilog;
using Moq;

namespace RCGC.EverfiReportConverter.Tests.Core
{
    public class FileToFileArchiverTest
    {
        [Fact]
        public void Archive_AppendsDateTimeStampToEndOfFileName()
        {
            var mockLog = new Mock<ILogger>();
            FileInfo inputFile = new FileInfo("../../../Utilities/TestData/TestFileToArchive.txt").CopyTo("../../../Utilities/TestData/CopyToArchive.txt");
            DirectoryInfo destinationDirectory = new DirectoryInfo("../../../Utilities/TestData/ExistingTestDirectory");
            DateTime timestamp = DateTime.Now;
            //Match String such as /Utilities/TestData/TestFileToArchive_20002021-142245.txt
            Regex pattern = new Regex("^.+_[\\d-]+\\..*");

           FileArchiver archiver = new FileArchiver(timestamp,mockLog.Object);

            FileInfo result = archiver.Archive(inputFile, destinationDirectory);
            String resultFullName = result.FullName;
            result.Delete();
            inputFile.Delete();
            Assert.Matches(pattern, resultFullName);
        }
        [Fact]
        public void Archive_SavesFileInProvidedDestinationDirectory()
        {
            var mockLog = new Mock<ILogger>();
            FileInfo inputFile = new FileInfo("../../../Utilities/TestData/TestFileToArchive.txt").CopyTo("../../../Utilities/TestData/CopyToArchive.txt"); ;
         
            DirectoryInfo destinationDirectory = new DirectoryInfo("../../../Utilities/TestData/ExistingTestDirectory");
            DateTime timestamp = DateTime.Now;

            FileArchiver archiver = new FileArchiver(timestamp, mockLog.Object);

            String expectedDestinationDirectory = destinationDirectory.FullName;
            FileInfo result = archiver.Archive(inputFile, destinationDirectory);
            String resultDirectoryPath = result.DirectoryName;

            result.Delete();

            Assert.Contains(expectedDestinationDirectory, resultDirectoryPath);
        }
    }
}
