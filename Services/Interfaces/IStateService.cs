namespace HolidayApi.Services.Interfaces
{
    public interface IStateService
    {
        Task<int> GetStateIdAsync(int ibgeCode);
    }
}