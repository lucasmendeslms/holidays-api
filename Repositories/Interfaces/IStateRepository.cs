using HolidayApi.Data.DTOs;
using HolidayApi.Data.Entities;

namespace HolidayApi.Repositories.Interfaces
{
    public interface IStateRepository
    {
        Task<State?> FindStateAsync(int ibgeCode);
        Task<int> SaveState(StateDto state);
    }
}