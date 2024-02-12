using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using єMessage.Commands;
using єMessage.Models;
using єMessage.Repositories;
using єMessage.Services;

namespace єMessage.ViewModels
{
    public class CreateAccountCommand : CommandBase
    {
        private readonly SingUpPageViewModel _singUpPageViewModel;
        private readonly NavigationServices<SingInPageViewModel> _navigationService;
        private readonly IUserRepository _userRepository;

        public CreateAccountCommand(SingUpPageViewModel pageViewModel, NavigationServices<SingInPageViewModel> navigationService)
        {
            _singUpPageViewModel = pageViewModel;
            _navigationService = navigationService;
            _userRepository = new UserRepository();
            _singUpPageViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object parameter)
        {
            bool validData;
            if (string.IsNullOrWhiteSpace(_singUpPageViewModel.Email) || _singUpPageViewModel.Email.Length <= 11 ||
                string.IsNullOrWhiteSpace(_singUpPageViewModel.Username) || _singUpPageViewModel.Username.Length <= 1 ||
                _singUpPageViewModel.Password == null || _singUpPageViewModel.Password.Length <= 6 ||
                _singUpPageViewModel.Password == null || _singUpPageViewModel.Password.Length <= 6)
            {
                validData = false;
            }
            else
            {
                validData = true;
            }
            return validData;
        }

        public override void Execute(object parameter)
        {
            bool isUserRegistered = _userRepository.IsUserRegistered(_singUpPageViewModel.Email);
            bool isPasswordSame = _singUpPageViewModel.Password != _singUpPageViewModel.RepeatPassword;

            if (isUserRegistered)
            {
                _singUpPageViewModel.ErrorMessage = "* This email is already registered.";
            }
            else if (isPasswordSame)
            {
                _singUpPageViewModel.ErrorMessage = "* Passwords are not the same.";
            }
            else if (isUserRegistered)
            {
                _userRepository.AddUser(_singUpPageViewModel.Email, _singUpPageViewModel.Username, _singUpPageViewModel.Password);

                Thread.CurrentPrincipal = new GenericPrincipal(
                    new GenericIdentity(_singUpPageViewModel.Email), null);
                _singUpPageViewModel.IsViewVisible = false;

                _navigationService.Navigate();
            }
        }

        private void OnViewModelPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SingUpPageViewModel.Username) ||
                e.PropertyName == nameof(SingUpPageViewModel.Email) || 
                e.PropertyName == nameof(SingUpPageViewModel.Password) ||
                e.PropertyName == nameof(SingUpPageViewModel.RepeatPassword))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
