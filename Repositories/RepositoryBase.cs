using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace єMessage.Repositories
{
    public abstract class RepositoryBase
    {
        string _connectionString;
        public RepositoryBase()
        {
            _connectionString = @"Data Source=DESKTOP-0KOAAD4\SQLEXPRESS;Initial Catalog=[Users];Integrated Security=True;TrustServerCertificate=True;";

        }

        protected SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
