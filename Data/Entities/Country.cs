namespace HolidayApi.Data.Entities
{
    public class Country : LocationBase
    {
        public required string CountryCode { get; set; }
        public required IEnumerable<Holiday> Holidays { get; set; } = new List<Holiday>();
        public required IEnumerable<State> States { get; set; } = new List<State>();
    }
}