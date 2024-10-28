using Crontastic.vo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crontastic.vo;

namespace Crontastic.bo
{
    public class CronSchedule
    {
        private readonly Cron _cron;

        public CronSchedule(Cron cron)
        {
            _cron = cron;
        }

        public List<DateTime> GetTriggers(int triggerCount)
        {
            var triggers = new List<DateTime>();
            var currentTime = DateTime.Now;

            while (triggers.Count < triggerCount)
            {
                currentTime = currentTime.AddMinutes(1);
                if (IsTime(currentTime))
                {
                    triggers.Add(currentTime);
                }
            }

            return triggers;
        }

        private bool IsTime(DateTime dateTime)
        {
            return IsMatch(dateTime.Minute, _cron.Minutes) &&
                   IsMatch(dateTime.Hour, _cron.Hours) &&
                   IsMatch(dateTime.Day, _cron.DaysOfMonth) &&
                   IsMatch(dateTime.Month, _cron.Months) &&
                   IsMatch((int)dateTime.DayOfWeek, _cron.DaysOfWeek);
        }

        private bool IsMatch(int value, string cronPart)
        {
            if (cronPart == "*")
            {
                return true;
            }

            var parts = cronPart.Split(',');
            foreach (var part in parts)
            {
                if (part.Contains("/"))
                {
                    var stepParts = part.Split('/');
                    var range = stepParts[0] == "*" ? "0-" + (cronPart == _cron.Minutes ? "59" : cronPart == _cron.Hours ? "23" : cronPart == _cron.DaysOfMonth ? "31" : cronPart == _cron.Months ? "12" : "6") : stepParts[0];
                    var step = int.Parse(stepParts[1]);
                    var rangeParts = range.Split('-');
                    var start = int.Parse(rangeParts[0]);
                    var end = int.Parse(rangeParts[1]);

                    for (int i = start; i <= end; i += step)
                    {
                        if (i == value)
                        {
                            return true;
                        }
                    }
                }
                else if (part.Contains("-"))
                {
                    var rangeParts = part.Split('-');
                    var start = int.Parse(rangeParts[0]);
                    var end = int.Parse(rangeParts[1]);

                    if (value >= start && value <= end)
                    {
                        return true;
                    }
                }
                else if (int.TryParse(part, out int intValue))
                {
                    if (value == intValue)
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}
