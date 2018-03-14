using System;
using System.Net;
using BullclipTestAutomation.Library;
using BullclipTestAutomation.Controller;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BullclipTestAutomation.DataModel;

namespace BullclipTestAutomation.Test_Login
{
    
    [TestClass]
    public class ValidLogin
    {
        [TestMethod]
        [DeploymentItem("BullclipTestAutomation\\Data.xml")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\Data.xml", "ValidLogin", DataAccessMethod.Sequential)]
        public void ValidLogin_Test()
        {
            Logger.BeginTest("Login_Valid");

            RestExecutor.Url = m_testContext.DataRow["Url"].ToString();
            Logger.LogInfo("Url :" + RestExecutor.Url);

            LoginCredentials loginCred = new LoginCredentials();
            loginCred.username= m_testContext.DataRow["username"].ToString();
            loginCred.password = m_testContext.DataRow["password"].ToString();
            string json = JSONExecutor.SerializeJason(loginCred);
            Logger.LogInfo("Json sent - Login :" + json);

            string expectedStatusCode = m_testContext.DataRow["ExpectedStatusCode"].ToString();
            Logger.LogInfo("Expected Status Code :" + expectedStatusCode);

            string statusCode;
            string responseStream = RestExecutor.RunPOSTRequest(json, out statusCode);
            Logger.LogInfo("Actual Status Code :" + statusCode);
            Logger.LogInfo("Response :" + responseStream);

            //The status code should be 200 - Ok as the username & password is valid
            Logger.Assert_True(statusCode.Equals(expectedStatusCode), "The expected status code : " + expectedStatusCode + " and actual status code :" + statusCode);
            //Access Token should be generated
            Logger.Assert_True(responseStream.Contains("accessToken"), "The response stream contains an  : accessToken.");
            
        }
        private TestContext m_testContext;

        public TestContext TestContext

        {

            get { return m_testContext; }

            set { m_testContext = value; }

        }
    }
}
