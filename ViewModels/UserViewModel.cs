using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using єMessage.Models;

namespace єMessage.ViewModels
{
    public class UserViewModel : ViewModelBase
    {
        private readonly User _user;

        public string Password => _user.Password;

        public string Email => _user.Email;

        public UserViewModel(User user)
        {
            _user = user;
        }
    }
}
