using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using єMessage.Commands;
using єMessage.Services;

namespace єMessage.ViewModels
{
    public class SingUpPageViewModel : ViewModelBase
    {
        private string _username;
        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private string _email;
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }

            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public ICommand SingInPageCommand { get; }
        public ICommand createAccountCommand { get; }

        public SingUpPageViewModel(NavigationServices singUpViewNavigateService)
        {
            SingInPageCommand = new NavigationCommand(singUpViewNavigateService);
            createAccountCommand = new CreateAccountCommand(this);
        }
    }
}
