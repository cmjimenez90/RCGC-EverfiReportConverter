using System.Linq;
using System.Text;
using System.Collections.Generic;
using RCGC.EverfiReportConverter.CSVParser.Model;
using TinyCsvParser;
namespace RCGC.EverfiReportConverter.CSVParser
{

    public class EverfiCSVReader
    {
        private char DEFAULT_DELIMETER = ',';
        private bool DEFAULT_CONTAINS_HEADER = true;
        private CsvParser<EverfiUser> parser;

        public EverfiCSVReader(){
            CsvParserOptions options = new CsvParserOptions(DEFAULT_CONTAINS_HEADER, DEFAULT_DELIMETER);
            parser = new CsvParser<EverfiUser>(options, new EverfiUserModelMapping());
        }
        public EverfiCSVReader(char delimeter)
        {
            CsvParserOptions options = new CsvParserOptions(DEFAULT_CONTAINS_HEADER, delimeter);
            parser = new CsvParser<EverfiUser>(options, new EverfiUserModelMapping());
        }

        public EverfiCSVReader(bool hasHeader, char delimeter)
        {
            CsvParserOptions options = new CsvParserOptions(hasHeader, delimeter);
            parser = new CsvParser<EverfiUser>(options, new EverfiUserModelMapping());
        }

        public IEnumerable<EverfiUser> ReadDataFromCsvFile(string fileName)
        {
            var result = parser.ReadFromFile(fileName, Encoding.ASCII).ToList();
            bool isSuccessfulParse = result.All(row => row.IsValid);
            if (isSuccessfulParse)
            {
                return result.Select(row => row.Result);
            }
                return Enumerable.Empty<EverfiUser>();
            

        }
    }
}