using HolidayApi.Data.DTOs;
using HolidayApi.Data.Entities;
using HolidayApi.ResponseHandler;

namespace HolidayApi.Services.Interfaces
{
    public interface IStateService
    {
        Task<Result<int>> GetStateIdAsync(int ibgeCode);
        Task<Result<int>> FindStateIdAsync(int ibgeCode);
        Task<Result<int>> SaveState(StateDto state);
    }
}