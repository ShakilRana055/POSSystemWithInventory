using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POSSystemWithInventory.Models
{
    public class UserLoginVM
    {
        public UserLoginVM()
        {
            ErrorMessage = "Username or Password is incorrect";
        }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ErrorMessage { get; set; }
    }
}
