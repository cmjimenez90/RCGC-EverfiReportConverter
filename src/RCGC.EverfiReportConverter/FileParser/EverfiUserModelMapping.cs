namespace RCGC.EverfiReportConverter.FileParser
{
    using TinyCsvParser.Mapping;
    using RCGC.EverfiReportConverter.Model;
    public class EverfiUserModelMapping : CsvMapping<EverfiUser>
    {
        public EverfiUserModelMapping() : base()
        {
            MapProperty(0, everfiUser => everfiUser.FirstName);
            MapProperty(1, everfiUser => everfiUser.LastName);
            MapProperty(2, everfiUser => everfiUser.Email);
            MapProperty(3, everfiUser => everfiUser.Supervisor);
            MapProperty(4, everfiUser => everfiUser.EmployeId);
        }
    }
}