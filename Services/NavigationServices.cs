using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using єMessage.Stores;
using єMessage.ViewModels;

namespace єMessage.Services
{
    public class NavigationServices
    {
        private readonly NavigationStore _navigationStore;
        public Func<ViewModelBase> _createViewModel;

        public NavigationServices(NavigationStore navigationStore, Func<ViewModelBase> createViewModel)
        {
            _navigationStore = navigationStore;
            _createViewModel = createViewModel;
        }

        public void Navigate()
        {
            _navigationStore.CurrentViewModel = _createViewModel();
        }
    }
}
