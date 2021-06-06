using System;
using AlexOneLab.Events.Resources;
using AlexOneLab.Events.Utils;
using Replikit.Core.Controllers.Parameters;

namespace AlexOneLab.Events.Converters
{
    public class TimeSpanConverter : IParameterConverter<TimeSpan>
    {
        public ConversionResult<TimeSpan> Convert(string parameter)
        {
            return TimeZoneOffsetParser.TryParse(parameter, out var timeSpan)
                ? ConversionResult.Success(timeSpan)
                : ConversionResult.Error<TimeSpan>(Locale.InvalidTimeFormat);
        }
    }
}
