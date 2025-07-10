using HolidayApi.Data.DTOs;
using HolidayApi.Data.Entities;
using HolidayApi.Facades.Interfaces;
using HolidayApi.Repositories.Interfaces;
using HolidayApi.Services.Interfaces;

namespace HolidayApi.Services
{
    public class StateService : IStateService
    {
        private readonly IStateRepository _stateRepository;
        private readonly IIbgeFacade _ibgeFacade;

        public StateService(IStateRepository stateRepository, IIbgeFacade ibgeFacade)
        {
            _stateRepository = stateRepository;
            _ibgeFacade = ibgeFacade;
        }

        public async Task<int> FindStateIdAsync(int ibgeCode)
        {
            return await _stateRepository.FindStateIdAsync(ibgeCode);
        }

        public async Task<int> SaveState(StateDto state)
        {
            return await _stateRepository.SaveState(state);
        }

        public async Task<int> GetStateIdAsync(int ibgeCode)
        {
            int stateId = await FindStateIdAsync(ibgeCode);

            if (stateId is not 0)
            {
                return stateId;
            }

            StateDto ibgeApiResponse = await _ibgeFacade.GetIbgeStateAsync(ibgeCode);

            return await SaveState(ibgeApiResponse);
        }
    }
}