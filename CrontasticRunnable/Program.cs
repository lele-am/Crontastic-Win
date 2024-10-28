using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crontastic;

namespace CrontasticRunnable
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Crontastic.Crontastic.Run("*/15 * * * *"));
            Console.ReadKey();
        }
    }
}
