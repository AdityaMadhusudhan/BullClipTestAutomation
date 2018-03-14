using System;
using System.Linq;
using BullclipTestAutomation.Library;
using BullclipTestAutomation.Controller;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BullclipTestAutomation.DataModel;

namespace BullclipTestAutomation.Test_UserCreation
{
    [TestClass]
    public class DuplicateUserCreation
    {
        
        [TestMethod]
        [DeploymentItem("BullclipTestAutomation\\Data.xml")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\Data.xml", "DuplicateUserCreation_FirstName", DataAccessMethod.Sequential)]
        public void DuplicateUserCreation_Test()
        {
            Logger.BeginTest("UserCreation_DuplicateUserCreationTest");

            RestExecutor.Url = m_testContext.DataRow["Url"].ToString();
            Logger.LogInfo("Url :" + RestExecutor.Url);

            NewUserCreation newUser = new NewUserCreation();
            string json = JSONExecutor.SerializeJason(newUser);
            Logger.LogInfo("Json sent - User creation :" + json);

            string expectedStatusCode ="OK";
            Logger.LogInfo("Expected Status Code :" + expectedStatusCode);
            string statusCode;
            string key = m_testContext.DataRow["Key"].ToString();
            Logger.LogInfo("Auth Key Used :" + key);

            string responseStream = RestExecutor.RunPOSTRequest(json,key, out statusCode);
            Logger.LogInfo("Actual Status Code :" + statusCode);
            Logger.LogInfo("Response :" + responseStream);

            //The status code should be 200 - Ok as the username & password is valid
            Logger.Assert_True(statusCode.Equals(expectedStatusCode), "The expected status code : " + expectedStatusCode + " and actual status code :" + statusCode);

            //Now create same user with same email
            string existingUser = newUser.email;
            NewUserCreation newUser_1 = new NewUserCreation();
            newUser_1.email = existingUser;
            json = JSONExecutor.SerializeJason(newUser_1);
            Logger.LogInfo("Json sent - User creation :" + json);

            expectedStatusCode = m_testContext.DataRow["ExpectedStatusCode"].ToString();
            Logger.LogInfo("Json sent - User creation (Duplicate) :" + json);
            Logger.LogInfo("Expected Status Code :" + expectedStatusCode);

            responseStream = RestExecutor.RunPOSTRequest(json,key, out statusCode);
            Logger.LogInfo("Actual Status Code :" + statusCode);
            Logger.LogInfo("Response :" + responseStream);

            //The status code should be 400 - bad request as the email id was already created in previous step
            Logger.Assert_True(statusCode.Equals(expectedStatusCode),"The expected status code : " + expectedStatusCode + " and actual status code :" + statusCode);

            //Response should say user exists
            Logger.Assert_True(responseStream.Contains("User already exists."), "The response stream contains : User already exists.");
            
        }

        private TestContext m_testContext;

        public TestContext TestContext

        {

            get { return m_testContext; }

            set { m_testContext = value; }

        }
    }
}
