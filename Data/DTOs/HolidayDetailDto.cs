using HolidayApi.Data.Entities;
using HolidayApi.ValueObjects;

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
            Date = new HolidayDate(month, day).DateString;
            Type = type;
        }
    }
}
