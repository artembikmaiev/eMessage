using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using єMessage.Stores;

namespace єMessage.ViewModels
{
    public class AddContactViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;

        public AddContactViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
        }
    }
}
