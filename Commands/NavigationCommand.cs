using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using єMessage.Services;
using єMessage.Stores;
using єMessage.ViewModels;

namespace єMessage.Commands
{
    public class NavigationCommand<TViewModel> : CommandBase
        where TViewModel : ViewModelBase
    {
        private readonly NavigationServices<TViewModel> _navigationService;

        public NavigationCommand(NavigationServices<TViewModel> navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Execute(object parameter)
        {
            _navigationService.Navigate();
        }
    }
}
