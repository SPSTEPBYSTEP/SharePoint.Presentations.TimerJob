using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint;

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

        #region Properties
        public string Property1
        {
            get;
            set;
        }
        #endregion

        public void Execute()
        {
            // Business logic goes here
        }
    }
}
