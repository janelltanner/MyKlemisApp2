using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyKlemisApp.Models
{
    public class Admin
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }

        public Admin() { }

        public Admin(string Username, string Password)
        {
            this.Username = Username;
            this.Password = Password;
        }

        public bool CheckInformation()
        {
            if (this.Username.Equals("Klemis") && this.Password.Equals("Kitchen")
                || this.Username.Equals("Name") && this.Password.Equals("Password"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

