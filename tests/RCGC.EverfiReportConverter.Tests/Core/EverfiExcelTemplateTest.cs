using System.IO;
using Moq;
using Xunit;
using RCGC.EverfiReportConverter.Core;
using OfficeOpenXml;
using Serilog;

namespace RCGC.EverfiReportConverter.Tests.Core
{
    public class EverfiExcelTemplateTest
    {
        [Fact]
        public void EverfiExcelTemplate_ThrowsError_WhenTemplateDoesNotExist()
        {
            var mockLogger = new Mock<ILogger>();
            FileInfo nonExistingTemplatePath = new FileInfo("./nonexistingFilePath.xlsx");

            Assert.Throws<FileNotFoundException>(() => new EverfiExcelTemplate(nonExistingTemplatePath,mockLogger.Object));
        }

        [Fact]
        public void EverfiExcelTemplate_SaveTemplateTo_ReturnsFalseIfFileAlreadyExist()
        {
            FileInfo existingTemplate = new FileInfo("../../../Utilities/TestData/faketemplate.xlsx");
            FileInfo existingFilePathPath = new FileInfo("../../../Utilities/TestData/existingsaveas.xlsx");
            var mockLogger = new Mock<ILogger>();
            bool didFileSave = true;

            using (EverfiExcelTemplate excelTemplate = new EverfiExcelTemplate(existingTemplate, mockLogger.Object))
            {
                didFileSave = excelTemplate.SaveTemplateTo(existingFilePathPath);
            }

            Assert.False(didFileSave);
        }

        [Fact]
        public void EverfiExcelTemplate_SaveTemplateTo_ReturnsTrueIfFileSaveSuccessful()
        {
            FileInfo existingTemplate = new FileInfo("../../../Utilities/TestData/faketemplate.xlsx");
            FileInfo nonexistingFilePath = new FileInfo("../../../Utilities/TestData/nonexistingsaveas.xlsx");
            var mockLogger = new Mock<ILogger>();
            bool didFileSave = false;

            using (EverfiExcelTemplate excelTemplate = new EverfiExcelTemplate(existingTemplate, mockLogger.Object))
            {
                didFileSave = excelTemplate.SaveTemplateTo(nonexistingFilePath);
            }

            nonexistingFilePath.Delete();
            Assert.True(didFileSave);
        }

        [Fact]
        public void EverfiExcelTemplate_SaveTemplateTo_WillLeaveCreatedFileBehind()
        {
            FileInfo existingTemplate = new FileInfo("../../../Utilities/TestData/faketemplate.xlsx");
            FileInfo nonexistingFilePath = new FileInfo("../../../Utilities/TestData/nonexistingsaveas.xlsx");
            var mockLogger = new Mock<ILogger>();
            bool doesFileInitiallyExist = nonexistingFilePath.Exists;
            bool doesFileExistAfterSaveAs = false;

            using (EverfiExcelTemplate excelTemplate = new EverfiExcelTemplate(existingTemplate, mockLogger.Object))
            {
                excelTemplate.SaveTemplateTo(nonexistingFilePath);
                nonexistingFilePath.Refresh();
                doesFileExistAfterSaveAs = nonexistingFilePath.Exists;
            }

            nonexistingFilePath.Delete();
            Assert.False(doesFileInitiallyExist);
            Assert.True(doesFileExistAfterSaveAs);
        }


        [Fact]
        public void EverfiExcelTemplate_ImportCSV_ThrowsFileNotFoundIfFileDoesNotExist()
        {
            FileInfo existingFilePath = new FileInfo("../../../Utilities/TestData/faketemplate.xlsx");
            FileInfo nonExistingCSVFile = new FileInfo("../../../Utilities/TestData/nonexisting.csv");
            var mockLogger = new Mock<ILogger>();
            using (EverfiExcelTemplate excelTemplate = new EverfiExcelTemplate(existingFilePath, mockLogger.Object))
            {
                ExcelTextFormat format = new ExcelTextFormat();
                Assert.Throws<FileNotFoundException>(() => excelTemplate.ImportCsv(format, nonExistingCSVFile));
            }
        }
        [Fact]
        public void EverfiExcelTemplate_ImportCSV_ReturnsFalseIfSheetDoesNotExist()
        {
            FileInfo existingFilePath = new FileInfo("../../../Utilities/TestData/faketemplate.xlsx");
            FileInfo csvFile = new FileInfo("../../../Utilities/TestData/data.csv");
            string wrongSheetName = "Does Not Exist";
            bool csvDataImported = true;
            var mockLogger = new Mock<ILogger>();
            using (EverfiExcelTemplate excelTemplate = new EverfiExcelTemplate(existingFilePath, mockLogger.Object))
            {
                ExcelTextFormat format = new ExcelTextFormat();
                excelTemplate.TEMPLATE_SHEET_NAME = wrongSheetName;
                csvDataImported = excelTemplate.ImportCsv(format, csvFile);
            }

            Assert.False(csvDataImported);
        }

        [Fact]
        public void EverfiExcelTemplate_ImportCSV_ReturnsTrueIfDataIsImported()
        {
            FileInfo existingFilePath = new FileInfo("../../../Utilities/TestData/faketemplate.xlsx");
            FileInfo csvFile = new FileInfo("../../../Utilities/TestData/data.csv");
            bool csvDataImported = false;
            var mockLogger = new Mock<ILogger>();
            using (EverfiExcelTemplate excelTemplate = new EverfiExcelTemplate(existingFilePath, mockLogger.Object))
            {
                ExcelTextFormat format = new ExcelTextFormat();
                csvDataImported = excelTemplate.ImportCsv(format, csvFile);
            }

            Assert.True(csvDataImported);
        }
    }
}