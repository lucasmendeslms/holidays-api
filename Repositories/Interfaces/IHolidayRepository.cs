using HolidayApi.Data.DTOs;
using HolidayApi.Data.Entities;
using HolidayApi.ResponseHandler;
using HolidayApi.ValueObjects;

namespace HolidayApi.Repositories.Interfaces
{
    public interface IHolidayRepository
    {
        Task<IEnumerable<HolidayDetailDto>> FindAllStateHolidays(int ibgeCode);
        Task<IEnumerable<HolidayDetailDto>> FindAllMunicipalityHolidays(int ibgeCode);
        Task<Result<Holiday>> FindMunicipalityHoliday(int ibgeCode, HolidayDate date);
        Task<Holiday?> FindStateHoliday(int ibgeCode, HolidayDate date);
        Task<int> UpdateHolidayName(int id, string name);
        Task<int> SaveStateHoliday(int stateId, HolidayDate date, string name);
        Task<int> SaveMunicipalityHoliday(int municipalityId, HolidayDate date, string name);
        Task<Result<bool>> DeleteHolidayById(int id);
    }
}