using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using єMessage.Commands;
using єMessage.Services;
using єMessage.Stores;

namespace єMessage.ViewModels
{
    public class SingInPageViewModel : ViewModelBase
    {
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

        private bool _isViewVisible = true;
        public bool IsViewVisible
        {
            get
            {
                return _isViewVisible;
            }

            set
            {
                _isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }

        public ICommand SingUpPageCommand { get; }

        public ICommand SingInCommand { get; }

        public SingInPageViewModel(NavigationStore navigationStore)
        {
            SingUpPageCommand = new NavigationCommand<SingUpPageViewModel>(new NavigationServices<SingUpPageViewModel>(navigationStore, () => new SingUpPageViewModel(navigationStore)));
            SingInCommand = new SingInCommand(this, new NavigationServices<SplashScreenViewModel>(navigationStore, () => new SplashScreenViewModel(navigationStore, Email)));
        }
    }
}
