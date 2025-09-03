using HolidayApi.Data.DTOs;
using HolidayApi.Repositories.Interfaces;
using HolidayApi.ResponseHandler;
using HolidayApi.Services.Interfaces;
using HolidayApi.ValueObjects;

namespace HolidayApi.Strategies
{
    public class MunicipalityHolidayStrategy : IHolidayStrategy
    {
        private readonly IHolidayRepository _holidayRepository;
        private readonly IMunicipalityService _municipalityService;

        public bool AppliesTo(IbgeCode ibgeCode) => ibgeCode.IsMunicipality;

        public MunicipalityHolidayStrategy(IHolidayRepository holidayRepository, IMunicipalityService municipalityService)
        {
            _holidayRepository = holidayRepository;
            _municipalityService = municipalityService;
        }

        public async Task<Result<int>> RegisterHolidayByIbgeCode(int ibgeCode, HolidayDate date, string name)
        {
            var holiday = await _holidayRepository.FindStateHoliday(ibgeCode, date);

            if (holiday is not null)
            {
                if (holiday.Name != name)
                {
                    var updateResult = await _holidayRepository.UpdateHolidayName(holiday.Id, name);

                    return updateResult == 1 ? Result<int>.Success((int)OperationTypeCode.Update) : Result<int>.Failure(Error.HolidayUpdateFailed);
                }

                return Result<int>.Failure(Error.HolidayConflict);
            }

            var municipalityId = await _municipalityService.GetMunicipalityIdAsync(ibgeCode);

            if (municipalityId.IsFailure)
            {
                return municipalityId;
            }

            int result = await _holidayRepository.SaveStateHoliday(municipalityId.Value, date, name);

            return result == 1 ? Result<int>.Success((int)OperationTypeCode.Create) : Result<int>.Failure(Error.SaveHolidayFailed);
        }

        public async Task<Result<IEnumerable<HolidayDetailDto>>> FindAllHolidaysByIbgeCode(int ibgeCode)
        {
            var result = await _holidayRepository.FindAllMunicipalityHolidays(ibgeCode);

            return result.Any() ? Result<IEnumerable<HolidayDetailDto>>.Success(result) : Result<IEnumerable<HolidayDetailDto>>.Failure(Error.HolidayNotFound);
        }

        public async Task<Result<HolidayDto>> FindHolidayByIbgeCodeAndDate(int ibgeCode, HolidayDate date)
        {
            var result = await _holidayRepository.FindMunicipalityHoliday(ibgeCode, date);

            return result is not null ? Result<HolidayDto>.Success(new HolidayDto(result.Name)) : Result<HolidayDto>.Failure(Error.HolidayNotFound);
        }

        public async Task<Result<int>> DeleteHolidayAsync(int ibgeCode, HolidayDate date)
        {
            var holiday = await _holidayRepository.FindMunicipalityHoliday(ibgeCode, date);

            if (holiday is null)
            {
                return Result<int>.Failure(Error.HolidayNotFound);
            }

            int result = await _holidayRepository.DeleteHolidayById(holiday.Id);

            return result == 1 ? Result<int>.Success(result) : Result<int>.Failure(Error.DeleteHolidayFailed);
        }
    }
}