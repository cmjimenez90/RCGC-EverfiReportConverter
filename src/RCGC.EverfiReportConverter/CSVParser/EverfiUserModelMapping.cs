namespace RCGC.EverfiReportConverter.CSVParser
{
    using TinyCsvParser.Mapping;
    using RCGC.EverfiReportConverter.CSVParser.Model;
    using RCGC.EverfiReportConverter.Configuration;

    public class EverfiUserModelMapping : CsvMapping<EverfiUser>
    {
        private enum CSV_FIELD_ENUM
        {
            FIRST_NAME,
            LAST_NAME,
            EMAIL,
            SUPERVISER,
            EMPLOYEE_ID,
            LOCATION_TITLE,
            LOCATION_ABR,
            GROUP_TITLE,
            GROUP_ABR
        }
        public EverfiUserModelMapping() : base()
        {
            MapProperty((int)CSV_FIELD_ENUM.FIRST_NAME, everfiUser => everfiUser.FIRST_NAME);
            MapProperty((int)CSV_FIELD_ENUM.LAST_NAME, everfiUser => everfiUser.LAST_NAME);
            MapProperty((int)CSV_FIELD_ENUM.EMAIL, everfiUser => everfiUser.EMAIL);
            MapProperty((int)CSV_FIELD_ENUM.SUPERVISER, everfiUser => everfiUser.SUPERVISOR);
            MapProperty((int)CSV_FIELD_ENUM.EMPLOYEE_ID, everfiUser => everfiUser.EMPLOYEE_ID);
            MapProperty((int)CSV_FIELD_ENUM.LOCATION_TITLE, everfiUser => everfiUser.LOCATION_TITLE);
            MapProperty((int)CSV_FIELD_ENUM.LOCATION_ABR, everfiUser => everfiUser.LOCATION_ABR);
            MapProperty((int)CSV_FIELD_ENUM.GROUP_TITLE, everfiUser => everfiUser.GROUP_TITLE);
            MapProperty((int)CSV_FIELD_ENUM.GROUP_ABR, everfiUser => everfiUser.GROUP_ABR);
        }           
    }
}