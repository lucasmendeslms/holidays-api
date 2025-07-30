using HolidayApi.Data.DTOs;
using HolidayApi.Data.Entities;
using HolidayApi.Repositories.Interfaces;
using HolidayApi.Services.Interfaces;
using HolidayApi.ValueObjects;

namespace HolidayApi.Strategies
{
    public class MunicipalityHolidayStrategy : IHolidayStrategy
    {

        private readonly IHolidayRepository _holidayRepository;
        private readonly IMunicipalityService _municipalityService;

        public bool AppliesTo(IbgeCode ibgeCode) => ibgeCode.IsMunicipality;

        public MunicipalityHolidayStrategy(IHolidayRepository holidayRepository, IMunicipalityService municipalityService)
        {
            _holidayRepository = holidayRepository;
            _municipalityService = municipalityService;
        }

        public async Task<int> RegisterHolidayByIbgeCode(int ibgeCode, HolidayDate date, string name)
        {
            Holiday? holiday = await _holidayRepository.FindMunicipalityHoliday(ibgeCode, date);

            if (holiday is not null)
            {
                if (holiday.Name != name)
                {
                    return await _holidayRepository.UpdateHolidayName(holiday.Id, name);
                }

                return StatusCodes.Status200OK;
            }

            int municipalityId = await _municipalityService.GetMunicipalityIdAsync(ibgeCode);

            return await _holidayRepository.SaveMunicipalityHoliday(municipalityId, date, name);
        }

        public async Task<IEnumerable<HolidayDetailDto>> FindAllHolidaysByIbgeCode(int ibgeCode)
        {
            return await _holidayRepository.FindAllMunicipalityHolidaysAsync(ibgeCode);
        }

        public async Task<HolidayDto?> FindHolidayByIbgeCodeAndDate(int ibgeCode, HolidayDate date)
        {
            var holiday = await _holidayRepository.FindMunicipalityHoliday(ibgeCode, date);

            return holiday is not null ? new HolidayDto(holiday.Name) : null;
        }

        public async Task<bool> DeleteHolidayAsync(int ibgeCode, HolidayDate date)
        {
            var holiday = await _holidayRepository.FindMunicipalityHoliday(ibgeCode, date);

            return holiday is not null ? await _holidayRepository.DeleteHolidayById(holiday.Id) : false;
        }
    }
}