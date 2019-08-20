using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Linq;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Tooling.Connector;
using Newtonsoft.Json;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using DynamicsSLA.Services;
using DynamicsSLA.Earlybound;

namespace DynamicsSLA.Bagunça
{
    class Bagunça
    {
    }
    public class CalendarHandler
    {
        public Entity Calendar { get; set; }
        private readonly CrmServiceClient Service;
        public CalendarHandler(Entity calendar, CrmServiceClient service)
        {
            if (calendar.LogicalName != "calendar")
                throw new ArgumentException("LogicalName errado!!!");
            Calendar = calendar;
            Service = service;
        }

        public List<CalendarRulePattern> GetCalendarRulesUseFul()
        {
            List<CalendarRulePattern> calendarRulePatterns = new List<CalendarRulePattern>();
            foreach (Entity calendarRule in Calendar.GetAttributeValue<EntityCollection>("calendarrules").Entities)
            {
                calendarRulePatterns.Add(GetCalendarRuleUseFul(calendarRule));
            }
            return calendarRulePatterns;
        }
        private CalendarRulePattern GetCalendarRuleUseFul(Entity calendarRule)
        {
            if (calendarRule != null)
            {
                List<Entity> innerCalendarRules = GetInnerCalendarRule(calendarRule);
                List<CalendarRuleInterval> innerCalendarRuleIntervals = new List<CalendarRuleInterval>();
                for (int i = 1; i < innerCalendarRules.Count; i++)
                    innerCalendarRuleIntervals.Add(innerCalendarRules[i]);

                return new CalendarRulePattern(calendarRule.GetAttributeValue<string>("pattern"),
                                               new TimeSpan(0, innerCalendarRules[0].GetAttributeValue<int>("duration"), 0),
                                               new TimeSpan(0, innerCalendarRules[0].GetAttributeValue<int>("offset"), 0),
                                               innerCalendarRuleIntervals);
            }
            return null;
        }
        private List<Entity> GetInnerCalendarRule(Entity calendarRule)
        {
            Entity innerCalendar = Service.Retrieve("calendar", calendarRule.GetAttributeValue<EntityReference>("innercalendarid").Id, new ColumnSet(true));
            DataCollection<Entity> innerCalendarRules = innerCalendar.GetAttributeValue<EntityCollection>("calendarrules").Entities;

            return innerCalendarRules.ToList();
        }
    }
    /// <summary>
    /// Regra do calendário simplificado
    /// </summary>
    public class CalendarRulePattern
    {
        public string Pattern { get; }
        public TimeSpan Duration { get; }
        public TimeSpan Offset { get; }
        public List<CalendarRuleInterval> CalendarRuleIntervals { get; }

        public CalendarRulePattern(string pattern, TimeSpan duration, TimeSpan offset, List<CalendarRuleInterval> calendarRuleIntervals)
        {
            Pattern = pattern;
            Duration = duration;
            Offset = offset;
            CalendarRuleIntervals = calendarRuleIntervals;
        }
        public TimeSpan EndTime
        {
            get { return Duration + Offset; }
        }
        public DateTime GetEndDateTime(DateTime startDateTime, TimeSpan span)
        {
            DateTime endDateTime = startDateTime;

            if (startDateTime.TimeOfDay <= Offset)
            {
                endDateTime += Offset - endDateTime.TimeOfDay;
            }
            else if (startDateTime.TimeOfDay > Offset && startDateTime.TimeOfDay <= EndTime)
            {
                span += endDateTime.TimeOfDay - Offset;
                foreach (var calendarRuleInterval in CalendarRuleIntervals)
                {

                }
                endDateTime -= endDateTime.TimeOfDay - Offset;
            }
            else
            {
                endDateTime += new TimeSpan(0, 1440, 0) - endDateTime.TimeOfDay + Offset;
            }
            return GetEndDateTimeAux(endDateTime, span);
        }
        private DateTime GetEndDateTimeAux(DateTime startDateTime, TimeSpan span)
        {
            if (span > Duration)
            {
                startDateTime = startDateTime.AddDays(1);
                return GetEndDateTimeAux(startDateTime, span - Duration);
            }
            return startDateTime + span;
        }
    }
    public class CalendarRuleInterval
    {
        public TimeSpan Duration { get; }
        public TimeSpan Offset { get; }

        public CalendarRuleInterval(TimeSpan duration, TimeSpan offset)
        {
            Duration = duration;
            Offset = offset;
        }
        public static implicit operator CalendarRuleInterval(Entity entity)
        {
            if (entity.LogicalName == "calendarrule")
            {
                var duration = new TimeSpan(0, entity.GetAttributeValue<int>("duration"), 0);
                var offset = new TimeSpan(0, entity.GetAttributeValue<int>("offset"), 0);
                var calendarRuleInterval = new CalendarRuleInterval(duration, offset);
                return calendarRuleInterval;
            }
            return null;
        }
    }
}
