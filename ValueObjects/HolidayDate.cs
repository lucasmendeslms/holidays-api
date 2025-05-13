using System.ComponentModel;
using System.Globalization;

namespace HolidayApi.ValueObjects
{
    public class HolidayDate
    {
        public DateTime Date { get; private set; }

        public HolidayDate(string date)
        {
            DateTime parsedDate;

            if (DateTime.TryParseExact(date, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate) || DateTime.TryParseExact(date, "MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
            {
                Date = parsedDate;
                return;
            }

            throw new ArgumentException("Invalid date", date);

        }
    }
}