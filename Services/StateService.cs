using HolidayApi.Data.DTOs;
using HolidayApi.Data.Entities;
using HolidayApi.Data.ModelResponse;
using HolidayApi.Facades.Interfaces;
using HolidayApi.Repositories.Interfaces;

namespace HolidayApi.Services
{
    public class StateService
    {
        private readonly IStateRepository _stateRepository;
        private readonly IIbgeFacade _ibgeFacade;

        public StateService(IStateRepository stateRepository, IIbgeFacade ibgeFacade)
        {
            _stateRepository = stateRepository;
            _ibgeFacade = ibgeFacade;
        }

        public async Task<int> GetStateIdAsync(int ibgeCode)
        {
            State? state = await _stateRepository.FindStateAsync(ibgeCode);

            if (state is not null)
            {
                return state.Id;
            }

            StateDto ibgeApiResponse = await _ibgeFacade.GetIbgeStateAsync(ibgeCode);

            return await _stateRepository.SaveState(ibgeApiResponse);
        }
    }
}