using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using єMessage.Encryption;
using єMessage.Models;
using єMessage.Repositories;
using єMessage.Services;
using єMessage.Stores;
using єMessage.ViewModels;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace єMessage.Commands
{
    public class SingInCommand : CommandBase
    {
        private readonly SingInPageViewModel _singInPageViewModel;
        private readonly NavigationServices<SplashScreenViewModel> _navigationService;
        private readonly IUserRepository _userRepository;

        public SingInCommand(SingInPageViewModel singInPageViewModel, NavigationServices<SplashScreenViewModel> navigationService)
        {
            _singInPageViewModel = singInPageViewModel;
            _navigationService = navigationService;
            _userRepository = new UserRepository();
            _singInPageViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object parameter)
        {
            bool validData;
            if (string.IsNullOrWhiteSpace(_singInPageViewModel.Email) || _singInPageViewModel.Email.Length <= 11 ||
                _singInPageViewModel.Password == null || _singInPageViewModel.Password.Length <= 6)
                validData = false;
            else
                validData = true;
            return validData;
        }

        public override void Execute(object parameter)
        {
            var isValidUser = _userRepository.AuthenticateUser(_singInPageViewModel.Email, SHA256Hash.hashPassword(_singInPageViewModel.Password));
            if (isValidUser)
            {
                Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(_singInPageViewModel.Email), null);
                _singInPageViewModel.IsViewVisible = false;

                _navigationService.Navigate();

            }
            else
            {
                _singInPageViewModel.ErrorMessage = "* Invalid email or password";
            }
        }

        private void OnViewModelPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SingInPageViewModel.Email) ||
                e.PropertyName == nameof(SingInPageViewModel.Password))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
