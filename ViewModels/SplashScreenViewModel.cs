using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using єMessage.Models;
using єMessage.Repositories;
using єMessage.Services;
using єMessage.Stores;

namespace єMessage.ViewModels
{
    public class SplashScreenViewModel : ViewModelBase
    {
        private readonly string _email;
        private readonly NavigationServices<ChatWindowViewModel> _navigationService;
        private readonly IUserRepository _userRepository;

        public SplashScreenViewModel(NavigationStore navigationStore, string email)
        {
            _email = email;
            _userRepository = new UserRepository();
            _navigationService = new NavigationServices<ChatWindowViewModel>(navigationStore, () => new ChatWindowViewModel(navigationStore));

            Initialize();
        }

        private string _fullName;
        public string FullName
        {
            get { return _fullName; }
            set
            {
                _fullName = value;
                OnPropertyChanged(nameof(FullName));
            }
        }

        private async void Initialize()
        {
            if (!string.IsNullOrEmpty(_email))
            {
                Task task = Task.WhenAll(GetNamesAndNavigate(_email), DelayNavigation());
                await task;
            }
        }

        private async Task GetNamesAndNavigate(string email)
        {
            (string firstName, string lastName) = _userRepository.GetNamesByEmail(email);

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            {
                FullName = $"Welcome!";
            }
            else
            {
                FullName = $"Welcome, {firstName} {lastName}";
            }
        }

        private async Task DelayNavigation()
        {
            await Task.Delay(10000);
            _navigationService.Navigate();
        }
    }
}

