using System.IO;
using Xunit;
using RCGC.EverfiReportConverter.Core;
using RCGC.EverfiReportConverter.Tests.Utilities;

namespace RCGC.EverfiReportConverter.Tests.Core
{
    public class EverfiExcelTemplateTest
    {
        [Fact]
        public void EverfiExcelTemplate_ThrowsError_WhenTemplateDoesNotExist()
        {
            FileInfo nonExistingTemplatePath = new FileInfo("./nonexistingFilePath.xlsx");

            Assert.Throws<FileNotFoundException>(() => new EverfiExcelTemplate(nonExistingTemplatePath));
        }

        [Fact]
        public void EverfiExcelTemplate_SaveTemplateTo_ReturnsFalseIfFileAlreadyExist()
        {
            FileInfo existingTemplate = new FileInfo("../../../Utilities/TestData/faketemplate.xlsx");
            FileInfo existingFilePathPath = new FileInfo("../../../Utilities/TestData/existingsaveas.xlsx");

            bool didFileSave = true;

            using (EverfiExcelTemplate excelTemplate = new EverfiExcelTemplate(existingTemplate))
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

            bool didFileSave = false;

            using (EverfiExcelTemplate excelTemplate = new EverfiExcelTemplate(existingTemplate))
            {
                didFileSave = excelTemplate.SaveTemplateTo(nonexistingFilePath);
            }

            FileCleanup.Instance.RemoveFile(nonexistingFilePath);
            Assert.True(didFileSave);
        }

        [Fact]
        public void EverfiExcelTemplate_SaveTemplateTo_WillLeaveCreatedFileBehind()
        {
            FileInfo existingTemplate = new FileInfo("../../../Utilities/TestData/faketemplate.xlsx");
            FileInfo nonexistingFilePath = new FileInfo("../../../Utilities/TestData/nonexistingsaveas.xlsx");

            bool doesFileInitiallyExist = nonexistingFilePath.Exists;
            bool doesFileExistAfterSaveAs = false;

            using (EverfiExcelTemplate excelTemplate = new EverfiExcelTemplate(existingTemplate))
            {
                excelTemplate.SaveTemplateTo(nonexistingFilePath);
                nonexistingFilePath.Refresh();
                doesFileExistAfterSaveAs = nonexistingFilePath.Exists;
            }

            FileCleanup.Instance.RemoveFile(nonexistingFilePath);
            Assert.False(doesFileInitiallyExist);
            Assert.True(doesFileExistAfterSaveAs);
        }


        [Fact]
        public void EverfiExcelTemplate_ImportCSV_ThrowsFileNotFoundIfFileDoesNotExist()
        {
            FileInfo existingFilePath = new FileInfo("../../../Utilities/TestData/faketemplate.xlsx");
            FileInfo nonExistingCSVFile = new FileInfo("../../../Utilities/TestData/nonexisting.csv");

            using (EverfiExcelTemplate excelTemplate = new EverfiExcelTemplate(existingFilePath))
            {
                CSVFileFormat format = new CSVFileFormat();
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

            using (EverfiExcelTemplate excelTemplate = new EverfiExcelTemplate(existingFilePath))
            {
                CSVFileFormat format = new CSVFileFormat();
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

            using (EverfiExcelTemplate excelTemplate = new EverfiExcelTemplate(existingFilePath))
            {
                CSVFileFormat format = new CSVFileFormat();
                csvDataImported = excelTemplate.ImportCsv(format, csvFile);
            }

            Assert.True(csvDataImported);
        }
    }
}