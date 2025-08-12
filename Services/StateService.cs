using HolidayApi.Data.DTOs;
using HolidayApi.Data.Entities;
using HolidayApi.Facades.Interfaces;
using HolidayApi.Repositories.Interfaces;
using HolidayApi.ResponseHandler;
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

        public async Task<Result<int>> FindStateIdAsync(int ibgeCode)
        {
            int result = await _stateRepository.FindStateIdAsync(ibgeCode);

            return result != 0 ? Result<int>.Success(result) : Result<int>.Failure(Error.StateNotFound);
        }

        public async Task<Result<int>> SaveState(StateDto state)
        {
            int result = await _stateRepository.SaveState(state);

            return result == 1 ? Result<int>.Success(result) : Result<int>.Failure(Error.SaveStateFailed);
        }

        public async Task<Result<int>> GetStateIdAsync(int ibgeCode)
        {
            var findState = await FindStateIdAsync(ibgeCode);

            if (findState.IsSuccess)
            {
                return findState;
            }

            var ibgeResult = await _ibgeFacade.GetIbgeStateAsync(ibgeCode);

            return ibgeResult.IsSuccess && ibgeResult.Value is not null ?
                await SaveState(new StateDto(ibgeResult.Value.IbgeCode, ibgeResult.Value.StateCode, ibgeResult.Value.Name))
                : Result<int>.Failure(Error.StateNotFound);
        }
    }
}