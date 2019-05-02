using OfficeOpenXml;
using RCGC.EverfiReportConverter.Configuration;
using RCGC.EverfiReportConverter.Core;
using RCGC.EverfiReportConverter.Core.FileTagger;
using Serilog;
using System;
using System.IO;

namespace RCGC.EverfiReportConverter
{
    class Application : IApplication
    {
        private readonly AppConfiguration configuration;
        private readonly ILogger logger;

        private readonly FileInfo templateFile;
        private readonly FileInfo csvFile;
        private readonly DateTime timeStamp;
        private readonly FileArchiver fileArchiver;

        public Application(AppConfiguration configuration, ILogger logger)
        {
            this.configuration = configuration;
            this.logger = logger;
            this.logger.ForContext<Application>();
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
                    logger.Error("Required Files do not exist. Exiting the application.");
                    Environment.Exit(0);
                }

                logger.Information("Loading Template at: {0}", templateFile.FullName);
                using (EverfiExcelTemplate template = new EverfiExcelTemplate(this.templateFile, this.logger))
                {
                    ExcelTextFormat format = GetCSVFileConfiguration();
                    logger.Information("Importing CSV File from: {0}", csvFile.FullName);
                    template.ImportCsv(format, this.csvFile);
                    DateFileTagger fileTagger = new DateFileTagger();
                    FileInfo saveDestination = new FileInfo(Environment.ExpandEnvironmentVariables(this.configuration.ReportSavePath));                  
                    saveDestination = fileTagger.Tag(saveDestination, this.timeStamp);

                    logger.Information("Saving report to: {0}", saveDestination.FullName);
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
                logger.Error("Could not verify excel template: {0}", this.templateFile.FullName);
                return false;
            }            
            if (!this.csvFile.Exists)
            {
                logger.Error("Could not verify csv file: {0}", this.csvFile.FullName);
                return false;
            }
            return true;
        }    

        private ExcelTextFormat GetCSVFileConfiguration()
        {
            ExcelTextFormat format = new ExcelTextFormat
            {
                EOL = configuration.EOF,
                SkipLinesBeginning = configuration.SkipBeginingCSVLines
            };
            return format;
        }
    }
}
