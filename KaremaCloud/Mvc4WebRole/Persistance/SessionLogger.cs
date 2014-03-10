using System;
using System.Collections.Generic;

namespace Mvc4WebRole
{
    public class SessionLogger
    {
        public static List<String> Logs
        {
            get;
            private set;
        }

        static SessionLogger()
        {
            Logs = new List<string>();
            AddLog("Logger initialized");
        }

        public static void AddLog(String text)
        {
            var dtn = DateTime.Now;
            var log = dtn.ToLongDateString() + " at " + dtn.ToLongTimeString() + " : " + text;
            Logs.Add(log);
        }

        public static void AddLogInit(String text)
        {
            AddLog(text + " initialized");
        }

        public static void AddLogFinished(String text)
        {
            AddLog(text + " finished");
        }
    }
}