
using Spectre.Console;

namespace Services
{
    public class Validation
    {
        public static bool IsValidDateTime(string input, string expectedFormat)
        {
            if (DateTime.TryParseExact(input, expectedFormat, null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
            {
                return parsedDate <= DateTime.Now;
            }
            return false;
        }

        public static bool IsValidTimeRange(DateTime startTime, DateTime endTime)
        {
            if (endTime <= startTime)
            {
                return false;
            }
            return true;
        }

        public static bool IsESCkeyPressed(ConsoleKeyInfo key)
        {
            return key.Key == ConsoleKey.Escape;
        }
    }
}
