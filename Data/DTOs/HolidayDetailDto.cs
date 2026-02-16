using HolidayApi.Data.Entities;
using HolidayApi.Data.ValueObjects;

namespace HolidayApi.Data.DTOs
{
    public class HolidayDetailDto
    {
        public string Name { get; }
        public string Date { get; }
        public HolidayType Type { get; }

        public HolidayDetailDto(string name, int month, int day, HolidayType type)
        {
            Name = name;
            Date = HolidayDate.Create(month, day).Value!.DateString;
            Type = type;
        }
    }
}
