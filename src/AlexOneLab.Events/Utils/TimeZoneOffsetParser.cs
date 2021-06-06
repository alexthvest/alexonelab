using System;
using System.Text.RegularExpressions;

namespace AlexOneLab.Events.Utils
{
    public static class TimeZoneOffsetParser
    {
        public static bool TryParse(ReadOnlySpan<char> input, out TimeSpan offset)
        {
            var match = Regex.Match(input.ToString(), @"^GMT([+-](1[0-4]|[0-9]))$", RegexOptions.IgnoreCase);

            if (match.Success)
            {
                var hours = int.Parse(match.Groups[1].Value);
                offset = TimeSpan.FromHours(hours);

                return true;
            }

            return TimeSpan.TryParse(input, out offset);
        }
    }
}
