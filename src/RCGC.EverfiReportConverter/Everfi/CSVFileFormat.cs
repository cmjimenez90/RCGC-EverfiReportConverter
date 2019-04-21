using OfficeOpenXml;

namespace RCGC.EverfiReportConverter.Everfi
{
    public class CSVFileFormat
    {
        private ExcelTextFormat format;

        public CSVFileFormat(){
             this.format = new ExcelTextFormat();
        }

        public CSVFileFormat(char delimeter){
            this.format = new ExcelTextFormat();
            this.format.Delimiter = delimeter;
        }

        public CSVFileFormat(char delimeter, int skipBeginingLines){
            this.format = new ExcelTextFormat();
            this.format.Delimiter = delimeter;
            this.format.SkipLinesBeginning = skipBeginingLines;
        }

        public ExcelTextFormat getFormat(){
            return this.format;
        }
    }
}