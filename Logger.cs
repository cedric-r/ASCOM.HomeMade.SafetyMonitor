using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASCOM.HomeMade
{
    public static class Logger
    {
        public static bool trace = false;
        public static void LogMessage(string message)
        {
            if (trace)
            {
                try
                {
                    LogMessage(@"c:\temp\SafetyMonitor.log",message);
                }
                catch (Exception e)
                {

                }
            }
        }

        public static void LogMessage(string fileName, string message)
        {
            if (trace)
            {
                try
                {
                    File.AppendAllText(fileName, DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString() + ": " + message + "\n");
                }
                catch (Exception e)
                {

                }
            }
        }

    }
}
