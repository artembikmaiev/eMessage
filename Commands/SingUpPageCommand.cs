﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using єMessage.Services;
using єMessage.ViewModels;

namespace єMessage.Commands
{
    public class SingUpPageCommand : CommandBase
    {
        private readonly SingUpPageViewModel _singUpPageViewModel;

        public SingUpPageCommand(SingUpPageViewModel singUpPageViewModel, NavigationServices singUpViewNavigateService)
        {
            _singUpPageViewModel = singUpPageViewModel;

            _singUpPageViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        private void OnViewModelPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

        }

        public override void Execute(object parameter)
        {

        }
    }
}