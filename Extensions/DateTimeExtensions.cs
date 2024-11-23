using System.Globalization;

namespace GladLogsApi.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToIso8601String(this DateTime dateTime)
        {
            return dateTime.ToString("o",CultureInfo.InvariantCulture);
        }
    }
}
