using HolidayApi.ValueObjects;

namespace HolidayApi.Strategies
{
    public class HolidayStrategyContext
    {
        private readonly IEnumerable<IHolidayStrategy> _holidayStrategies;

        public HolidayStrategyContext(IEnumerable<IHolidayStrategy> strategies)
        {
            _holidayStrategies = strategies;
        }

        public IHolidayStrategy? SetStrategy(IbgeCode ibgeCode)
        {
            var strategy = _holidayStrategies.FirstOrDefault(s => s.AppliesTo(ibgeCode));
            return strategy;
        }
    }
}