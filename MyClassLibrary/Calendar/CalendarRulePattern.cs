using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClassLibrary.Calendar
{
    public class CalendarRulePattern
    {
        public string Pattern { get; }
        public TimeSpan Duration { get; }
        public TimeSpan Offset { get; set; }

        public CalendarRulePattern(string pattern, TimeSpan duration, TimeSpan offset)
        {
            Pattern = pattern;
            Duration = duration;
            Offset = offset;
        }
        public TimeSpan EndTime
        {
            get { return Duration + Offset; }
        }
        public DateTime GetEndDateTime(DateTime startDateTime, TimeSpan interval)
        {
            DateTime endDateTime = startDateTime;

            if (startDateTime.TimeOfDay <= Offset)
            {
                endDateTime += Offset - endDateTime.TimeOfDay;
            }
            else if (startDateTime.TimeOfDay > Offset && startDateTime.TimeOfDay <= EndTime)
            {
                interval += endDateTime.TimeOfDay - Offset;
                endDateTime -= endDateTime.TimeOfDay - Offset;
            }
            else
            {
                endDateTime += (new TimeSpan(0, 1440, 0) - endDateTime.TimeOfDay) + Offset;
            }
            return GetEndDateTimeAux(endDateTime, interval);
        }
        private DateTime GetEndDateTimeAux(DateTime startDateTime, TimeSpan interval)
        {
            if (interval > Duration)
            {
                startDateTime = startDateTime.AddDays(1);
                return GetEndDateTimeAux(startDateTime, interval - Duration);
            }
            return startDateTime + interval;
        }
    }
}
