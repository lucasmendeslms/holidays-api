using HolidayApi.Data.ValueObjects;
using HolidayApi.Strategies;

namespace HolidayApi.Data.Models
{
    public class RequestContext : IRequestContext
    {
        public IbgeCode IbgeCode { get; init; }
        public HolidayDate Date { get; init; }
        public IHolidayStrategy Strategy { get; init; }

        public RequestContext(IbgeCode ibgeCode, HolidayDate date, IHolidayStrategy strategy)
        {
            IbgeCode = ibgeCode;
            Date = date;
            Strategy = strategy;
        }
    }

}