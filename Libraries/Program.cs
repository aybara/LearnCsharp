using System;
using System.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using MyClassLibrary.AzureServices.AzureTranslatorText;
using MyClassLibrary.Cache;
using MyClassLibrary.Queues;
using MyClassLibrary.Math;
using Newtonsoft.Json;
using MyClassLibrary;
using MyClassLibrary.AzureServices;
using MyClassLibrary.Calendar;

namespace Libraries
{
    class Program
    {
        static void Main(string[] args)
        {
            var duration = new TimeSpan(0, 960, 0);
            var offset = new TimeSpan(0, 360, 0);
            CalendarRulePattern pattern = new CalendarRulePattern("pattern", duration, offset);
            var now = DateTime.Now;
            var endDateTime = pattern.GetEndDateTime(now, new TimeSpan(16, 120, 0));
        }
    }
    
}
