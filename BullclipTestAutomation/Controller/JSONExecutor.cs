using BullclipTestAutomation.DataModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;


namespace BullclipTestAutomation.Controller
{
    public class JSONExecutor
    {

        public static string SerializeJason(object obj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            // Serialize it.  
            string serializedJson = serializer.Serialize(obj);
            
            return serializedJson;
        }

        public static LoginResponse DeSerializeLoginResponse(string jSon)
        {
            LoginResponse data = new LoginResponse() ;
              data = JsonConvert.DeserializeObject<LoginResponse>(jSon);
            
            return data;

        }
    }
}
