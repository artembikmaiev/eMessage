using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using єMessage.ViewModels;

namespace єMessage.Commands
{
    public class SingInPageCommand : CommandBase
    {
        private readonly SingInPageViewModel _singInPageViewModel;

        public SingInPageCommand(SingInPageViewModel singInPageViewModel)
        {
            _singInPageViewModel = singInPageViewModel;

            _singInPageViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            
        }

        public override void Execute(object parameter)
        {
            
        }
    }
}
