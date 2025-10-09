using HolidayApi.Data.DTOs;
using HolidayApi.Repositories.Interfaces;
using HolidayApi.ResponseHandler;
using HolidayApi.Services.Interfaces;
using HolidayApi.Data.ValueObjects;

namespace HolidayApi.Strategies
{
    public class StateHolidayStrategy : IHolidayStrategy
    {
        private const int ONE_ROW_AFFECTED = 1;
        private readonly IHolidayRepository _holidayRepository;
        private readonly IStateService _stateService;

        public bool AppliesTo(IbgeCode ibgeCode) => ibgeCode.IsState;

        public StateHolidayStrategy(IHolidayRepository holidayRepository, IStateService stateService)
        {
            _holidayRepository = holidayRepository;
            _stateService = stateService;
        }

        public async Task<Result<int>> RegisterHolidayByIbgeCode(int ibgeCode, HolidayDate date, string name)
        {
            var holiday = await _holidayRepository.FindStateHoliday(ibgeCode, date);

            if (holiday is not null)
            {
                if (holiday.Name != name)
                {
                    var updateResult = await _holidayRepository.UpdateHolidayName(holiday.Id, name);

                    return updateResult == ONE_ROW_AFFECTED ? Result<int>.Success((int)OperationTypeCode.Update) : Result<int>.Failure(Error.HolidayUpdateFailed);
                }

                return Result<int>.Failure(Error.HolidayConflict);
            }

            var stateId = await _stateService.GetStateIdAsync(ibgeCode);

            if (stateId.IsFailure)
            {
                return stateId;
            }

            int result = await _holidayRepository.SaveStateHoliday(stateId.Value, date, name);

            return result == ONE_ROW_AFFECTED ? Result<int>.Success((int)OperationTypeCode.Create) : Result<int>.Failure(Error.SaveHolidayFailed);
        }

        public async Task<Result<IEnumerable<HolidayDetailDto>>> FindAllHolidaysByIbgeCode(int ibgeCode)
        {
            var result = await _holidayRepository.FindAllStateHolidays(ibgeCode);

            return result.Any() ? Result<IEnumerable<HolidayDetailDto>>.Success(result) : Result<IEnumerable<HolidayDetailDto>>.Failure(Error.HolidayNotFound);
        }

        public async Task<Result<HolidayDto>> FindHolidayByIbgeCodeAndDate(int ibgeCode, HolidayDate date)
        {
            var result = await _holidayRepository.FindStateHoliday(ibgeCode, date);

            return result is not null ? Result<HolidayDto>.Success(new HolidayDto(result.Name)) : Result<HolidayDto>.Failure(Error.HolidayNotFound);
        }

        public async Task<Result<int>> DeleteHolidayAsync(int ibgeCode, HolidayDate date)
        {
            var holiday = await _holidayRepository.FindStateHoliday(ibgeCode, date);

            if (holiday is null)
            {
                return Result<int>.Failure(Error.HolidayNotFound);
            }

            int result = await _holidayRepository.DeleteHolidayById(holiday.Id);

            return result == ONE_ROW_AFFECTED ? Result<int>.Success(result) : Result<int>.Failure(Error.DeleteHolidayFailed);
        }
    }
}