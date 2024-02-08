using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using єMessage.Commands;
using єMessage.Services;
using єMessage.Stores;

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

        private string _repeatPassword;
        public string RepeatPassword
        {
            get
            {
                return _repeatPassword;
            }
            set
            {
                _repeatPassword = value;
                OnPropertyChanged(nameof(RepeatPassword));
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
        public ICommand СreateAccountCommand { get; }

        public SingUpPageViewModel(NavigationStore navigationStore)
        {
            SingInPageCommand = new NavigationCommand<SingInPageViewModel>(new NavigationServices<SingInPageViewModel>(navigationStore, () => new SingInPageViewModel(navigationStore)));
            СreateAccountCommand = new CreateAccountCommand(this);
        }
    }
}
