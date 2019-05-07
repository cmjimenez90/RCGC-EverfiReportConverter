using System.IO;

using Xunit;
using RCGC.EverfiReportConverter.CSVParser;
using System.Linq;
using Moq;
using Serilog;
using RCGC.EverfiReportConverter.Configuration;
using System.Collections.Generic;

namespace RCGC.EverfiReportConverter.Tests.CSVParser
{

    public class EverfiCSVReaderTest
    {
        [Fact]
        public void ReadDataFromCsvFile_ValidCSVFile_ReturnsListOfEverfiUser()
        {
            var mockLogger = new Mock<ILogger>();
            string testFile = "../../../Utilities/validdata.csv";
            EverfiCSVReader reader = new EverfiCSVReader(mockLogger.Object);

            var usersFromCSV = reader.ReadDataFromCsvFile(testFile).ToList();

            Assert.NotEmpty(usersFromCSV);
        }
        [Fact]
         public void ReadDataFromCsvFile_InvalidDataFromCSV_ReturnsEmptyList()
        {
            var mockLogger = new Mock<ILogger>();
            string testFile = "../../../Utilities/invaliddata.csv";
            EverfiCSVReader reader = new EverfiCSVReader(mockLogger.Object);

            var usersFromCSV = reader.ReadDataFromCsvFile(testFile);
           
            Assert.Empty(usersFromCSV);
        }
        [Fact]
        public void ReadDataFromCsvFile_FileDoesNotExist_ThrowsFileNotFound()
        {
            var mockLogger = new Mock<ILogger>();
            string testFile = "../../../Utilities/doesnotexist.csv";
            EverfiCSVReader reader = new EverfiCSVReader(mockLogger.Object);

            Assert.Throws<FileNotFoundException>(() => reader.ReadDataFromCsvFile(testFile));
        }
        [Fact]
        public void ReadDataFromCsvFile_DoesNotOverride_WhenEmptyCSVFieldOverridesProvided()
        {
            
            var mockLogger = new Mock<ILogger>();
            string testFile = "../../../Utilities/validdata.csv";
            CSVFieldOverrides withoutOverrides = new CSVFieldOverrides();

            Dictionary<string, string> fields = new Dictionary<string, string>();
            fields.Add("FIRST_NAME", "FIRST_NAME");
            fields.Add("LAST_NAME", "LAST_NAME");
            fields.Add("EMAIL", "EMAIL");
            fields.Add("SUPERVISOR", "SUPERVISOR");
            fields.Add("EMPLOYEE_ID", "EMPLOYEE_ID");
            fields.Add("LOCATION_TITLE", "LOCATION_TITLE");
            fields.Add("LOCATION_ABR", "LOCATION_ABR");
            fields.Add("GROUP_TITLE", "GROUP_TITLE");
            fields.Add("GROUP_ABR", "GROUP_ABR");
            CSVFieldOverrides withOverrides = new CSVFieldOverrides();
            withOverrides.Fields = fields;

            EverfiCSVReader reader = new EverfiCSVReader(mockLogger.Object, withoutOverrides);
            var usersFromCSVNoOverride = reader.ReadDataFromCsvFile(testFile).ToList();           

            foreach(var field in fields)
            {
                foreach( var user in usersFromCSVNoOverride)
                {
                    Assert.DoesNotMatch(field.Value, user.GetType().GetProperty(field.Key).GetValue(user).ToString());
                }
            }
        }

        [Fact]
        public void ReadDataFromCsvFile_OverridesProvidedFields_CSVFieldOverridesProvided()
        {
            var mockLogger = new Mock<ILogger>();
            string testFile = "../../../Utilities/validdata.csv";
            CSVFieldOverrides withoutOverrides = new CSVFieldOverrides();

            Dictionary<string, string> fields = new Dictionary<string, string>();
            fields.Add("FIRST_NAME", "FIRST_NAME");
            fields.Add("LAST_NAME", "LAST_NAME");
            fields.Add("EMAIL", "EMAIL");
            fields.Add("SUPERVISOR", "SUPERVISOR");
            fields.Add("EMPLOYEE_ID", "EMPLOYEE_ID");
            fields.Add("LOCATION_TITLE", "LOCATION_TITLE");
            fields.Add("LOCATION_ABR", "LOCATION_ABR");
            fields.Add("GROUP_TITLE", "GROUP_TITLE");
            fields.Add("GROUP_ABR", "GROUP_ABR");
            CSVFieldOverrides withOverrides = new CSVFieldOverrides();
            withOverrides.Fields = fields;

            EverfiCSVReader reader = new EverfiCSVReader(mockLogger.Object, withOverrides);
            var usersFromCSVWithOverride = reader.ReadDataFromCsvFile(testFile).ToList();

            foreach (var field in fields)
            {
                foreach (var user in usersFromCSVWithOverride)
                {
                    Assert.Matches(field.Value, user.GetType().GetProperty(field.Key).GetValue(user).ToString());
                }
            }
        }
    }
}