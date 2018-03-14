using BullclipTestAutomation.Library;
using BullclipTestAutomation.Controller;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BullclipTestAutomation.DataModel;

namespace BullclipTestAutomation.Test_UserModification
{
    [TestClass]
    public class UpdateName
    {
        [TestMethod]
        [DeploymentItem("BullclipTestAutomation\\Data.xml")]
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.XML", "|DataDirectory|\\Data.xml", "UpdateName", DataAccessMethod.Sequential)]
        public void UpdateName_Test()
        {
            Logger.BeginTest("Update FirstName & LastName");

            //Login and get the authorization header
            RestExecutor.Url = m_testContext.DataRow["LoginUrl"].ToString();
            Logger.LogInfo("Login Url :" + RestExecutor.Url);

            LoginCredentials loginCred = new LoginCredentials();
            loginCred.username = m_testContext.DataRow["username"].ToString();
            loginCred.password = m_testContext.DataRow["password"].ToString();
            string json = JSONExecutor.SerializeJason(loginCred);
            Logger.LogInfo("Json sent - Login :" + json);

            string key = m_testContext.DataRow["Key"].ToString();
            Logger.LogInfo("Auth Key Used :" + key);

            string statusCode;
            string responseStream = RestExecutor.RunPOSTRequest(json,key, out statusCode);
            Logger.LogInfo("Status Code :" + statusCode);
            Logger.LogInfo("Response :" + responseStream);

            //De serialize response to get the authroization header
            LoginResponse loginResponse = new LoginResponse();
            loginResponse=JSONExecutor.DeSerializeLoginResponse(responseStream);

            UpdateUser existingUser = new UpdateUser();
            json = JSONExecutor.SerializeJason(existingUser);
            Logger.LogInfo("Json sent - User modification :" + json);

            string expectedStatusCode = m_testContext.DataRow["ExpectedStatusCode"].ToString();
            Logger.LogInfo("Expected Status Code :" + expectedStatusCode);
            
            Logger.LogInfo("Auth Key Used :" + key);

            //Login and get the authorization header
            RestExecutor.Url = m_testContext.DataRow["UpdateUrl"].ToString();
            Logger.LogInfo("Update Url :" + RestExecutor.Url);

            //use the authorization header 
            responseStream = RestExecutor.RunPOSTRequest(json, key,loginResponse.authorizationHeader, out statusCode);
            Logger.LogInfo("Actual Status Code :" + statusCode);
            Logger.LogInfo("Response :" + responseStream);

            //The status code should be 200 - Ok as the username & password is valid
            Logger.Assert_True(statusCode.Equals(expectedStatusCode), "The expected status code : " + expectedStatusCode + " and actual status code :" + statusCode);

        }

        private TestContext m_testContext;

        public TestContext TestContext

        {

            get { return m_testContext; }

            set { m_testContext = value; }

        }
    }
}
