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
            MapProperty((int)CSV_FIELD_ENUM.FIRST_NAME, everfiUser => everfiUser.FirstName);
            MapProperty((int)CSV_FIELD_ENUM.LAST_NAME, everfiUser => everfiUser.LastName);
            MapProperty((int)CSV_FIELD_ENUM.EMAIL, everfiUser => everfiUser.Email);
            MapProperty((int)CSV_FIELD_ENUM.SUPERVISER, everfiUser => everfiUser.Supervisor);
            MapProperty((int)CSV_FIELD_ENUM.EMPLOYEE_ID, everfiUser => everfiUser.EmployeId);
            MapProperty((int)CSV_FIELD_ENUM.LOCATION_TITLE, everfiUser => everfiUser.LocationTitle);
            MapProperty((int)CSV_FIELD_ENUM.LOCATION_ABR, everfiUser => everfiUser.LocationAbr);
            MapProperty((int)CSV_FIELD_ENUM.GROUP_TITLE, everfiUser => everfiUser.GroupTitle);
            MapProperty((int)CSV_FIELD_ENUM.GROUP_ABR, everfiUser => everfiUser.GroupAbr);
        }           
    }
}