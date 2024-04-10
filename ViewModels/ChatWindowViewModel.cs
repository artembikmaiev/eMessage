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

        private byte[] _selectedFileBytes;
        public byte[] SelectedFileBytes
        {
            get { return _selectedFileBytes; }
            set
            {
                _selectedFileBytes = value;
                OnPropertyChanged(nameof(SelectedFileBytes));
            }
        }

        private string _fileName;
        public string FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                _fileName = value;
                OnPropertyChanged(nameof(FileName));
            }
        }

        public string EncodeFileToBase64(byte[] fileBytes)
        {
            return Convert.ToBase64String(fileBytes);
        }

        private string ExtractFileNameIfAny(string message)
        {
            // Спрощена перевірка, що повідомлення закінчується розширенням файла
            var fileExtensions = new List<string> { ".jpg", ".png", ".pdf", ".docx", ".xlsx" };
            foreach (var ext in fileExtensions)
            {
                if (message.EndsWith(ext, StringComparison.OrdinalIgnoreCase))
                {
                    // Припускаємо, що повідомлення є ім'ям файла
                    return message;
                }
            }

            // Якщо збігів не знайдено, повертаємо null або порожній рядок
            return null;
        }


        public ICommand SendCommand { get; set; }
        public ICommand ToProfile { get; }
        public ICommand ToAddContactCommand { get; }
        public ICommand AddContactCommand { get; private set; }
        public ICommand ChooseFileCommand { get; private set; }
        public ICommand DownloadFileCommand { get; private set; }
        public ChatWindowViewModel(NavigationStore navigationStore, string email)
        {
            _userRepository = new UserRepository();
            Messages = new ObservableCollection<MessageModel>();
            Contacts = new ObservableCollection<ContactModel>();

            Email = email;

            UserInfo userInfo = _userRepository.GetNamesByEmail(email);
            Username = userInfo.Username;
            _photoSource = userInfo.AvatarImage;
            Status = userInfo.Status;

            ws = new WebSocket("ws://bristle-lacy-suede.glitch.me/");
            ws.OnMessage += (sender, e) =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    string message = e.Data;
                    // Тут вам потрібно визначити, чи містить повідомлення ім'я файла, і якщо так, то встановити це ім'я файлу в `FileName`.
                    string fileName = ExtractFileNameIfAny(message); // Це ваш метод для отримання імені файлу з повідомлення.

                    Messages.Add(new MessageModel
                    {
                        Message = message,
                        FileName = fileName, // Встановити ім'я файла тут, якщо воно існує.
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
                Messages = Messages,
            });


            ToProfile = new NavigationCommand<ProfileViewModel>(new NavigationServices<ProfileViewModel>(navigationStore, () => new ProfileViewModel(navigationStore, Email)));
            DownloadFileCommand = new DownloadFileCommand(this);
            ChooseFileCommand = new ChooseFileCommand(this);
        }

    }
}
