using HolidayApi.Data.DTOs;
using HolidayApi.Data.Entities;
using HolidayApi.Facades.Interfaces;
using HolidayApi.Repositories.Interfaces;
using HolidayApi.ResponseHandler;
using HolidayApi.Services.Interfaces;

namespace HolidayApi.Services
{
    public class MunicipalityService : IMunicipalityService
    {
        private readonly IMunicipalityRepository _municipalityRepository;
        private readonly IIbgeFacade _ibgeFacade;
        private readonly IStateService _stateService;
        private const int NO_ROWS_AFFECTED = 0;

        public MunicipalityService(IMunicipalityRepository municipalityRepository, IIbgeFacade ibgeFacade, IStateService stateService)
        {
            _municipalityRepository = municipalityRepository;
            _ibgeFacade = ibgeFacade;
            _stateService = stateService;
        }

        public async Task<Result<int>> FindMunicipalityIdAsync(int ibgeCode)
        {
            int result = await _municipalityRepository.FindMunicipalityIdAsync(ibgeCode);

            return result != NO_ROWS_AFFECTED ? Result<int>.Success(result) : Result<int>.Failure(Error.MunicipalityNotFound);
        }

        public async Task<Result<int>> GetMunicipalityIdAsync(int ibgeCode)
        {
            var findMunicipalityId = await FindMunicipalityIdAsync(ibgeCode);

            if (findMunicipalityId.IsSuccess)
            {
                return findMunicipalityId;
            }

            var ibgeResult = await _ibgeFacade.GetIbgeMunicipalityAsync(ibgeCode);

            if (ibgeResult.IsFailure || ibgeResult.Value is null)
            {
                return Result<int>.Failure(ibgeResult.Error ?? Error.MunicipalityNotFound);
            }

            var getStateId = await _stateService.FindStateIdAsync(ibgeResult.Value.State.IbgeCode);

            if (getStateId.IsFailure)
            {
                getStateId = await _stateService.SaveState(ibgeResult.Value.State);
            }

            if (getStateId.IsFailure)
            {
                return Result<int>.Failure(getStateId.Error!);
            }

            var result = await _municipalityRepository.SaveMunicipality(new MunicipalityDto(ibgeResult.Value.Name, ibgeResult.Value.IbgeCode, getStateId.Value));

            return result != NO_ROWS_AFFECTED ? Result<int>.Success(result) : Result<int>.Failure(Error.MunicipalityNotFound);
        }
    }
}