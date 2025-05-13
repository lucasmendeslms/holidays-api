using HolidayApi.Data.DTOs;
using HolidayApi.ValueObjects;

namespace HolidayApi.Services.Interfaces
{
    public interface IHolidayService
    {
        Task<IEnumerable<HolidayDto>> FindAllHolidays(Location location);
        Task<int> RegisterHoliday(Location location, HolidayDate date, string name);
        Task<HolidayDto?> FindHoliday(Location location, HolidayDate date);
    }
}