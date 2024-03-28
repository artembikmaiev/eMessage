using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using єMessage.ViewModels;

namespace єMessage.Commands
{
    public class ChooseFileCommand : CommandBase
    {
        private readonly ChatWindowViewModel _chatWindowViewModel;

        public ChooseFileCommand(ChatWindowViewModel chatWindowViewModel)
        {
            _chatWindowViewModel = chatWindowViewModel;
        }

        public override void Execute(object parameter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;

                // Read the selected file into a byte array
                byte[] fileBytes = File.ReadAllBytes(filePath);

                // Now you have the file's bytes, you can do whatever you need with them,
                // such as storing them to send later
                _chatWindowViewModel.SelectedFileBytes = fileBytes;

                // Optionally, you can store just the filename for display purposes
                _chatWindowViewModel.Message = Path.GetFileName(filePath);
            }
        }
    }
}
