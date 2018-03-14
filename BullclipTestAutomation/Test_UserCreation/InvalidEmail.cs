using System;
using System.Linq;
using BullclipTestAutomation.Library;
using BullclipTestAutomation.Controller;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BullclipTestAutomation.DataModel;

namespace BullclipTestAutomation.Test_UserCreation
{
    [TestClass]
    public class InvalidEmail
    {

        [TestMethod]
        [DeploymentItem("BullclipTestAutomation\\Data.xml")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\Data.xml", "InvalidEmail", DataAccessMethod.Sequential)]
        public void InvalidEmail_Test()
        {
            Logger.BeginTest("UserCreation_InvalidEmail");

            RestExecutor.Url = m_testContext.DataRow["Url"].ToString();
            Logger.LogInfo("Url :" + RestExecutor.Url);

            NewUserCreation newUser = new NewUserCreation();
            newUser.email = "abc";
            string json = JSONExecutor.SerializeJason(newUser);
            Logger.LogInfo("Json sent - User creation :" + json);

            string expectedStatusCode = m_testContext.DataRow["ExpectedStatusCode"].ToString();
            Logger.LogInfo("Expected Status Code :" + expectedStatusCode);

            string statusCode;
            string key = m_testContext.DataRow["Key"].ToString();
            Logger.LogInfo("Auth Key Used :" + key);

            string responseStream = RestExecutor.RunPOSTRequest(json, key, out statusCode);
            Logger.LogInfo("Actual Status Code :" + statusCode);
            Logger.LogInfo("Response :" + responseStream);

            //The status code should be 400 - bad request as passwords were different
            Logger.Assert_True(statusCode.Equals(expectedStatusCode), "The expected status code : " + expectedStatusCode + " and actual status code :" + statusCode);

            //Response should say email invalid
            Logger.Assert_True(responseStream.Contains("The Email field is not a valid e-mail address."), "The response stream contains : The Email field is not a valid e-mail address.");
        }

        private TestContext m_testContext;

        public TestContext TestContext

        {

            get { return m_testContext; }

            set { m_testContext = value; }

        }
    }
}
