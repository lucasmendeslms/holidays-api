namespace HolidayApi.Data.Entities
{
    public abstract class LocationBase
    {
        public int Id { get; }
        public required string Name { get; set; }
        public required int IbgeCode { get; set; }

    }
}