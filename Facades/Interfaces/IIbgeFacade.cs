using HolidayApi.Data.DTOs;

namespace HolidayApi.Facades.Interfaces
{
    public interface IIbgeFacade
    {
        Task<StateDto> GetIbgeStateAsync(int ibgeCode);
        Task<MunicipalityReadDto> GetIbgeMunicipalityAsync(int ibgeCode);
    }
}