using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chinook.Repository
{
    public class ConnectionHelper
    {
        //method for the connection string (for the connection between console application and database)

        public static string GetConnectionString()
        {
            SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder();
            sqlConnectionStringBuilder.DataSource = "BETTY\\SQLEXPRESS";
            sqlConnectionStringBuilder.InitialCatalog = "Chinook";
            sqlConnectionStringBuilder.IntegratedSecurity = true;
            return sqlConnectionStringBuilder.ConnectionString;
        }
    }
}
