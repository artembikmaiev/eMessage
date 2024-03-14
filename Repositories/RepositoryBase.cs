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
            _connectionString = @"Data Source=database-1.c5y22eygaesm.eu-north-1.rds.amazonaws.com;Initial Catalog=[Users];User ID=admin;Password=12345678;TrustServerCertificate=True;";
        }

        protected SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
