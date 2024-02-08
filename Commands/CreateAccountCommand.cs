using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using єMessage.Commands;

namespace єMessage.ViewModels
{
    public class CreateAccountCommand : CommandBase
    {
        private readonly SingUpPageViewModel _singUpPageViewModel;

        public CreateAccountCommand(SingUpPageViewModel pageViewModel)
        {
            _singUpPageViewModel = pageViewModel;
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
