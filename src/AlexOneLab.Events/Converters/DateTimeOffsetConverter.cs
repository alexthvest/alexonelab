using System;
using System.Linq;
using AlexOneLab.Events.Resources;
using AlexOneLab.Events.Utils;
using Replikit.Core.Controllers.Parameters;

namespace AlexOneLab.Events.Converters
{
    public class DateTimeOffsetConverter : IParameterConverter<DateTimeOffset>
    {
        public ConversionResult<DateTimeOffset> Convert(string parameter)
        {
            var dateTimeParts = parameter.Split("_").ToList();
            var dateTimeZoneIndex = dateTimeParts.FindIndex(p => p.StartsWith("GMT"));

            if (dateTimeZoneIndex != -1 && TimeZoneOffsetParser.TryParse(dateTimeParts[dateTimeZoneIndex], out var offset))
            {
                dateTimeParts[dateTimeZoneIndex] = (offset.Hours >= 0 ? "+" : "-") + offset.ToString("hh") + ":00";     
            }

            var dateTimeString = string.Join(" ", dateTimeParts);
            
            return DateTimeOffset.TryParse(dateTimeString, out var dateTime)
                ? ConversionResult.Success(dateTime)
                : ConversionResult.Error<DateTimeOffset>(Locale.InvalidDateFormat);
        }
    }
}
