using log4net;
using log4net.Config;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullclipTestAutomation.Library
{
    public static class Logger
    {
        public static string LogPath=System.Configuration.ConfigurationSettings.AppSettings["LogPath"] + "_" + DateTime.Now.ToString("dd_MM_yyyy");
        //Declare an instance for log4net
        private static readonly ILog Log =
              LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static void BeginTest(string testCaseName)
        {
            Directory.CreateDirectory(LogPath);
            log4net.GlobalContext.Properties["LogFileName"] = LogPath+"\\"+testCaseName+"_"+DateTime.Now.ToString("dd_MM_yy_HH_mm")+".log";
            XmlConfigurator.Configure();
            LogInfo("Test Begin");
        }
        public static void LogInfo(string message)
        {
            Log.Info(message);
        }
        public static void Assert_True(bool condition,string message)
        {
            if (condition)
                Log.Info(message + " Is true");
            else
                Log.Error(message+ " Is false");

            Assert.IsTrue(condition, message);

        }
        public static void Assert_False(bool condition, string message)
        {
            if (condition)
                Log.Error(message + " Is true");
            else
                Log.Info(message + " Is false");

            Assert.IsFalse(condition, message);

        }

    }
}
