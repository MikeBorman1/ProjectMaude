using Humanizer;
using Humanizer.Localisation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CPMA_Core_APP.Utils
{
    public static class TimeUtils
    {
        public static string HumanizeTimeSpan(TimeSpan input)
        {
            if (input.TotalSeconds < 60)
            {
                return "under a minute";
            }
            else
            {
                var output = input.Humanize(2, minUnit: TimeUnit.Minute, maxUnit: TimeUnit.Hour, collectionSeparator: ", ");
                return output;
            }
        }

    }
}
