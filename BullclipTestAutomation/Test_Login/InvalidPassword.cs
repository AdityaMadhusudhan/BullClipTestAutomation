using System;
using System.Net;
using BullclipTestAutomation.Library;
using BullclipTestAutomation.Controller;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BullclipTestAutomation.DataModel;

namespace BullclipTestAutomation.Test_Login
{
    [TestClass]
    public class InvalidPassword
    {
        [TestMethod]
        [DeploymentItem("BullclipTestAutomation\\Data.xml")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\Data.xml", "InvalidPasswordLogin", DataAccessMethod.Sequential)]

        public void InvalidPassword_Test()
        {
            Logger.BeginTest("Login_InvalidPassword");

            RestExecutor.Url = m_testContext.DataRow["Url"].ToString();
            Logger.LogInfo("Url :" + RestExecutor.Url);

            LoginCredentials loginCred = new LoginCredentials();
            loginCred.username = m_testContext.DataRow["username"].ToString();
            loginCred.password = m_testContext.DataRow["password"].ToString();
            string json = JSONExecutor.SerializeJason(loginCred);
            Logger.LogInfo("Json sent - Login :" + json);

            string expectedStatusCode = m_testContext.DataRow["ExpectedStatusCode"].ToString();
            Logger.LogInfo("Expected Status Code :" + expectedStatusCode);

            string statusCode;
            string responseStream = RestExecutor.RunPOSTRequest(json, out statusCode);
            Logger.LogInfo("Actual Status Code :" + statusCode);
            Logger.LogInfo("Response :" + responseStream);

            //The status code should be 400 - Bad Request as the password is wrong
            Logger.Assert_True(statusCode.Equals(expectedStatusCode), "The expected status code : " + expectedStatusCode + " and actual status code :" + statusCode);
           
            //Response should not have accesstoken
            Logger.Assert_False(responseStream.Contains("accessToken"), "The response stream does not contain an  : accessToken.");

        }

        private TestContext m_testContext;

        public TestContext TestContext

        {

            get { return m_testContext; }

            set { m_testContext = value; }

        }
    }
}
