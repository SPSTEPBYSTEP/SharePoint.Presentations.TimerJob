using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint.Administration;

namespace MyTimerJob.Timer_Jobs
{
    public class ULSLogger
    {
        public const string CATEGORY = "TimerJobDemo";
        private static ULSLogger _Current;
        private SPDiagnosticsService diag = SPDiagnosticsService.Local;

        public static ULSLogger Instance
        {
            get
            {
                if (_Current == null)
                {
                    _Current = new ULSLogger();
                }
                return _Current;
            }
        }

        public void LogInfo(string message, params object[] args)
        {
            try
            {
                diag.WriteTrace(0,
                    new SPDiagnosticsCategory(CATEGORY, TraceSeverity.Verbose, EventSeverity.Information),
                    TraceSeverity.Verbose,
                    string.Format(message, args));
            }
            catch { }
        }
        public void LogError(Exception ex)
        {
            try
            {
                diag.WriteTrace(0,
                    new SPDiagnosticsCategory(CATEGORY, TraceSeverity.High, EventSeverity.Error),
                    TraceSeverity.High,
                    "Exception occurred {0}", new object[] { ex });
            }
            catch { }
        }

    }
}
