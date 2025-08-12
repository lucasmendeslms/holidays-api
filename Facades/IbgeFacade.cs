using HolidayApi.Configurations.Settings;
using HolidayApi.Data.DTOs;
using HolidayApi.Data.Entities;
using HolidayApi.Data.ExternalModels;
using HolidayApi.Facades.Interfaces;
using HolidayApi.ResponseHandler;


namespace HolidayApi.Facades
{
    public class IbgeFacade : IIbgeFacade
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _appSettings;

        public IbgeFacade(HttpClient httpClient, AppSettings appSettings)
        {
            _httpClient = httpClient;
            _appSettings = appSettings;

            _httpClient.BaseAddress = new Uri(_appSettings.ApiSettings.IbgeApi.BaseUrl);
        }

        public async Task<Result<StateDto>> GetIbgeStateAsync(int ibgeCode)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_appSettings.ApiSettings.IbgeApi.StateEndpoint}/{ibgeCode}");

                if (!response.IsSuccessStatusCode)
                {
                    return Result<StateDto>.Failure(Error.IbgeServiceFailure);
                }

                var result = await response.Content.ReadFromJsonAsync<StateModelResponse>();

                if (result is null)
                {
                    return Result<StateDto>.Failure(Error.IbgeDeserializationFailure);
                }

                return Result<StateDto>.Success(new StateDto(result.Id, result.StateCode, result.Name));
            }
            catch
            {
                return Result<StateDto>.Failure(Error.IbgeServiceUnavailable);
            }

        }

        public async Task<Result<MunicipalityReadDto>> GetIbgeMunicipalityAsync(int ibgeCode)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_appSettings.ApiSettings.IbgeApi.MunicipalityEndpoint}/{ibgeCode}");

                if (!response.IsSuccessStatusCode)
                {
                    return Result<MunicipalityReadDto>.Failure(Error.IbgeServiceFailure);
                }

                var result = await response.Content.ReadFromJsonAsync<MunicipalityModelResponse>();

                if (result is null)
                {
                    return Result<MunicipalityReadDto>.Failure(Error.IbgeDeserializationFailure);
                }

                MunicipalityReadDto municipality = new MunicipalityReadDto(
                    result.Id,
                    result.Name,
                    new StateDto(
                        result.Microregion.Mesoregion.State.Id,
                        result.Microregion.Mesoregion.State.StateCode,
                        result.Microregion.Mesoregion.State.Name
                    )
                );

                return Result<MunicipalityReadDto>.Success(municipality);
            }
            catch
            {
                return Result<MunicipalityReadDto>.Failure(Error.IbgeServiceUnavailable);
            }

        }

    }

}

// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-9.0
//https://learn.microsoft.com/pt-br/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests#issues-with-the-original-httpclient-class-available-in-net
