using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using єMessage.ViewModels;

namespace єMessage.Models
{
    
    public interface IUserRepository
    {
        bool AuthenticateUser(string email, string password);
    }
}
