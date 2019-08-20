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

namespace DynamicsSLA
{
    class Program
    {
        static void Main(string[] args)
        {
            //DateTime dateTime = DateTime.Now;
            //DateTime dateTime1 = dateTime;
            //dateTime1 = dateTime1.AddMinutes(20);
            //TimeSpan duration = new TimeSpan(0, 960, 0);
            //TimeSpan offset = new TimeSpan(0, 360, 0);
            //TimeSpan now = DateTime.Now.TimeOfDay;
            //CalendarRulePattern pattern = new CalendarRulePattern("pattern", duration, offset);

            //DateTime endTime = pattern.GetEndDateTime(DateTime.Now, new TimeSpan(18, 0, 0));

            string user = "leandro.coelho@smartconsulting.com.br";
            string pass = "2357111317192329313741434753@Flgc";
            string url = "https://smartcaixa.crm2.dynamics.com/";
            string connString = $"AuthType=Office365;Username={user};Password={pass};Url={url}";
            var crmService = new CrmServiceClient(connString);

            if (crmService.IsReady)
            {
                Console.WriteLine("Conectado!");
                Calendar calendar = null;
                using (var orgContext = new OrganizationServiceContext(crmService))
                {
                    List<Calendar> calendars = (from c in orgContext.CreateQuery<Calendar>()
                                                where c.Name.Contains("nível 1")
                                                select new Calendar
                                                {
                                                    Id = c.Id
                                                }).ToList();
                    calendar = calendars[0];
                    var calendarRules = calendar.GetAttributeValue<EntityCollection>("calendarrules").Entities.ToList();
                    var holydayCalendar = crmService.Retrieve("calendar", calendar.HolidayScheduleCalendarId.Id, new ColumnSet(true));
                    var holidayCalendarRules = holydayCalendar.GetAttributeValue<EntityCollection>("calendarrules").Entities.ToList();
                    //var innerCalendarholiday = crmService.Retrieve("calendar", holidayCalendarRules[0].GetAttributeValue<EntityReference>("innercalendarid").Id, new ColumnSet(true));
                    //var innerCalendarRulesholiday = innerCalendarholiday.GetAttributeValue<EntityCollection>("calendarrules").Entities.ToList();
                }
                var parameters = new ParameterCollection();
                parameters.Add("calendarId", calendar.Id.ToString());
                parameters.Add("startTimeLocal", DateTime.Now);
                parameters.Add("endTimeLocal", DateTime.Now.AddDays(7));

                var Action = new OrganizationRequest("smt_obtercalendario")
                {
                    Parameters = parameters
                };
                OrganizationResponse response = crmService.Execute(Action);

                var datas = JsonConvert.DeserializeObject<List<Calendario>>(response.Results.FirstOrDefault().Value.ToString()).Where(c => c.PSA_Day.Date >= DateTime.Now.Date).ToList();
            }
            else
                Console.WriteLine("Não Conectou!");

            Console.WriteLine("FIM!!!");
            //Console.ReadLine();
        }
    }

    public class CalendarHandler
    {
        public Entity Calendar { get; }
        private readonly CrmServiceClient Service;
        private readonly List<WorkDay> WorkDays;
        private readonly List<Holiday> Holydays;
        public CalendarHandler(Entity calendar, CrmServiceClient service)
        {
            if (calendar.LogicalName != "calendar")
                throw new ArgumentException("LogicalName errado!!!");
            Calendar = calendar;
            Service = service;
            Holydays = GetHolidays();
            List<Entity> calendarRules = GetCalendarRules();
            foreach(Entity calendarRule in calendarRules)
            {
                List<Entity> innerCalendarRules = GetInnerCalendarRules(calendarRule);
            }
        }
        private List<Entity> GetCalendarRules()
        {
            return Calendar.GetAttributeValue<EntityCollection>("calendarrules").Entities.ToList();
        }
        private List<Entity> GetInnerCalendarRules(Entity calendarRule)
        {
            Entity innerCalendar = Service.Retrieve("calendar", calendarRule.GetAttributeValue<EntityReference>("innercalendarid").Id, new ColumnSet(true));
            DataCollection<Entity> innerCalendarRules = innerCalendar.GetAttributeValue<EntityCollection>("calendarrules").Entities;

            return innerCalendarRules.ToList();
        }
        private List<Holiday> GetHolidays()
        {
            List<Entity> entities = RetrieveHolidays();
            List<Holiday> holidays = new List<Holiday>();
            foreach (var entity in entities)
                holidays.Add(entity);

            return holidays;
        }
        private List<Entity> RetrieveHolidays()
        {
            if (Calendar.Contains("holidayschedulecalendarid"))
            {
                var holydayCalendar = Service.Retrieve("calendar", Calendar.GetAttributeValue<EntityReference>("holidayschedulecalendarid").Id, new ColumnSet(true));
                return holydayCalendar.GetAttributeValue<EntityCollection>("calendarrules").Entities.ToList();
            }
            return null;
        }
    }

    public class WorkDay
    {
        public TimeSpan Start { get; }
        public TimeSpan End { get; }
        public List<WorkDayInterval> Intervals { get; }
        public WorkDay(TimeSpan start, TimeSpan end, List<WorkDayInterval> intervals)
        {
            Start = start;
            End = end;
            Intervals = intervals;
        }
    }
    public class WorkDayInterval
    {
        public TimeSpan Start { get; }
        public TimeSpan End { get; }
        public WorkDayInterval(TimeSpan start, TimeSpan end)
        {
            Start = start;
            End = end;
        }
    }
    public class Holiday
    {
        public int Duration { get; }
        public DateTime EffectiveIntervalStart { get; }
        public DateTime EffectiveIntervalEnd { get; }
        public Holiday(DateTime start, DateTime End, int duration)
        {
            EffectiveIntervalStart = start;
            EffectiveIntervalEnd = End;
            Duration = duration;
        }
        public static implicit operator Holiday(Entity entity)
        {
            if(entity.LogicalName == "calendarrule")
            {
                return new Holiday(entity.GetAttributeValue<DateTime>("effectiveintervalstart"),
                                   entity.GetAttributeValue<DateTime>("effectiveintervalend"),
                                   entity.GetAttributeValue<int>("duration"));
            }
            return null;
        }
    }
    public class Calendario
    {
        public DateTime PSA_Day;
        public decimal PSA_Duration;
        public DateTime PSA_EndTime;
        public DateTime PSA_EndTimeUtc;
        public DateTime PSA_Month;
        public DateTime PSA_Quarter;
        public DateTime PSA_StartTime;
        public DateTime PSA_StartTimeUtc;
        public DateTime PSA_Week;
        public string ResourceId;
        public int ResourceSubCode;
        public int ResourceTimeCode;
    }
}
