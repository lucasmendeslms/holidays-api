namespace HolidayApi.Data.Entities
{
    public class State : LocationBase
    {
        public required string StateCode { get; set; }
        public IEnumerable<Municipality> Municipalities { get; set; } = new List<Municipality>();
        public IEnumerable<Holiday> Holidays { get; set; } = new List<Holiday>();
    }
}