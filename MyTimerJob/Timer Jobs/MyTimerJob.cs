using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace MyTimerJob.Timer_Jobs
{
    public class MyTimerJob : SPJobDefinition
    {
        public static string jobName = "Demo Timer Job";
        const string jobTitle = "My Timer Job";
        const string listName = "Movie Listings";

        #region Constructor
        public MyTimerJob()
            : base()
        { }
        public MyTimerJob(string jobName, SPService service,
            SPServer server, SPJobLockType targetType)
            : base(jobName, service,
                server, targetType)
        { }
        public MyTimerJob(string jobName, SPWebApplication webApplication)
            : base(jobName, webApplication, null, SPJobLockType.ContentDatabase)
        {
            this.Title = jobTitle;
        }
        #endregion
        public override void Execute(Guid targetInstanceId)
        {
            try
            {
                ULSLogger.Instance.LogInfo(string.Format("{0} is searching for its configuration", jobName));
                // Retrieve the timer job's setting 
                MyTimerJobSettings jobSettings = this.WebApplication.GetChild<MyTimerJobSettings>(MyTimerJobSettings.SettingsName);
                if (jobSettings == null)
                {
                    ULSLogger.Instance.LogInfo(string.Format("{0} could not find its configuration. There is nothing to do, will check again in next run.", jobName));
                    return;
                }
                // Put your job's code here   
                BusinessLogic.MyTimerBusinessLogic.Instance.WebApplication = this.Parent as SPWebApplication;
                BusinessLogic.MyTimerBusinessLogic.Instance.WOEIDs = jobSettings.WOEID;
                BusinessLogic.MyTimerBusinessLogic.Instance.JobName = jobName;
                BusinessLogic.MyTimerBusinessLogic.Instance.Execute();
            }
            catch (Exception ex)
            {
                ULSLogger.Instance.LogError(ex);
            }
        }


        /// <summary>
        /// Ensure that the movies list exists
        /// </summary>
        private void EnsureList() { }

    }
}
