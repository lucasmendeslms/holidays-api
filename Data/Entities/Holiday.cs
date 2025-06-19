namespace HolidayApi.Data.Entities
{
    public class Holiday
    {
        public int Id { get; }
        public required string Name { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public HolidayType Type { get; set; }
        public int? StateId { get; set; }
        public int? MunicipalityId { get; set; }
        public int? CountryId { get; set; }
        public State? State { get; set; }
        public Municipality? Municipality { get; set; }
        public Country? Country { get; set; }
    }

    public enum HolidayType
    {
        National = 1,
        State = 2,
        Municipal = 3
    }
}