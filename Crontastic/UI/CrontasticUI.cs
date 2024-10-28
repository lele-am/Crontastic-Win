using Crontastic.vo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crontastic.UI
{
    public partial class CrontasticUI : Form
    {
        public CrontasticUI()
        {
            InitializeComponent();
        }
        public Cron getCron()
        {
            vo.Cron cron = new vo.Cron(string.Format("{0} {1} {2} {3} {4}", textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text));
            return cron;
        }
        public void setCron(vo.Cron cron)
        {
            textBox1.Text = cron.Minutes;
            textBox2.Text = cron.Hours;
            textBox3.Text = cron.DaysOfMonth;
            textBox4.Text = cron.Months;
            textBox5.Text = cron.DaysOfWeek;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            vo.Cron cron = new vo.Cron(string.Format("{0} {1} {2} {3} {4}", textBox1.Text, textBox2.Text, textBox3.Text, textBox4.Text, textBox5.Text));
            bo.CronSchedule cronSchedule = new bo.CronSchedule(cron);
            string cronTriggers = "Your expression will trigger on:\n";
            int it = 1;
            foreach (DateTime triggerdate in cronSchedule.GetTriggers(10))
            {
                cronTriggers += "#" + it.ToString() + " " + triggerdate.ToLongTimeString() + " " + triggerdate.ToLongDateString() + "\n";
                it++;
            }
            MessageBox.Show(cronTriggers);
        }

        private void textBox1_MouseLeave(object sender, EventArgs e)
        {
            LFieldInfo.Text = "Cron expressions are used to schedule tasks. They consist of five fields: minute ('0-59'), hour ('0-23'), day of the month ('1-31'), month (1-12 (JAN-DEC)), and day of the week ('0-6' (SUN-SAT)). Special characters include '*' for any value, ',' for multiple values, '-' for ranges, and '/' for steps. For example, '0 12 * * *' runs at noon every day";
        }

        private void textBox2_MouseMove(object sender, MouseEventArgs e)
        {
            LFieldInfo.Text = "Hour (0-23): Use '*' for any value, ',' for multiple values, '-' for ranges, and '/' for steps.";
        }

        private void textBox1_MouseMove(object sender, MouseEventArgs e)
        {
            LFieldInfo.Text = "Minute (0-59): Use '*' for any value, ',' for multiple values, '-' for ranges, and '/' for steps.";
        }

        private void textBox3_MouseMove(object sender, MouseEventArgs e)
        {
            LFieldInfo.Text = "Day of the Month ('1-31'): Use '*' for any value, ',' for multiple values, '-' for ranges, and '/' for steps.";
        }

        private void textBox4_MouseMove(object sender, MouseEventArgs e)
        {
            LFieldInfo.Text = "Month (1-12): Use '*' for any value, ',' for multiple values, '-' for ranges, and '/' for steps.";
        }

        private void textBox5_MouseMove(object sender, MouseEventArgs e)
        {
            LFieldInfo.Text = "Day of the Week ('0-6'): Use '*' for any value, ',' for multiple values, '-' for ranges, and '/' for steps.";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
