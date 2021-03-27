using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
namespace DL.Repositories
{
    public class ClientEntiryRepo : Abstract.IClientEntiryRepo
    {
        private MySqlConnection connection;
        private string addString = "INSERT INTO Client[(Title, ContactInformation)] values (@title, @c_info);SELECT LAST_INSERT_ID();";
        public ClientEntiryRepo(string connectionString = @"Driver={MySQL ODBC 5.3 Unicode Driver}; Server = localhost; Database = work_fac; UID = root; PWD = Kukrakuska713")
        {
            connection = new MySqlConnection();
            connection.ConnectionString = connectionString;
        }
        public object Create(ClientEntity client)
        {
            MySqlCommand command = new MySqlCommand(addString);
            MySqlParameter titleParam = new MySqlParameter("@title", client.Title);
            MySqlParameter contactInfoParam = new MySqlParameter("@c_info", client.ContactInformation);
            
            command.Parameters.Add(titleParam);
            command.Parameters.Add(contactInfoParam);

            MySqlDataReader reader = command.ExecuteReader();
            object id = null;
            if (reader.HasRows)
            {
                id = reader.GetValue(0);
            }
            return id;
        }

        public void Delete(ClientEntity clientEntity)
        {
            throw new NotImplementedException();
        }

        public List<ClientEntity> Read()
        {
            throw new NotImplementedException();
        }

        public void Update(ClientEntity clientEntity)
        {
            throw new NotImplementedException();
        }
    }

   


}
