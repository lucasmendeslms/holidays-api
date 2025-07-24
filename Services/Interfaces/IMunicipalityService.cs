namespace HolidayApi.Services.Interfaces
{
    public interface IMunicipalityService
    {
        Task<int> GetMunicipalityIdAsync(int ibgeCode);
        Task<int> FindMunicipalityIdAsync(int ibgeCode);
    }
}