using HolidayApi.Data.DTOs;
using HolidayApi.ResponseHandler;

namespace HolidayApi.Facades.Interfaces
{
    public interface IIbgeFacade
    {
        Task<Result<StateDto>> GetIbgeStateAsync(int ibgeCode);
        Task<Result<MunicipalityReadDto>> GetIbgeMunicipalityAsync(int ibgeCode);
    }
}