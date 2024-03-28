using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using єMessage.Models;
using єMessage.ViewModels;

namespace єMessage.Commands
{
    public class AddContactCommand : CommandBase
    {
        private readonly ChatWindowViewModel _viewModel;
        public ObservableCollection<ContactModel> Contacts { get; set; }

        public AddContactCommand(ChatWindowViewModel viewModel, ObservableCollection<ContactModel> contacts)
        {
            _viewModel = viewModel;
            Contacts = contacts;
        }

        public override async void Execute(object parameter)
        {
            Contacts.Add(new ContactModel
            {
                Username = $"Загальний {Contacts.Count + 1}", // Приклад генерації нового імені
                ImageSource = "https://i.imgur.com/F9Nf9Fx.jpeg",
                Messages = new ObservableCollection<MessageModel>()
            });
        }
    }
}
