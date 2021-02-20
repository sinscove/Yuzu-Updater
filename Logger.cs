using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yuzu_Updater
{
    class Logger
    {
        public void Log(object o)
        {
            Log(LogLevel.INFO, o);
        }

        public void Log(LogLevel level, object o)
        {
            try
            {
                File.AppendAllText(Directory.GetCurrentDirectory() + "/updater.log", DateTime.Now.ToShortTimeString() + " - " + level.ToString() + " - " + o +"\r\n");
            }
            catch(Exception e)
            {
                EventLog.WriteEntry("Application", e.Message, EventLogEntryType.Error);
            }
        }
    }

    enum LogLevel
    {
        INFO, WARNING, ERROR, FATAL
    }
}
