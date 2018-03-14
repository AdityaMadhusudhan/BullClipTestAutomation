using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullclipTestAutomation.DataModel
{
    public class LoginResponse
    {
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
        public string authorizationHeader { get; set; }
        public string expiresOnUtc { get; set; }
       

    }
}
