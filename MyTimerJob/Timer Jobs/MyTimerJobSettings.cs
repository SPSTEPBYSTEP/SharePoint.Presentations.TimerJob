using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Administration;

namespace MyTimerJob.Timer_Jobs
{
    public class MyTimerJobSettings : SPPersistedObject
    {
        public static string SettingsName = "MyTimerJobSettings";

        public MyTimerJobSettings() { }
        public MyTimerJobSettings(SPPersistedObject parent, Guid id) :
            base(SettingsName, parent, id) { }

        [Persisted]
        public string WOEID;
    }
}
