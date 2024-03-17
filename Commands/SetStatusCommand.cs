using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using єMessage.Models;
using єMessage.ViewModels;

namespace єMessage.Commands
{
    public class SetStatusCommand : CommandBase
    {
        private readonly ProfileViewModel _viewModel;
        private readonly IUserRepository _userRepository;
        private readonly string _email;

        public SetStatusCommand(ProfileViewModel viewModel, IUserRepository userRepository, string email)
        {
            _viewModel = viewModel;
            _userRepository = userRepository;
            _email = email;
        }

        public override async void Execute(object parameter)
        {
            await _userRepository.UpdateStatus(_email, _viewModel.Status);
        }
    }
}
