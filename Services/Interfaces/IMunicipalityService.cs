using HolidayApi.ResponseHandler;

namespace HolidayApi.Services.Interfaces
{
    public interface IMunicipalityService
    {
        Task<Result<int>> GetMunicipalityIdAsync(int ibgeCode);
        Task<Result<int>> FindMunicipalityIdAsync(int ibgeCode);
    }
}