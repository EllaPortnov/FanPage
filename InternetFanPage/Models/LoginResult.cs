using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InternetFanPage.Models
{
    public class LoginResult
    {
        public bool LoginSucceeded
        {
            get;
            set;
        }
        public string Token
        {
            get;
            set;
        }

        public LoginResult()
        {
        }

        public static LoginResult Failed = new LoginResult()
        {
            LoginSucceeded = false,
            Token = null
        };
    }
}
