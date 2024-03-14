using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using єMessage.Commands;
using єMessage.Models;
using єMessage.Repositories;
using єMessage.Services;
using єMessage.Stores;
using WebSocketSharp;

namespace єMessage.ViewModels
{
    public class ChatWindowViewModel : ViewModelBase
    {
        public string Email;
        private readonly IUserRepository _userRepository;
        WebSocket ws = new WebSocket("ws://bristle-lacy-suede.glitch.me/");
        public ObservableCollection<MessageModel> Messages { get; set; }
        public ObservableCollection<ContactModel> Contacts { get; set; }

        private ContactModel _selectedContact;
        public ContactModel SelectedContact
        {
            get
            {
                return _selectedContact;
            }
            set
            {
                _selectedContact = value;
                OnPropertyChanged(nameof(SelectedContact));
            }
        }

        private string _message;
        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
                OnPropertyChanged(nameof(Message));
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


        public ICommand SendCommand { get; set; }

        public ChatWindowViewModel(NavigationStore singUpViewNavigateStore, string email)
        {
            _userRepository = new UserRepository();
            Messages = new ObservableCollection<MessageModel>();
            Contacts = new ObservableCollection<ContactModel>();

            Email = email;

            UserInfo userInfo = _userRepository.GetNamesByEmail(email);
            Username = userInfo.Username;
            _photoSource = userInfo.AvatarImage;

            ws = new WebSocket("ws://bristle-lacy-suede.glitch.me/");
            ws.OnMessage += (sender, e) =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    string message = e.Data;
                    Messages.Add(new MessageModel
                    {
                        Message = message,
                        ImageSource = userInfo.AvatarImage,
                        Time = DateTime.Now,
                        IsNativeOrigin = true,
                    });
                });
            };
            ws.Connect();

            SendCommand = new SendCommand(this, ws);

            //Messages.Add(new MessageModel
            //{
            //    Message = "Last",
            //    Username = userInfo.Username,
            //    ImageSource = userInfo.AvatarImage,
            //    Time = DateTime.Now,
            //    UsernameColor = "#409aff",
            //    IsNativeOrigin = true,
            //});

            Contacts.Add(new ContactModel
            {
                Username = $"Загальний",
                ImageSource = "https://i.imgur.com/F9Nf9Fx.jpeg",
                Messages = Messages
            });
        }

    }
}
