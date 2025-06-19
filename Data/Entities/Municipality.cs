namespace HolidayApi.Data.Entities
{
    public class Municipality : LocationBase
    {
        public required int StateId { get; set; }
        public required State State { get; set; }
        public IEnumerable<Holiday> Holidays { get; set; } = new List<Holiday>();
    }
}