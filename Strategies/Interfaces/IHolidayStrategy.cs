using HolidayApi.Data.DTOs;
using HolidayApi.ValueObjects;

namespace HolidayApi.Strategies
{
    public interface IHolidayStrategy
    {
        bool AppliesTo(IbgeCode ibgeCode);
        Task<IEnumerable<HolidayDto>> FindAllHolidays(int ibgeCode);
        Task<int> RegisterHoliday(int ibgeCode, HolidayDate date, string name);
        // Task<HolidayDto?> FindHoliday(UserLocation userLocation, HolidayDate date);
    }
}