using System.Globalization;
using HolidayApi.ResponseHandler;

namespace HolidayApi.ValueObjects

{
    public class HolidayDate
    {
        public DateOnly Date { get; }
        public string DateString { get; }

        private HolidayDate(DateOnly date, string dateString)
        {
            Date = date;
            DateString = dateString;
        }

        public static Result<HolidayDate> Create(string date)
        {
            var validationResult = ValidateDate(date);

            return validationResult.IsSuccess
                ? Result<HolidayDate>.Success(new HolidayDate(validationResult.Value, date))
                : Result<HolidayDate>.Failure(validationResult.Error!);
        }

        public static Result<HolidayDate> Create(int month, int day)
        {
            string formatted = $"{month:D2}-{day:D2}";
            return Create(formatted);
        }

        private static Result<DateOnly> ValidateDate(string date)
        {
            if (DateOnly.TryParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var fullDate))
            {
                return Result<DateOnly>.Success(fullDate);
            }

            if (DateOnly.TryParseExact(date, "MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var partialDate))
            {
                return Result<DateOnly>.Success(partialDate);
            }

            return Result<DateOnly>.Failure(Error.InvalidDate);
        }
    }
}
