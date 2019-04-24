using System.IO;

namespace RCGC.EverfiReportConverter.Core
{
    public interface IExcelTemplate
    {
        bool ImportCsv(CSVFileFormat csvFormat, FileInfo csvFile);
        bool SaveTemplateTo(FileInfo savePath);
    }
}