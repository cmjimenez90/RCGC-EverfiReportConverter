using System;
using System.IO;
using OfficeOpenXml;

namespace RCGC.EverfiReportConverter.Core
{
    public class EverfiExcelTemplate : IDisposable, IExcelTemplate
    {
        public string INPUT_START_LOCATION { get; set; } = "A3";
        public string TEMPLATE_SHEET_NAME { get; set; } = "Upload Template";
        private readonly ExcelPackage excelPackage;

        public EverfiExcelTemplate(FileInfo fileInfo)
        {
            if (fileInfo.Exists)
            {
                excelPackage = new ExcelPackage(fileInfo);
            }
            else
            {
                throw new FileNotFoundException($"File not found: {fileInfo.FullName}");
            }
        }

        public bool ImportCsv(CSVFileFormat csvFormat, FileInfo csvFile)
        {
            if (csvFile.Exists)
            {
                ExcelWorksheet sheet = FindWorkSheetByName(this.TEMPLATE_SHEET_NAME);
                if(null != sheet){
                    sheet.Cells[this.INPUT_START_LOCATION].LoadFromText(csvFile,csvFormat.GetFormat());
                    return true;
                }
                return false;
            }
            else
            {
                throw new FileNotFoundException($"CSV file does not exist: {csvFile.FullName}");
            }
        }

        public bool SaveTemplateTo(FileInfo savePath)
        {
            if(savePath.Exists){
                return false;
            }
            this.excelPackage.SaveAs(savePath);
            return true;
        }

        public void Dispose()
        {
            this.excelPackage.Dispose();
        }

        private ExcelWorksheet FindWorkSheetByName(String workSheetName)
        {
            foreach (ExcelWorksheet sheet in this.excelPackage.Workbook.Worksheets)
            {
                if (sheet.Name == workSheetName)
                    return sheet;
            }
            return null;
        }

    }
}