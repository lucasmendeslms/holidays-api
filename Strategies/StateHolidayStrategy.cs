using HolidayApi.Data.DTOs;
using HolidayApi.Data.Entities;
using HolidayApi.Facades.Interfaces;
using HolidayApi.Repositories.Interfaces;
using HolidayApi.Services.Interfaces;
using HolidayApi.ValueObjects;

namespace HolidayApi.Strategies
{
    public class StateHolidayStrategy : IHolidayStrategy
    {

        private readonly IHolidayRepository _holidayRepository;
        private readonly IStateService _stateService;

        public bool AppliesTo(IbgeCode ibgeCode) => ibgeCode.IsState;

        public StateHolidayStrategy(IHolidayRepository holidayRepository, IStateService stateService)
        {
            _holidayRepository = holidayRepository;
            _stateService = stateService;
        }

        public async Task<int> RegisterHolidayByIbgeCode(int ibgeCode, HolidayDate date, string name)
        {
            Holiday? holiday = await _holidayRepository.FindStateHoliday(ibgeCode, date);

            if (holiday is not null)
            {
                if (holiday.Name != name)
                {
                    return await _holidayRepository.UpdateHolidayName(holiday.Id, name);
                }

                return StatusCodes.Status200OK;
            }

            int stateId = await _stateService.GetStateIdAsync(ibgeCode);

            return await _holidayRepository.SaveStateHoliday(stateId, date, name);
        }

        public async Task<IEnumerable<HolidayDetailDto>> FindAllHolidaysByIbgeCode(int ibgeCode)
        {
            return await _holidayRepository.FindAllStateHolidays(ibgeCode);
        }

        public async Task<HolidayDto?> FindHolidayByIbgeCodeAndDate(int ibgeCode, HolidayDate date)
        {
            var holiday = await _holidayRepository.FindStateHoliday(ibgeCode, date);

            return holiday is not null ? new HolidayDto(holiday.Name) : null;
        }
    }
}