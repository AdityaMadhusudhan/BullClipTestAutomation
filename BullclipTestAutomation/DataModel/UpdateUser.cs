using BullclipTestAutomation.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullclipTestAutomation.DataModel
{
    public class UpdateUser
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string companyName { get; set; }
        public string department { get; set; }
        public string title { get; set; }
        public string phone { get; set; }
        public bool isOptInForEmail { get; set; }

        public UpdateUser()
        {
            firstname = RandomString.GetRandomString(4);
            lastname = RandomString.GetRandomString(4);
            companyName = RandomString.GetRandomString(5);
            department = RandomString.GetRandomString(5);
            title = RandomString.GetRandomAlphaNumericString(8);
            phone = RandomString.GetRandomAlphaNumericString(8);
            isOptInForEmail = true;
        }

    }
}
