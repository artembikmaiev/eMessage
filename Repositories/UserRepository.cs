using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using єMessage.Models;
using static System.Net.WebRequestMethods;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace єMessage.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        public async Task AddUser(string email, string username, string password)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                await connection.OpenAsync();
                command.Connection = connection;
                command.CommandText = "INSERT INTO UsersInfo (email, username, [password]) VALUES (@email, @username, @password)";
                command.Parameters.Add("@email", SqlDbType.NVarChar).Value = email;
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                command.Parameters.Add("@password", SqlDbType.NVarChar).Value = password;
                await command.ExecuteNonQueryAsync();
            }
        }


        public async Task<bool> AuthenticateUser(string email, string password)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                await connection.OpenAsync();
                command.Connection = connection;
                command.CommandText = "select * from UsersInfo where email=@email and [password]=@password";
                command.Parameters.Add("@email", SqlDbType.NVarChar).Value = email;
                command.Parameters.Add("@password", SqlDbType.NVarChar).Value = password;
                var result = await command.ExecuteScalarAsync();
                return result != null;
            }
        }


        public UserInfo GetNamesByEmail(string email)
        {
            UserInfo userInfo = new UserInfo();

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT FirstName, LastName, Username, Status, AvatarImage FROM UsersInfo WHERE email = @Email";
                command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = email;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        userInfo.FirstName = reader["FirstName"].ToString();
                        userInfo.LastName = reader["LastName"].ToString();
                        userInfo.Username = reader["Username"].ToString();
                        userInfo.Status = reader["Status"].ToString();

                        // Отримання фото як масив байтів
                        if (reader["AvatarImage"] != DBNull.Value)
                        {
                            userInfo.AvatarImage = (byte[])reader["AvatarImage"];
                        }
                        else
                        {
                            string imagePath = "Images/avatar.png";
                            byte[] imageBytes = System.IO.File.ReadAllBytes(imagePath);

                            userInfo.AvatarImage = imageBytes;
                        }
                    }
                }
            }

            return userInfo;
        }

        public async Task<bool> UpdateAvatarImageByEmailAsync(string email, byte[] avatarImage)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                try
                {
                    await connection.OpenAsync();
                    command.Connection = connection;
                    command.CommandText = @"
                    UPDATE UsersInfo 
                    SET AvatarImage = @AvatarImage 
                    WHERE Email = @Email";

                    command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = email;
                    command.Parameters.Add("@AvatarImage", SqlDbType.VarBinary).Value = avatarImage;

                    int rowsAffected = await command.ExecuteNonQueryAsync();

                    return rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    throw new Exception("Error updating user avatar.", ex);
                }
            }
        }

        public async Task UpdateFirstName(string email, string firstName)
        {
            using (var connection = GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("UPDATE UsersInfo SET FirstName = @FirstName WHERE Email = @Email", connection))
                {
                    command.Parameters.AddWithValue("@FirstName", firstName);
                    command.Parameters.AddWithValue("@Email", email);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateUsername(string email, string username)
        {
            using (var connection = GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("UPDATE UsersInfo SET Username = @Username WHERE Email = @Email", connection))
                {
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Email", email);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateStatus(string email, string status)
        {
            using (var connection = GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("UPDATE UsersInfo SET Status = @Status WHERE Email = @Email", connection))
                {
                    command.Parameters.AddWithValue("@Status", status);
                    command.Parameters.AddWithValue("@Email", email);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task UpdateLastName(string email, string lastName)
        {
            using (var connection = GetConnection())
            {
                await connection.OpenAsync();
                using (var command = new SqlCommand("UPDATE UsersInfo SET LastName = @LastName WHERE Email = @Email", connection))
                {
                    command.Parameters.AddWithValue("@LastName", lastName);
                    command.Parameters.AddWithValue("@Email", email);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public bool IsUserRegistered(string email)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT COUNT(*) FROM UsersInfo WHERE email = @email";
                command.Parameters.Add("@email", SqlDbType.NVarChar).Value = email;

                int count = (int)command.ExecuteScalar();
                return count > 0;
            }
        }
    }
}
