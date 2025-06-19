namespace HolidayApi.Configurations.Settings
{
    public class AppSettings
    {
        public required ConnectionStrings ConnectionStrings { get; set; }
        public required ApiSettings ApiSettings { get; set; }
    }

    public class ConnectionStrings
    {
        public required string Database { get; set; }
    }

    public class ApiSettings
    {
        public required IbgeApiSettings IbgeApi { get; set; }

    }

    public class IbgeApiSettings
    {
        public required string BaseUrl { get; set; }
        public required string StateEndpoint { get; set; }
        public required string MunicipalityEndpoint { get; set; }
    }
}