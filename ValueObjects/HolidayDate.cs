using System.Globalization;

namespace HolidayApi.ValueObjects

{
    public class HolidayDate
    {
        public DateOnly Date { get; }
        public string DateString { get; }

        public HolidayDate(string date)
        {
            Date = ParseDate(date);
            DateString = date;
        }

        public HolidayDate(int month, int day)
        {
            string formatted = $"{month:D2}-{day:D2}";
            Date = ParseDate(formatted);
            DateString = formatted;
        }

        private static DateOnly ParseDate(string date)
        {
            if (DateOnly.TryParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var fullDate))
            {
                return fullDate;
            }

            if (DateOnly.TryParseExact(date, "MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var partialDate))
            {
                return partialDate;
            }

            throw new ArgumentException("Formato inválido de data", nameof(date));
        }

        public override string ToString() => DateString;
    }
}
