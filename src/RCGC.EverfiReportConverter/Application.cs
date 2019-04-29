using OfficeOpenXml;
using RCGC.EverfiReportConverter.Configuration;
using RCGC.EverfiReportConverter.Core;
using Serilog;
using System;
using System.IO;

namespace RCGC.EverfiReportConverter
{
    class Application : IApplication
    {
        private readonly ReportConfiguration configuration;
        private readonly ILogger logger;

        private readonly FileInfo templateFile;
        private readonly FileInfo csvFile;
        private readonly DateTime timeStamp;
        private readonly FileArchiver fileArchiver;

        public Application(ReportConfiguration configuration, ILogger logger)
        {
            this.configuration = configuration;
            this.logger = logger;
            try
            {
                timeStamp = DateTime.Now;
                fileArchiver = new FileArchiver(this.timeStamp, this.logger);
                this.templateFile = new FileInfo(Environment.ExpandEnvironmentVariables(configuration.ExcelTemplatePath));
                this.csvFile = new FileInfo(Environment.ExpandEnvironmentVariables(configuration.CSVReportPath));             
            }
            catch(Exception ex)
            {
                logger.Error("Failed to parse file paths | Reason: {0}", ex);
                Environment.Exit(0);
            }          
        }

        public void Run()
        {  
            try
            {
                if (!VerifyImportFiles())
                {
                    logger.Information("Required Files do not exist. Exiting the application.");
                    Environment.Exit(0);
                }
                EverfiExcelTemplate template = new EverfiExcelTemplate(this.templateFile);

                ExcelTextFormat format = GetCSVFileConfiguration();
                template.ImportCsv(format, this.csvFile);
                FileInfo saveDestination = CreateExcelReportFileInfo(Environment.ExpandEnvironmentVariables(this.configuration.ReportSavePath));


                bool saveSuccessful = template.SaveTemplateTo(saveDestination);
                if (saveSuccessful)
                {
                    logger.Information("Save successful | Path: {0}", saveDestination.FullName);
                    DirectoryInfo archiveDirectory = new DirectoryInfo(Environment.ExpandEnvironmentVariables(configuration.ArchiveDirectory));
                    FileInfo archivedFile = this.fileArchiver.Archive(csvFile, archiveDirectory);
                    if (!archivedFile.Exists)
                    {
                        logger.Warning("Could not archive the CSV File: {0}", archivedFile.FullName);
                    }
                    else
                    {
                        logger.Information("Archive successful: {0}", archivedFile.FullName);
                    }
                }
                else
                {
                    logger.Error("Failed to save the exported excel sheet in the specified location: {0}", saveDestination.FullName);
                    Environment.Exit(0);
                }
            }
            catch (Exception ex)
            {
                logger.Error("Something unexpected happened | Reason: {0}", ex);
                Environment.Exit(0);
            }
            
        }

       
        private bool VerifyImportFiles()
        {
            if (!this.templateFile.Exists)
            {
                logger.Information("Could not verify excel template: {0}", this.templateFile.FullName);
                return false;
            }            
            if (!this.csvFile.Exists)
            {
                logger.Information("Could not verify csv file: {0}", this.csvFile.FullName);
                return false;
            }
            return true;
        }

        private FileInfo CreateExcelReportFileInfo(string input)
        {
            FileInfo orginalFileInfo = new FileInfo(input);
            FileInfo destinationFileInfo = new FileInfo(ModifyFileName(orginalFileInfo));
            return destinationFileInfo;
        }

        private String ModifyFileName(FileInfo input)
        {
            int TimestampStartLocation = input.FullName.Length - input.Extension.Length;

            String newFileName = input.FullName.Insert(TimestampStartLocation, FormatTimeStamp(this.timeStamp));
            return newFileName;
        }

        private String FormatTimeStamp(DateTime timestamp)
        {
            return timestamp.ToString("_yyyyMMdd-HHmmss");
        }

        private ExcelTextFormat GetCSVFileConfiguration()
        {
            ExcelTextFormat format = new ExcelTextFormat();
            format.EOL = configuration.EOF;
            format.SkipLinesBeginning = configuration.SkipBeginingCSVLines;
            return format;
        }
    }
}
