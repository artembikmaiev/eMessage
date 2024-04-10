using System;
using System.Net;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using єMessage.Models;
using єMessage.ViewModels; // Припускаючи, що клас MessageModel знаходиться тут

namespace єMessage.Commands
{
    public class DownloadFileCommand : CommandBase
    {
        private readonly ChatWindowViewModel _chatWindowViewModel;

        public DownloadFileCommand(ChatWindowViewModel chatWindowViewModel)
        {
            _chatWindowViewModel = chatWindowViewModel;
        }

        public override void Execute(object parameter)
        {
            // Перевірити, що переданий параметр є типом MessageModel
            var fileName = parameter as string;
            if (!string.IsNullOrEmpty(fileName))
            {
                var saveFileDialog = new SaveFileDialog
                {
                    FileName = fileName,
                    Filter = "All files (*.*)|*.*"
                };

                if (saveFileDialog.ShowDialog() == true)
                {
                    try
                    {
                        using (var webClient = new WebClient())
                        {
                            var fileUrl = $"https://bristle-lacy-suede.glitch.me/uploads/{fileName}";

                            webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

                            webClient.DownloadFile(fileUrl, saveFileDialog.FileName);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred while downloading the file: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("File name is not provided or the message is not a valid file.");
            }
        }
    }
}
