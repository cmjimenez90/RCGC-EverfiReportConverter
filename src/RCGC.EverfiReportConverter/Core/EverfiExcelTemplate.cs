using System;
using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;
using RCGC.EverfiReportConverter.CSVParser.Model;
using Serilog;

namespace RCGC.EverfiReportConverter.Core
{
    public class EverfiExcelTemplate : IDisposable, IExcelTemplate<EverfiUser>
    {
        public string INPUT_START_LOCATION { get; set; } = "A3";
        public string TEMPLATE_SHEET_NAME { get; set; } = "Upload Template";
        private readonly ILogger logger;
        private readonly ExcelPackage excelPackage;

        public EverfiExcelTemplate(FileInfo TemplateFile, ILogger logger)
        {
            this.logger = logger;
            this.logger.ForContext<EverfiExcelTemplate>();

            if (TemplateFile.Exists)
            {
                excelPackage = new ExcelPackage(TemplateFile);
               
            }
            else
            {
                logger.Debug("FILE NOT FOUND: {0}", TemplateFile.FullName);
                throw new FileNotFoundException($"File not found: {TemplateFile.FullName}");
            }
        }

        public bool ImportCsv(ExcelTextFormat csvFormat, FileInfo csvFile)
        {
            if (csvFile.Exists)
            {
                ExcelWorksheet sheet = FindWorkSheetByName(this.TEMPLATE_SHEET_NAME);
                if(null != sheet){
                    logger.Debug("Loading CSV data: {0} into sheet with name of {2}", csvFile.FullName, this.TEMPLATE_SHEET_NAME);
                    sheet.Cells[this.INPUT_START_LOCATION].LoadFromText(csvFile,csvFormat);
                    return true;
                }
                logger.Error("Sheet not found: {0}", this.TEMPLATE_SHEET_NAME);
                return false;
            }
            else
            {
                this.logger.Debug("CSV FILE DOES NOT EXIST: {0}", csvFile.FullName);
                throw new FileNotFoundException($"CSV file does not exist: {csvFile.FullName}");
            }
        }
        public bool ImportDataFromList(IList<EverfiUser> items)
        {
            if(items.Count >= 1)
            {
                ExcelWorksheet sheet = FindWorkSheetByName(this.TEMPLATE_SHEET_NAME);
                if (null != sheet)
                {
                    logger.Information("Loading {0} objects into the sheet: {1}", items.Count ,this.TEMPLATE_SHEET_NAME);
                    sheet.Cells[this.INPUT_START_LOCATION].LoadFromCollection(items);
                    return true;
                }
                logger.Error("Sheet not found: {0}", this.TEMPLATE_SHEET_NAME);
                return false;
            }
            else
            {
                logger.Error("No items found to import in provided list");
                return false;
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