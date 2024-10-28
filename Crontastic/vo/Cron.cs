using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crontastic.vo
{
    public class Cron
    {
        public string Minutes { get; }
        public string Hours { get; }
        public string DaysOfMonth { get; }
        public string Months { get; }
        public string DaysOfWeek { get; }

        private static readonly Dictionary<string, int> MonthMap = new Dictionary<string, int>
        {
            { "JAN", 1 }, { "FEB", 2 }, { "MAR", 3 }, { "APR", 4 },
            { "MAY", 5 }, { "JUN", 6 }, { "JUL", 7 }, { "AUG", 8 },
            { "SEP", 9 }, { "OCT", 10 }, { "NOV", 11 }, { "DEC", 12 }
        };

        private static readonly Dictionary<string, int> DayOfWeekMap = new Dictionary<string, int>
        {
            { "SUN", 0 }, { "MON", 1 }, { "TUE", 2 }, { "WED", 3 },
            { "THU", 4 }, { "FRI", 5 }, { "SAT", 6 }
        };

        public Cron(string cronExpression)
        {
            var cronParts = cronExpression.Split(' ');
            if (cronParts.Length != 5)
            {
                throw new ArgumentException("Cron expression must have 5 parts");
            }

            Minutes = ValidateCronPart(cronParts[0], 0, 59);
            Hours = ValidateCronPart(cronParts[1], 0, 23);
            DaysOfMonth = ValidateCronPart(cronParts[2], 1, 31);
            Months = ValidateCronPart(cronParts[3], 1, 12, MonthMap);
            DaysOfWeek = ValidateCronPart(cronParts[4], 0, 6, DayOfWeekMap);
        }

        private string ValidateCronPart(string cronPart, int minValue, int maxValue, Dictionary<string, int> nameMap = null)
        {
            if (cronPart == "*")
            {
                return cronPart;
            }

            var parts = cronPart.Split(',');
            foreach (var part in parts)
            {
                var value = part;
                if (nameMap != null && nameMap.ContainsKey(part.ToUpper()))
                {
                    value = nameMap[part.ToUpper()].ToString();
                }

                if (value.Contains("/"))
                {
                    var stepParts = value.Split('/');
                    if (stepParts.Length != 2 || !int.TryParse(stepParts[1], out int step) || step <= 0)
                    {
                        throw new ArgumentException($"Invalid step value in cron part: {cronPart}");
                    }
                    var range = stepParts[0] == "*" ? $"{minValue}-{maxValue}" : stepParts[0];
                    var rangeParts = range.Split('-');
                    if (rangeParts.Length != 2 || !int.TryParse(rangeParts[0], out int start) || !int.TryParse(rangeParts[1], out int end) || start < minValue || end > maxValue || start > end)
                    {
                        throw new ArgumentException($"Invalid range in cron part: {cronPart}");
                    }
                }
                else if (value.Contains("-"))
                {
                    var rangeParts = value.Split('-');
                    if (rangeParts.Length != 2 || !int.TryParse(rangeParts[0], out int start) || !int.TryParse(rangeParts[1], out int end) || start < minValue || end > maxValue || start > end)
                    {
                        throw new ArgumentException($"Invalid range in cron part: {cronPart}");
                    }
                }
                else if (!int.TryParse(value, out int intValue) || intValue < minValue || intValue > maxValue)
                {
                    throw new ArgumentException($"Invalid value in cron part: {cronPart}");
                }
            }

            return cronPart;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3} {4}", Minutes, Hours, DaysOfMonth, Months, DaysOfWeek);
        }
    }
}