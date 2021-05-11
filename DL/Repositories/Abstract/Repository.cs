using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
namespace DL.Repositories.Abstract
{
    public abstract class Repository
    {
        public Repository(string connectionString)
        {
            this.connection = new MySqlConnection(connectionString);
        } 
        public const int DefValInt = 0;
        public const decimal DefValDec = 0;
        protected MySqlConnection connection;
    }
}
