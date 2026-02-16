using HolidayApi.ResponseHandler;
using HolidayApi.Data.ValueObjects;

namespace HolidayApi.Strategies
{
    public class HolidayStrategyContext
    {
        private readonly IEnumerable<IHolidayStrategy> _holidayStrategies;

        public HolidayStrategyContext(IEnumerable<IHolidayStrategy> strategies)
        {
            _holidayStrategies = strategies;
        }

        public Result<IHolidayStrategy> SetStrategy(IbgeCode ibgeCode)
        {
            var context = _holidayStrategies.FirstOrDefault(s => s.AppliesTo(ibgeCode));

            return context is not null
                ? Result<IHolidayStrategy>.Success(context)
                : Result<IHolidayStrategy>.Failure(Error.StrategyContextFailed);
        }

    }
}