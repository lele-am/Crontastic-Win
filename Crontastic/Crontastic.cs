using Crontastic.UI;
using Crontastic.vo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crontastic
{
    public class Crontastic
    {
        public static string Run(string cronExpression)
        {
            CrontasticUI UI = new CrontasticUI();
            UI.setCron (new Cron(cronExpression));
            UI.ShowDialog();
            Cron cron = UI.getCron();
            return cron.ToString();
        }
        public static string Run()
        {
            CrontasticUI UI = new CrontasticUI();
            UI.ShowDialog();
            Cron cron = UI.getCron();
            return cron.ToString();
        }
    }
}
