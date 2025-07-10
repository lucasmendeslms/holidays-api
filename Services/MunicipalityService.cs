using HolidayApi.Data.DTOs;
using HolidayApi.Data.Entities;
using HolidayApi.Facades.Interfaces;
using HolidayApi.Repositories.Interfaces;
using HolidayApi.Services.Interfaces;

namespace HolidayApi.Services
{
    public class MunicipalityService : IMunicipalityService
    {
        private readonly IMunicipalityRepository _municipalityRepository;
        private readonly IIbgeFacade _ibgeFacade;
        private readonly IStateService _stateService;

        public MunicipalityService(IMunicipalityRepository municipalityRepository, IIbgeFacade ibgeFacade, IStateService stateService)
        {
            _municipalityRepository = municipalityRepository;
            _ibgeFacade = ibgeFacade;
            _stateService = stateService;
        }
        public async Task<int> GetMunicipalityIdAsync(int ibgeCode)
        {
            int municipalityId = await _municipalityRepository.FindMunicipalityIdAsync(ibgeCode);

            if (municipalityId is not 0)
            {
                return municipalityId;
            }

            MunicipalityReadDto ibgeApiResponse = await _ibgeFacade.GetIbgeMunicipalityAsync(ibgeCode);

            int stateId = await _stateService.FindStateIdAsync(ibgeApiResponse.State.IbgeCode);

            if (stateId is 0)
            {
                stateId = await _stateService.SaveState(ibgeApiResponse.State);
            }

            MunicipalityDto municipality = new MunicipalityDto
            {
                IbgeCode = ibgeApiResponse.IbgeCode,
                Name = ibgeApiResponse.Name,
                StateId = stateId
            };

            return await _municipalityRepository.SaveMunicipality(municipality);
        }
    }
}