using HolidayApi.Data.DTOs;
using HolidayApi.Data.Entities;

namespace HolidayApi.Repositories.Interfaces
{
    public interface IMunicipalityRepository
    {
        Task<int> FindMunicipalityIdAsync(int ibgeCode);
        Task<int> SaveMunicipality(MunicipalityDto municipality);
    }
}