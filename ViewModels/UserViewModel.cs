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

        public string Username => _user.Username;
        public string Password => _user.Password;

        public UserViewModel(User user)
        {
            _user = user;
        }
    }
}
