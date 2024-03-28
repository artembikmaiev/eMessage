using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using єMessage.Commands;
using єMessage.Models;
using єMessage.Repositories;
using єMessage.Services;
using єMessage.Stores;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace єMessage.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {
        private readonly NavigationStore _navigationStore;
        private readonly IUserRepository _userRepository;
        private readonly string _email;

        private byte[] _photoSource;
        public byte[] PhotoSource
        {
            get { return _photoSource; }
            set
            {
                _photoSource = value;
                OnPropertyChanged(nameof(PhotoSource));
            }
        }

        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        private string _status;
        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        private string _fullName;
        public string FullName
        {
            get { return _fullName; }
            set
            {
                _fullName = value;
                OnPropertyChanged(nameof(FullName));
            }
        }

        public ICommand ChangeAvatarCommand { get; private set; }
        public ICommand SetFirstNameCommand { get; private set; }
        public ICommand SetLastNameCommand { get; private set; }
        public ICommand SetUsernameCommand { get; private set; }
        public ICommand SetStatusCommand { get; private set; }
        public ICommand Back { get; }

        public ProfileViewModel(NavigationStore navigationStore, string email)
        {
            _email = email;
            _navigationStore = navigationStore;

            _userRepository = new UserRepository();
            UserInfo userInfo = _userRepository.GetNamesByEmail(email);
            Username = userInfo.Username;
            _photoSource = userInfo.AvatarImage;
            Status = userInfo.Status;

            FirstName = userInfo.FirstName;
            LastName = userInfo.LastName;
            FullName = $"{userInfo.FirstName} {userInfo.LastName}";

            ChangeAvatarCommand = new ChangeAvatarCommand(this, new UserRepository(), email);
            SetFirstNameCommand = new SetFirstNameCommand(this, new UserRepository(), email);
            SetLastNameCommand = new SetLastNameCommand(this, new UserRepository(), email);
            SetUsernameCommand = new SetUsernameCommand(this, new UserRepository(), email);
            SetStatusCommand = new SetStatusCommand(this, new UserRepository(), email);
            Back = new NavigationCommand<ChatWindowViewModel>(new NavigationServices<ChatWindowViewModel>(navigationStore, () => new ChatWindowViewModel(navigationStore, email)));
        }
    }
}
