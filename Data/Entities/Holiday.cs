namespace HolidayApi.Data.Entities
{
    public class Holiday
    {
        public int Id { get; }
        public required string Name { get; set; }
        public DateTime Date { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public ICollection<State>? States { get; set; } = new List<State>();
        public IEnumerable<Municipality>? Municipalities { get; set; }
    }
}