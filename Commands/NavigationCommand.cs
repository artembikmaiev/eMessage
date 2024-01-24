using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using єMessage.Services;

namespace єMessage.Commands
{
    public class NavigationCommand : CommandBase
    {
        private readonly NavigationServices _navigationServices;

        public NavigationCommand(NavigationServices navigationServices)
        {
            _navigationServices = navigationServices;
        }

        public override void Execute(object parameter)
        {
            _navigationServices.Navigate();
        }
    }
}
