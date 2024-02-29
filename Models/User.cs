using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace єMessage.Models
{
    public class User
    {
        public string Password { get; }
        public string Email { get; }

        public User(string password, string email)
        {
            Password = password;
            Email = email;
        }
    }
}
