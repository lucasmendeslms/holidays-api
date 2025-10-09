using HolidayApi.Data.ValueObjects;
using HolidayApi.Strategies;

namespace HolidayApi.Data
{
    public interface IRequestContext
    {
        IbgeCode IbgeCode { get; }
        HolidayDate Date { get; }
        IHolidayStrategy Strategy { get; }
    }

}