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

        public SingUpPageViewModel(NavigationServices singUpViewNavigateService)
        {
            SingInPageCommand = new NavigationCommand(singUpViewNavigateService);
        }
    }
}
