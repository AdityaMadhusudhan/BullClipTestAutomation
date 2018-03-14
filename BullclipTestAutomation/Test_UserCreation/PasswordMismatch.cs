using System;
using System.Linq;
using BullclipTestAutomation.Library;
using BullclipTestAutomation.Controller;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BullclipTestAutomation.DataModel;

namespace BullclipTestAutomation.Test_UserCreation
{
    [TestClass]
    public class PasswordMismatch
    {

       
        [TestMethod]
        [DeploymentItem("BullclipTestAutomation\\Data.xml")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\Data.xml", "PasswordMismatch", DataAccessMethod.Sequential)]
        public void PasswordMismatch_Test()
        {
            Logger.BeginTest("UserCreation_PasswordMismatch");

            RestExecutor.Url = m_testContext.DataRow["Url"].ToString();
            Logger.LogInfo("Url :" + RestExecutor.Url);

            NewUserCreation newUser = new NewUserCreation();
            newUser.confirmPassword += "abc";
            string json = JSONExecutor.SerializeJason(newUser);
            Logger.LogInfo("Json sent - User creation :" + json);

            string expectedStatusCode = m_testContext.DataRow["ExpectedStatusCode"].ToString();
            Logger.LogInfo("Expected Status Code :" + expectedStatusCode);

            string statusCode;
            string key = m_testContext.DataRow["Key"].ToString();
            Logger.LogInfo("Auth Key Used :" + key);

            string responseStream = RestExecutor.RunPOSTRequest(json,key, out statusCode);
            Logger.LogInfo("Actual Status Code :" + statusCode);
            Logger.LogInfo("Response :" + responseStream);

            //The status code should be 400 - bad request as passwords were different
            Logger.Assert_True(statusCode.Equals(expectedStatusCode), "The expected status code : " + expectedStatusCode + " and actual status code :" + statusCode);

            //Response should say password mismatch
            Logger.Assert_True(responseStream.Contains("Provided passwords doesn't match"), "The response stream contains : Provided passwords doesn't match");
            
        }

        private TestContext m_testContext;

        public TestContext TestContext

        {

            get { return m_testContext; }

            set { m_testContext = value; }

        }
    }
}
