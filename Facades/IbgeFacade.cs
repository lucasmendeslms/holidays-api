using HolidayApi.Configurations.Settings;
using HolidayApi.Data.DTOs;
using HolidayApi.Data.ExternalModels;
using HolidayApi.Facades.Interfaces;


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

        public async Task<StateDto> GetIbgeStateAsync(int ibgeCode)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_appSettings.ApiSettings.IbgeApi.StateEndpoint}/{ibgeCode}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Failed to get a state in Ibge API | GetIbgeStateAsync");
                }

                var modelResponse = await response.Content.ReadFromJsonAsync<StateModelResponse>();

                if (modelResponse is null)
                {
                    throw new Exception("Failed to convert the response of Ibge API to JSON | GetIbgeStateAsync");
                }

                return new StateDto(modelResponse.Id, modelResponse.StateCode, modelResponse.Name);

            }
            catch (Exception e)
            {
                throw new Exception($"Failed to execute the GetIbgeStateAsync function | {e.Message}");
            }

        }

        public async Task<MunicipalityReadDto> GetIbgeMunicipalityAsync(int ibgeCode)
        {
            try
            {
                var response = await _httpClient.GetAsync($"{_appSettings.ApiSettings.IbgeApi.MunicipalityEndpoint}/{ibgeCode}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Failed to get a municipality in Ibge API | GetIbgeMunicipalityAsync");
                }

                var modelResponse = await response.Content.ReadFromJsonAsync<MunicipalityModelResponse>();

                if (modelResponse is null)
                {
                    throw new Exception("Failed to convert the response of Ibge API to JSON | GetIbgeMunicipalityAsync");
                }

                MunicipalityReadDto municipality = new MunicipalityReadDto(
                    modelResponse.Id,
                    modelResponse.Name,
                    new StateDto(
                        modelResponse.Microregion.Mesoregion.State.Id,
                        modelResponse.Microregion.Mesoregion.State.StateCode,
                        modelResponse.Microregion.Mesoregion.State.Name
                    )
                );

                return municipality;

            }
            catch (Exception e)
            {
                throw new Exception($"Failed to execute the GetIbgeStateAsync function | {e.Message}");
            }

        }

    }

}

// https://learn.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-9.0
//https://learn.microsoft.com/pt-br/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests#issues-with-the-original-httpclient-class-available-in-net
