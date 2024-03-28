using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using єMessage.Models;
using єMessage.ViewModels;
using WebSocketSharp;
using System.IO;

namespace єMessage.Commands
{
    public class SendCommand : CommandBase
    {
        private readonly ChatWindowViewModel _chatWindowViewModel;
        private WebSocket _ws;

        public ObservableCollection<MessageModel> Messages { get; set; }

        public SendCommand(ChatWindowViewModel chatWindowViewModel, WebSocket ws)
        {
            _chatWindowViewModel = chatWindowViewModel;
            _ws = ws;
        }

        public event EventHandler CanExecuteChanged;

        public override void Execute(object parameter)
        {
            if (_chatWindowViewModel != null)
            {
                if (_chatWindowViewModel.SelectedFileBytes != null && _chatWindowViewModel.SelectedFileBytes.Length > 0)
                {
                    // Convert the file bytes to a Base64 string
                    string fileContentBase64 = Convert.ToBase64String(_chatWindowViewModel.SelectedFileBytes);
                    // Send the file name and content in a single message
                    string fileName = Path.GetFileName(_chatWindowViewModel.Message); // Assuming _chatWindowViewModel.Message contains the file name
                    string fileMessage = $"/file {fileName}:{fileContentBase64}";
                    _ws.Send(fileMessage);

                    _chatWindowViewModel.SelectedFileBytes = null;
                    _chatWindowViewModel.Message = "";
                }
                else if (!string.IsNullOrEmpty(_chatWindowViewModel.Message))
                {
                    string message = $"{_chatWindowViewModel.Username}: {_chatWindowViewModel.Message}";
                    _ws.Send(message);

                    _chatWindowViewModel.Message = "";
                }
            }
        }
    }
}
