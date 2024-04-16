using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G_MBIVautoTester._Globalz
{
    public class EventsManagerLib
    {
        public delegate void EventLogConsole_Handler(string arg_strval);
        public static event EventLogConsole_Handler OnLogConsoleEvent;
        public static void Call_LogConsole(string srg_str)
        {
             
            OnLogConsoleEvent?.Invoke(srg_str);
        }

    }
}
