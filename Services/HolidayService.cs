using HolidayApi.Data.DTOs;
using HolidayApi.Data.Entities;
using HolidayApi.Repositories.Interfaces;
using HolidayApi.Services.Interfaces;
using HolidayApi.ValueObjects;

namespace HolidayApi.Services
{
    public class HolidayService : IHolidayService
    {
        private readonly IHolidayRepository _repository;

        public HolidayService(IHolidayRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<HolidayDto>> FindAllHolidays(Location location)
        {
            if (location.IsMunicipality)
            {
                return await _repository.FindAllMunicipalityHolidays(location.IbgeCode);
            }

            return await _repository.FindAllStateHolidays(location.IbgeCode);
        }

        public async Task<HolidayDto?> FindHoliday(Location location, HolidayDate date)
        {
            Holiday? response;

            if (location.IsMunicipality)
            {
                response = await _repository.FindMunicipalityHoliday(location.IbgeCode, date);
            }
            else
            {
                response = await _repository.FindStateHoliday(location.IbgeCode, date);
            }

            return response != null ? new HolidayDto { Name = response.Name } : null;
        }

        public async Task<int> RegisterHoliday(Location location, HolidayDate date, string name)
        {
            Holiday? holiday = location.IsMunicipality ? await _repository.FindMunicipalityHoliday(location.IbgeCode, date) : await _repository.FindStateHoliday(location.IbgeCode, date); ;

            if (holiday != null)
            {
                if (holiday.Name != name)
                {
                    return await _repository.UpdateHolidayName(holiday.Id, name);
                }

                return StatusCodes.Status200OK;
            }

            return StatusCodes.Status201Created;
        }
    }
}