﻿using System;
using System.Collections.Generic;
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

        public Admin() { }

        public Admin(string Username, string Password)
        {
            this.Username = Username;
            this.Password = Password;
        }

        public bool CheckInformation()
        {
            if (this.Username.Equals("Klemis") && this.Password.Equals("Kitchen"))
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