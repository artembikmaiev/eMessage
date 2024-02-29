using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using єMessage.Models;
using єMessage.ViewModels;
using WebSocketSharp;

namespace єMessage.Commands
{
    public class SendCommand : CommandBase
    {
        private readonly ChatWindowViewModel _chatWindowViewModel;
        WebSocket _ws;
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
                string message = $"{_chatWindowViewModel.Message}";

                // Відправка повідомлення через WebSocket
                _ws.Send(message);

                // Очищення поля для введення повідомлення
                _chatWindowViewModel.Message = "";
            }
        }
    }
}
