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
            return !string.IsNullOrEmpty(_singUpPageViewModel.Username) &&
                   !string.IsNullOrEmpty(_singUpPageViewModel.Email) &&
                   !string.IsNullOrEmpty(_singUpPageViewModel.Password) &&
                   _singUpPageViewModel.Password.Length >= 6 &&
                 base.CanExecute(parameter);
        }

        public override void Execute(object parameter)
        {
            throw new NotImplementedException();
        }

        private void OnViewModelPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(SingUpPageViewModel.Username) ||
                e.PropertyName == nameof(SingUpPageViewModel.Email) || 
                e.PropertyName == nameof(SingUpPageViewModel.Password))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
