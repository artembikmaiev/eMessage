using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;
using єMessage.Commands;
using єMessage.Services;

namespace єMessage.ViewModels
{
    public class SingInPageViewModel : ViewModelBase
    {
        private int _username;
        public int Username
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

        private int _password;
        public int Password
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
        public ICommand SingUpCommand { get; }

        public SingInPageViewModel(NavigationServices logInViewNavigateService)
        {
            SingUpCommand = new NavigationCommand(logInViewNavigateService);
        }
    }
}
