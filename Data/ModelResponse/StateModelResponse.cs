namespace HolidayApi.Data.ModelResponse
{
    public class StateModelResponse
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string StateCode { get; set; }
        public required IbgeRegion Region { get; set; }
    }

    public class IbgeRegion
    {
        public required int Id { get; set; }
        public required string Name { get; set; }
        public required string RegionCode { get; set; }
    }
}