namespace HolidayApi.Data.Entities
{
    public class Municipality
    {
        public int Id { get; }
        public required string Name { get; set; }
        public required int IbgeCode { get; set; }
        public required int StateId { get; set; }
        public required IEnumerable<Holiday> Holidays { get; set; } = new List<Holiday>();
    }
}