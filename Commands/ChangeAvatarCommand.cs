using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using єMessage.Models;
using єMessage.ViewModels;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace єMessage.Commands
{
    public class ChangeAvatarCommand : CommandBase
    {
        private readonly ProfileViewModel _viewModel;
        private readonly IUserRepository _userRepository;
        private readonly string _email;

        public ChangeAvatarCommand(ProfileViewModel viewModel, IUserRepository userRepository, string email)
        {
            _viewModel = viewModel;
            _userRepository = userRepository;
            _email = email;
        }

        public override async void Execute(object parameter)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
            };

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    string fileName = openFileDialog.FileName;
                    byte[] imageBytes = await File.ReadAllBytesAsync(fileName);

                    bool isUpdated = await _userRepository.UpdateAvatarImageByEmailAsync(_email, imageBytes);

                    if (isUpdated)
                    {
                        _viewModel.PhotoSource = imageBytes;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while updating the avatar: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
