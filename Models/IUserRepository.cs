using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using єMessage.ViewModels;

namespace єMessage.Models
{
    
    public interface IUserRepository
    {
        Task<bool> AuthenticateUser(string email, string password);

        Task AddUser (string email, string username, string password);

        bool IsUserRegistered(string email);

        Task<bool> UpdateAvatarImageByEmailAsync(string email, byte[] avatarImage);

        Task UpdateFirstName(string email, string firstName);

        Task UpdateLastName(string email, string lastName);

        Task UpdateUsername(string email, string username);

        Task UpdateStatus(string email, string status);

        UserInfo GetNamesByEmail(string email);
    }
}
