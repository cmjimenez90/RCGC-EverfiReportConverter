using OfficeOpenXml;
using System.IO;

namespace RCGC.EverfiReportConverter.Core
{
    public interface IExcelTemplate
    {
        bool ImportCsv(ExcelTextFormat csvFormat, FileInfo csvFile);
        bool SaveTemplateTo(FileInfo savePath);
    }
}