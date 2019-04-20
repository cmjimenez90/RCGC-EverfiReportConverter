using System.IO;

using Xunit;
using RCGC.EverfiReportConverter.FileParser;
using RCGC.EverfiReportConverter.Model;
using System.Linq;

namespace RCGC.EverfiReportConverter.Tests.FileParser
{

    public class EverfiCSVReaderTest
    {
        [Fact]
        public void ReadDataFromCsvFile_ValidCSVFile_ReturnsListOfEverfiUser()
        {
            string testFile = "../../../Utilities/validdata.csv";
            char delimeter = ',';
            EverfiCSVReader reader = new EverfiCSVReader(delimeter);

            var usersFromCSV = reader.ReadDataFromCsvFile(testFile).ToList();

            Assert.NotEmpty(usersFromCSV);
        }
        [Fact]
         public void ReadDataFromCsvFile_InvalidDataFromCSV_ReturnsEmptyList()
        {
            string testFile = "../../../Utilities/invaliddata.csv";
            char delimeter = ',';
            EverfiCSVReader reader = new EverfiCSVReader(delimeter);

            var usersFromCSV = reader.ReadDataFromCsvFile(testFile);
           
            Assert.Empty(usersFromCSV);
        }
        [Fact]
        public void ReadDataFromCsvFile_FileDoesNotExist_ThrowsFileNotFound()
        {
            string testFile = "../../../Utilities/doesnotexist.csv";
            char delimeter = ',';
            EverfiCSVReader reader = new EverfiCSVReader(delimeter);

            Assert.Throws<FileNotFoundException>(() => reader.ReadDataFromCsvFile(testFile));
        }
    }
}