using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyKlemisApp.Models
{
    public class Token
    {
        public int Id { get; set; }
        public string accessToken { get; set; }
        public string errorMessage { get; set; }
        public DateTime expirationDate { get; set; }

        public Token()
        {
        }
    }
}
