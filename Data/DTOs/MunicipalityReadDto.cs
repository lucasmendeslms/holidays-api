namespace HolidayApi.Data.DTOs
{
    public class MunicipalityReadDto
    {
        public required int IbgeCode { get; set; }
        public required StateDto State { get; set; }
        public required string Name { get; set; }
    }
}