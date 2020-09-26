using System;
using System.Collections.Generic;
using System.Text;

namespace MyKlemisApp.Models
{
    class LoginAuthenticator
    {
        public bool authenticate(String login, String password)
        {
            if (login.Equals("Klemis") && password.Equals("Kitchen"))
            {
                return true;
            }

            return false;
        }

    }

    
}
