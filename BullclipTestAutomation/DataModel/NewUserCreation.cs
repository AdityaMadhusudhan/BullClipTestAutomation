using BullclipTestAutomation.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullclipTestAutomation.DataModel
{
    public class NewUserCreation
    {
      
            public string firstname { get; set; }
            public string lastname { get; set; }
            public string email { get; set; }
            public string password { get; set; }
            public string confirmPassword { get; set; }
            public bool isOptInForEmail { get; set; }

        public NewUserCreation()
        {
            firstname = RandomString.GetRandomString(4);
            lastname = RandomString.GetRandomString(4);
            email= RandomString.GetRandomString(5)+"@drawboard.com";
            password = RandomString.GetRandomAlphaNumericString(8);
            confirmPassword = password;
            isOptInForEmail = true;
        }


    }
}
