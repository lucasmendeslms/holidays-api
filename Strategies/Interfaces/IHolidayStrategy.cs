using HolidayApi.Data.DTOs;
using HolidayApi.ResponseHandler;
using HolidayApi.Data.ValueObjects;

namespace HolidayApi.Strategies
{
    public interface IHolidayStrategy
    {
        bool AppliesTo(IbgeCode ibgeCode);
        Task<Result<int>> RegisterHolidayByIbgeCode(int ibgeCode, HolidayDate date, string name);
        Task<Result<IEnumerable<HolidayDetailDto>>> FindAllHolidaysByIbgeCode(int ibgeCode);
        Task<Result<HolidayDto>> FindHolidayByIbgeCodeAndDate(int ibgeCode, HolidayDate date);
        Task<Result<int>> DeleteHolidayAsync(int ibgeCode, HolidayDate date);
    }
}