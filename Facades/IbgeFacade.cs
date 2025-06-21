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

                StateDto state = new StateDto
                {
                    IbgeCode = modelResponse.Id,
                    StateCode = modelResponse.StateCode,
                    Name = modelResponse.Name
                };

                return state;

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
