using HolidayApi.Data.Entities;

namespace HolidayApi.Data.DTOs
{
    public class HolidayDto
    {
        public string Name { get; }

        public HolidayDto(string name)
        {
            Name = name;
        }

        public static implicit operator HolidayDto(Holiday holiday)
        {
            return new HolidayDto(holiday.Name);
        }
    }
}