using System;
using System.IO;
using OfficeOpenXml;
using Serilog;

namespace RCGC.EverfiReportConverter.Core
{
    public class EverfiExcelTemplate : IDisposable, IExcelTemplate
    {
        public string INPUT_START_LOCATION { get; set; } = "A3";
        public string TEMPLATE_SHEET_NAME { get; set; } = "Upload Template";
        private readonly ILogger logger;
        private readonly ExcelPackage excelPackage;

        public EverfiExcelTemplate(FileInfo fileInfo, ILogger logger)
        {
            this.logger = logger;
            this.logger.ForContext<EverfiExcelTemplate>();

            if (fileInfo.Exists)
            {
                excelPackage = new ExcelPackage(fileInfo);
               
            }
            else
            {
                this.logger.Debug("FILE NOT FOUND: {0}", fileInfo.FullName);
                throw new FileNotFoundException($"File not found: {fileInfo.FullName}");
            }
        }

        public bool ImportCsv(ExcelTextFormat csvFormat, FileInfo csvFile)
        {
            if (csvFile.Exists)
            {
                ExcelWorksheet sheet = FindWorkSheetByName(this.TEMPLATE_SHEET_NAME);
                if(null != sheet){
                    this.logger.Debug("Loading CSV data: {0} into sheet with name of {2}", csvFile.FullName, this.TEMPLATE_SHEET_NAME);
                    sheet.Cells[this.INPUT_START_LOCATION].LoadFromText(csvFile,csvFormat);
                    return true;
                }              
                return false;
            }
            else
            {
                this.logger.Debug("CSV FILE DOES NOT EXIST: {0}", csvFile.FullName);
                throw new FileNotFoundException($"CSV file does not exist: {csvFile.FullName}");
            }
        }

        public bool SaveTemplateTo(FileInfo savePath)
        {
            if(savePath.Exists){
                this.logger.Debug("Can not overwrite existing save path: {0}",savePath.FullName);
                return false;
            }

            try
            {
                this.excelPackage.SaveAs(savePath);
                return true;
            }
            catch(Exception ex)
            {
                this.logger.Debug("Failed to save the excel file | Reason {0}", ex);
                return false;
            }
            
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
                {
                    this.logger.Verbose("Sheet found: {0}", sheet.Name);
                    return sheet;
                }
                    
            }
            this.logger.Verbose("Sheet not found: {0} | returning null", workSheetName);
            return null;
        }

    }
}