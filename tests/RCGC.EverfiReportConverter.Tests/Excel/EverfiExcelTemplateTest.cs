using Microsoft.VisualBasic.CompilerServices;
using System.Reflection;
using System.IO;
using Xunit;
using RCGC.EverfiReportConverter.Everfi;
using RCGC.EverfiReportConverter.Tests.Utilities;

namespace RCGC.EverfiReportConverter.Tests.Excel
{
    public class EverfiExcelTemplateTest
    {
        [Fact]
        public void EverfiExcelTemplate_ThrowsError_WhenTemplateDoesNotExist()
        {
            FileInfo fileInfo = new FileInfo("./nonexistingfile.xlsx");

            Assert.Throws<FileNotFoundException>(() => new EverfiExcelTemplate(fileInfo));
        }

        [Fact]
        public void EverfiExcelTemplate_SaveTemplateTo_ReturnsFalseIfFileAlreadyExist()
        {
            FileInfo template = new FileInfo("../../../Utilities/faketemplate.xlsx");
            FileInfo existingFile = new FileInfo("../../../Utilities/existingsaveas.xlsx");

            bool didFileSave = true;

            using (EverfiExcelTemplate excelTemplate = new EverfiExcelTemplate(template))
            {
                didFileSave = excelTemplate.SaveTemplateTo(existingFile);
            }

            Assert.False(didFileSave);
        }

        [Fact]
        public void EverfiExcelTemplate_SaveTemplateTo_ReturnsTrueIfFileSaveSuccessful()
        {
            FileInfo template = new FileInfo("../../../Utilities/faketemplate.xlsx");
            FileInfo nonExistingFile = new FileInfo("../../../Utilities/nonexistingsaveas.xlsx");

            bool didFileSave = false;

            using (EverfiExcelTemplate excelTemplate = new EverfiExcelTemplate(template))
            {
                didFileSave = excelTemplate.SaveTemplateTo(nonExistingFile);
            }

            FileCleanup.Instance.RemoveFile(nonExistingFile);
            Assert.True(didFileSave);
        }

        [Fact]
        public void EverfiExcelTemplate_SaveTemplateTo_WillLeaveCreatedFileBehind()
        {
            FileInfo template = new FileInfo("../../../Utilities/faketemplate.xlsx");
            FileInfo nonExistingFile = new FileInfo("../../../Utilities/nonexistingsaveas.xlsx");

            bool doesFileInitiallyExist = nonExistingFile.Exists;
            bool doesFileExistAfterSaveAs = false;

            using (EverfiExcelTemplate excelTemplate = new EverfiExcelTemplate(template))
            {
                excelTemplate.SaveTemplateTo(nonExistingFile);
                nonExistingFile.Refresh();
                doesFileExistAfterSaveAs = nonExistingFile.Exists;
            }

            FileCleanup.Instance.RemoveFile(nonExistingFile);
            Assert.False(doesFileInitiallyExist);
            Assert.True(doesFileExistAfterSaveAs);
        }


        [Fact]
        public void EverfiExcelTemplate_ImportCSV_ThrowsFileNotFoundIfFileDoesNotExist()
        {
            FileInfo existingFile = new FileInfo("../../../Utilities/faketemplate.xlsx");
            FileInfo nonExistingCSVFile = new FileInfo("../../../Utilities/nonexisting.csv");

            using (EverfiExcelTemplate excelTemplate = new EverfiExcelTemplate(existingFile))
            {
                CSVFileFormat format = new CSVFileFormat();
                Assert.Throws<FileNotFoundException>(() => excelTemplate.ImportCsv(format, nonExistingCSVFile));
            }
        }
        [Fact]
        public void EverfiExcelTemplate_ImportCSV_ReturnsFalseIfSheetDoesNotExist()
        {
            FileInfo existingFile = new FileInfo("../../../Utilities/faketemplate.xlsx");
            FileInfo csvFile = new FileInfo("../../../Utilities/data.csv");
            string wrongSheetName = "Does Not Exist";
            bool csvDataImported = true;

            using (EverfiExcelTemplate excelTemplate = new EverfiExcelTemplate(existingFile))
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
            FileInfo existingFile = new FileInfo("../../../Utilities/faketemplate.xlsx");
            FileInfo csvFile = new FileInfo("../../../Utilities/data.csv");
            bool csvDataImported = false;

            using (EverfiExcelTemplate excelTemplate = new EverfiExcelTemplate(existingFile))
            {
                CSVFileFormat format = new CSVFileFormat();
                csvDataImported = excelTemplate.ImportCsv(format, csvFile);
            }

            Assert.True(csvDataImported);
        }
    }
}