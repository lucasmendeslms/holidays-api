namespace HolidayApi.Data.Entities
{
    public class State
    {
        public int Id { get; }
        public required string Name { get; set; }
        public required string Abbreviation { get; set; }
        public required int IbgeCode { get; set; }
        public required IEnumerable<Municipality> Municipalities { get; set; } = new List<Municipality>();
        public required IEnumerable<Holiday> Holidays { get; set; } = new List<Holiday>();
    }
}