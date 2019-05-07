using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;

namespace RCGC.EverfiReportConverter.Core
{
    public interface IExcelTemplate<T>
    {
        bool ImportCsv(ExcelTextFormat csvFormat, FileInfo csvFile);
        bool ImportDataFromList(IList<T> items);
        bool SaveTemplateTo(FileInfo savePath);
    }
}