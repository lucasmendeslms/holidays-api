using HolidayApi.Data.DTOs;
using HolidayApi.ValueObjects;

namespace HolidayApi.Strategies
{
    public interface IHolidayStrategy
    {
        bool AppliesTo(IbgeCode ibgeCode);
        Task<int> RegisterHolidayByIbgeCode(int ibgeCode, HolidayDate date, string name);
        Task<IEnumerable<HolidayDetailDto>> FindAllHolidaysByIbgeCode(int ibgeCode);
        Task<HolidayDto?> FindHolidayByIbgeCodeAndDate(int ibgeCode, HolidayDate date);
    }
}