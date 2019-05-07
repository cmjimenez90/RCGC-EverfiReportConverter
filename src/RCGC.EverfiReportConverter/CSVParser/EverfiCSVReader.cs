using System.Linq;
using System.Text;
using System.Collections.Generic;
using RCGC.EverfiReportConverter.CSVParser.Model;
using TinyCsvParser;
using RCGC.EverfiReportConverter.Configuration;
using Serilog;
using System;

namespace RCGC.EverfiReportConverter.CSVParser
{

    public class EverfiCSVReader
    {
        private readonly char DEFAULT_DELIMETER = ',';
        private readonly bool DEFAULT_CONTAINS_HEADER = true;
        private readonly ILogger logger;
        private readonly CSVFieldOverrides fieldOverrides;

        private CsvParser<EverfiUser> parser;

        public EverfiCSVReader(ILogger logger){
            this.logger = logger;
            CsvParserOptions options = new CsvParserOptions(DEFAULT_CONTAINS_HEADER, DEFAULT_DELIMETER);
            parser = new CsvParser<EverfiUser>(options, new EverfiUserModelMapping());
            this.fieldOverrides = new CSVFieldOverrides();
        }
        public EverfiCSVReader(ILogger logger, CSVFieldOverrides fieldOverrides)
        {
            this.logger = logger;
            CsvParserOptions options = new CsvParserOptions(DEFAULT_CONTAINS_HEADER, DEFAULT_DELIMETER);
            parser = new CsvParser<EverfiUser>(options, new EverfiUserModelMapping());
            this.fieldOverrides = fieldOverrides;
        }   

        public IEnumerable<EverfiUser> ReadDataFromCsvFile(string fileName)
        {
            var result = parser.ReadFromFile(fileName, Encoding.ASCII).ToList();
            bool isSuccessfulParse = result.All(row => row.IsValid);
            if (isSuccessfulParse)
            {
                IEnumerable<EverfiUser> users =  result.Select(row => row.Result);
                return ApplyOverrides(users);
            }
                return Enumerable.Empty<EverfiUser>();           
        }

        private IEnumerable<EverfiUser> ApplyOverrides(IEnumerable<EverfiUser> users)
        {
            return users.Select(user =>
            {
                foreach (var field in fieldOverrides.Fields)
                {
                    try {
                        user.GetType().GetProperty(field.Key.ToString()).SetValue(user, field.Value.ToString());
                    }
                    catch(Exception ex)
                    {
                        logger.Warning("Failed to override requested field: {0} with value of {1} | Reason: {2}", field.Key, field.Value, ex);                    
                    }              
                }
                return user;
            });
        }
    }
}