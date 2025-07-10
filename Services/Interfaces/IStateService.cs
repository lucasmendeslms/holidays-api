using HolidayApi.Data.DTOs;
using HolidayApi.Data.Entities;

namespace HolidayApi.Services.Interfaces
{
    public interface IStateService
    {
        Task<int> GetStateIdAsync(int ibgeCode);
        Task<int> FindStateIdAsync(int ibgeCode);
        Task<int> SaveState(StateDto state);
    }
}