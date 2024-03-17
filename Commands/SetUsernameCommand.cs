using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using єMessage.Models;
using єMessage.Repositories;
using єMessage.ViewModels;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace єMessage.Commands
{
    public class SetUsernameCommand : CommandBase
    {
        private readonly ProfileViewModel _viewModel;
        private readonly IUserRepository _userRepository;
        private readonly string _email;

        public SetUsernameCommand(ProfileViewModel viewModel, IUserRepository userRepository, string email)
        {
            _viewModel = viewModel;
            _userRepository = userRepository;
            _email = email;
        }

        public override async void Execute(object parameter)
        {
            await _userRepository.UpdateUsername(_email, _viewModel.Username);
        }
    }
}
