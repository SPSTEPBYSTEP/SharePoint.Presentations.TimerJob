using Microsoft.SharePoint.Administration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    /// <summary>
    /// Used for logging into Uls in 2010/2013
    /// </summary>
    public class ULSLogger
    {
        public const string CATEGORY = "Sukul Demo";
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
