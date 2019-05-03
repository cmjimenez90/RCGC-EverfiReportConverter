using RCGC.EverfiReportConverter.Core.FileTagger;
using Serilog;
using System;
using System.IO;


namespace RCGC.EverfiReportConverter.Core
{
    public class FileArchiver : IArchiver<FileInfo, DirectoryInfo>
    {
        private readonly DateTime timestamp;
        private readonly ILogger log;
        private readonly DateFileTagger fileTagger;

        public FileArchiver(DateTime timestamp, ILogger log)
        {
            this.timestamp = timestamp;
            this.log = log;
            this.fileTagger = new DateFileTagger();
        }

        public FileInfo Archive(FileInfo input, DirectoryInfo destination)
        {
            CreateDestinationDirectory(destination);
            String archiveName = fileTagger.Tag(input, this.timestamp).FullName;
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
                log.Verbose("directory does not exist creating destination at {0}", directory.FullName);
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
    }
}
