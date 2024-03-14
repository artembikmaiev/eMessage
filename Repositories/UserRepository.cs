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
        public void AddUser(string email, string username, string password)
        {
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "INSERT INTO UsersInfo (email, username, [password]) VALUES (@email, @username, @password)";
                command.Parameters.Add("@email", SqlDbType.NVarChar).Value = email;
                command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
                command.Parameters.Add("@password", SqlDbType.NVarChar).Value = password;
                command.ExecuteNonQuery();
            }
        }

        public bool AuthenticateUser(string email, string password)
        {
            bool validUser;
            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "select *from UsersInfo where email=@email and [password]=@password";
                command.Parameters.Add("@email", SqlDbType.NVarChar).Value = email;
                command.Parameters.Add("@password", SqlDbType.NVarChar).Value = password;
                validUser = command.ExecuteScalar() == null ? false : true;
            }
            return validUser;
        }

        public UserInfo GetNamesByEmail(string email)
        {
            UserInfo userInfo = new UserInfo();

            using (var connection = GetConnection())
            using (var command = new SqlCommand())
            {
                connection.Open();
                command.Connection = connection;
                command.CommandText = "SELECT FirstName, LastName, Username, AvatarImage FROM UsersInfo WHERE email = @Email";
                command.Parameters.Add("@Email", SqlDbType.NVarChar).Value = email;

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        userInfo.FirstName = reader["FirstName"].ToString();
                        userInfo.LastName = reader["LastName"].ToString();
                        userInfo.Username = reader["Username"].ToString();

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
