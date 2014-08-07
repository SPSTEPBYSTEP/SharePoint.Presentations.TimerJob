using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.Client;

namespace BusinessLogic
{
    
    public class MyTimerBusinessLogic
    {
        #region Singleton Instance
        private static object lockObj = new object();
        private static MyTimerBusinessLogic _instance = null;
        public static MyTimerBusinessLogic Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (lockObj)
                    {
                        if (_instance == null) { _instance = new MyTimerBusinessLogic(); }
                    }
                }
                return _instance;
            }
        }
        #endregion

        private const string LISTNAME = "Yahoo Weather";
        #region Properties
        public string WOEIDs
        {
            get;
            set;
        }

        public string JobName
        {
            get;
            set;
        }
        #endregion

        public void Execute()
        {
            EnsureList();
            // Business logic goes here
            foreach (var woeid in WOEIDs.Split('|'))
            {
                try
                {
                    var data = YahooWeather.Instance.GetWeatherDataAsJSON(woeid, "c");
                    CreateListItem(webApplication, woeid, data, true);
                }
                catch (Exception ex)
                {
                    CreateListItem(webApplication, woeid, ex.ToString(), false);
                }
            }
        }

        private void EnsureList(SPWebApplication webApplication)
        {

            SPContentDatabase contentDb = webApplication.ContentDatabases[0];
            ULSLogger.Instance.LogInfo(string.Format("{0} is checking if {1} list exists", JobName, LISTNAME));
            SPList myList = contentDb.Sites[0].RootWeb.Lists.TryGetList(LISTNAME);

            if (myList == null)
            {
                ULSLogger.Instance.LogInfo(string.Format("{0} is creating {1} list", JobName, LISTNAME));
                var list = contentDb.Sites[0].RootWeb.Lists.Add(LISTNAME, LISTNAME, SPListTemplateType.GenericList);
                myList = contentDb.Sites[0].RootWeb.Lists.TryGetList(LISTNAME);

                myList.Fields.Add("JSONData", SPFieldType.Text, true);
                myList.Fields.Add("Success", SPFieldType.Boolean, true);
                myList.Fields.Add("ErrorMessage", SPFieldType.Text, true);

                // make new column visible in default view
                SPView view = myList.DefaultView;
                view.ViewFields.Add("JSONData");
                view.ViewFields.Add("Success");
                view.ViewFields.Add("ErrorMessage");
                view.Update();
                ULSLogger.Instance.LogInfo(string.Format("{0} is finished creating {1} list", JobName, LISTNAME)); 
            }
        }

        private void CreateListItem(SPWebApplication webApplication, string woeid, string data, bool success)
        {
            SPContentDatabase contentDb = webApplication.ContentDatabases[0];
            SPList myList = contentDb.Sites[0].RootWeb.Lists.TryGetList(LISTNAME);

             if (myList != null)
             {
                 var listEnumeration = myList.Items.OfType<SPListItem>();
                 SPListItem item = null;
                 // && p["Service"].ToString() == service && p["Method"].ToString() == method
                 item = listEnumeration.FirstOrDefault(p => p.Title == woeid);
                 if (item == null)
                 {
                     ULSLogger.Instance.LogInfo(string.Format("{0} is creating new weather list item", JobName));
                     item = myList.Items.Add();
                     item["Title"] = woeid;
                     item["ErrorMessage"] = string.Empty;
                     item["JSONData"] = data;
                 }
                 else
                 {
                     ULSLogger.Instance.LogInfo(string.Format("{0} is updating weather list item", JobName));
                 }
                 if (!success)
                 {
                     item["JSONData"] = string.Empty;
                     item["ErrorMessage"] = data;
                 }
                 item["Success"] = success;
                 item.Update();
             }
        }
    }
}
