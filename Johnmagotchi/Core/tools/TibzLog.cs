using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Johnmagotchi.Core.tools
{
    internal class TibzLog
    {
        public enum LogLevel : int
        {
            DEBUG = 0,
            INFO = 1,
            WARN = 2,
            ERROR = 3,
            CRITICAL = 4,
            DISABLED_LOG = 5
        }
        public static bool isRunningInVS = !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("VisualStudioEdition"));

        public static LogLevel ConsoleLogLevel { get; set; } = LogLevel.DEBUG;
        public static LogLevel FileOutLogLevel { get; set; } = LogLevel.DISABLED_LOG;

        public static void Debug(string str)
        {
            if (ConsoleLogLevel <= 0)
            {
                    if (isRunningInVS) System.Diagnostics.Debug.WriteLine(str);
                    else System.Console.WriteLine(str);
            }
        }
        public static void Debug(object obj)
        {
            if (ConsoleLogLevel <= 0)
            {
                if (isRunningInVS) System.Diagnostics.Debug.WriteLine(obj);
                else System.Console.WriteLine(obj);
            }
        }
        public static void Debug(string str, string str2)
        {
            if (ConsoleLogLevel <= 0)
            {
                if (isRunningInVS) System.Diagnostics.Debug.WriteLine(str, str2);
                else System.Console.WriteLine(str, str2);
            }
        }

        public static void Debug(string str, params object[] objs) {
            if (ConsoleLogLevel <= 0) {
                if (isRunningInVS) System.Diagnostics.Debug.WriteLine(str,objs);
                else System.Console.WriteLine(str, objs);
            }
        }
        public static void Debug(object obj, string str)
        {
            if (ConsoleLogLevel <= 0)
            {
                if (isRunningInVS) System.Diagnostics.Debug.WriteLine(obj, str);
                else
                {
                    System.Console.WriteLine("Tibzlog - Object trace not implemented in system console, only DEBUG in VS environment");
                    System.Console.WriteLine(obj);
                    System.Console.WriteLine(str);
                }
                    
            }
        }

        //TODO add more later if needed, honestly just wanted to make this work quickly.
        /* Ideally we would have multiple levels of logging available, and allow the log to be redirected to a file.
         * It Would also probably be useful to have the log levels listed in the prints, ie " ERROR = <log string> - <line number file number>" or something to that effect.
         * 
         */
    }
}
