using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RCGC.EverfiReportConverter.Core
{
    public class FileArchiver : IArchiver<FileInfo, DirectoryInfo>
    {
        private readonly DateTime timestamp;
        private readonly ILogger log;

        public FileArchiver(DateTime timestamp, ILogger log)
        {
            this.timestamp = timestamp;
            this.log = log;
        }

        public FileInfo Archive(FileInfo input, DirectoryInfo destination)
        {
            CreateDestinationDirectory(destination);
            String archiveName = CreateArchiveFileName(input);
            String fullArchivePath = Path.Combine(destination.FullName, Path.GetFileName(archiveName));
            try
            {
               input.MoveTo(fullArchivePath);
   
            }
            catch (Exception exception)
            {
                log.Error("Could not create archive the provided file: {FullName}. REASON: {Message}", input.FullName, exception.Message);
            }

            return new FileInfo(fullArchivePath);

        }

        private void CreateDestinationDirectory(DirectoryInfo directory)
        {
            if (!directory.Exists)
            {
                try
                {
                    directory.Create();
                }
                catch (Exception exception)
                {
                    log.Error("Could not create directory at destination: {Fullname}. REASON: {Message}", directory.FullName, exception.Message);
   
                }           
            }
        }

        private String CreateArchiveFileName(FileInfo input)
        {
            int TimestampStartLocation = input.FullName.Length - input.Extension.Length;
           
            String newFileName = input.FullName.Insert(TimestampStartLocation, FormatTimeStamp());
            return newFileName;
        }

        private String FormatTimeStamp()
        {
            return timestamp.ToString("_yyyyMMdd-HHmmss");
        }

    }
}
