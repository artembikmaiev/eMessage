using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace єMessage.Models
{
    public class User
    {
        public string Username { get; }
        public string Password { get; }
        public string Email { get; }

        public User(string username, string password, string email)
        {
            Username = username;
            Password = password;
            Email = email;
        }
    }
}
