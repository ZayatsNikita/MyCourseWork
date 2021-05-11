using DL.Entities;
using DL.Extensions;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
namespace DL.Repositories
{
    public class ClientEntiryRepo : Abstract.Repository, Abstract.IClientEntiryRepo
    {
        private string addString = "INSERT INTO Client(Title, ContactInformation) values (@title, @c_info);SELECT LAST_INSERT_ID();";
        private string deleteString = "Delete from Client where id=@id; ";
        private string readString = "select * from client ";
        private string updateString = "update client ";
        public ClientEntiryRepo(string connectionString) : base(connectionString) {; }
        public void Create(ClientEntity client)
        {
            connection.Open();
            MySqlCommand command = new MySqlCommand(addString);
            MySqlParameter titleParam = new MySqlParameter("@title", client.Title);
            MySqlParameter contactInfoParam = new MySqlParameter("@c_info", client.ContactInformation);
            
            command.Parameters.Add(titleParam);
            command.Parameters.Add(contactInfoParam);
            command.Connection = connection;

            object obj= null;
            
            try
            {
               obj = command.ExecuteScalar();
            }
            finally
            {
                connection.Close();
            }

            int id = Convert.ToInt32(obj);
            client.Id = id;
        }
        public void Delete(ClientEntity clientEntity)
        {
            connection.Open();
            MySqlCommand command = new MySqlCommand(deleteString);
            MySqlParameter parameter = new MySqlParameter("@id", clientEntity.Id.ToString());
            command.Parameters.Add(parameter);
            command.Connection = connection;
            try
            {
                int delCount = command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }
        public List<ClientEntity> Read(int MinId=DefValInt, int MaxId= DefValInt, string title=null, string contactInformation = null)
        {
            
            string stringWithWhere = CreateWherePartForReadQuery(MinId, MaxId, title, contactInformation);
            
            MySqlCommand command= new MySqlCommand(readString+ stringWithWhere);
            
            command.Connection = connection;

            List<ClientEntity> result = new List<ClientEntity>();

            try
            {
                connection.Open();

                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    object id = reader["id"];
                    object titleFromDb = reader["Title"];
                    object ContactInformation = reader["ContactInformation"];
                    ClientEntity client = new ClientEntity
                    {
                        Id = System.Convert.ToInt32(id),
                        Title = System.Convert.ToString(titleFromDb),
                        ContactInformation = System.Convert.ToString(ContactInformation)
                    };
                    result.Add(client);
                }
            }
            finally
            {
                connection.Close();
            }
            return result;
        }
        public void Update(ClientEntity clientEntity, string title = null, string contactInformation = null)
        {
            connection.Open();
            
            string setString = CreateSetPartForUpdateQuery(title, contactInformation);

            MySqlCommand command = new MySqlCommand(updateString + setString + $" where id = {clientEntity.Id};")
            {
                Connection = connection
            };

            try
            {
                int updateCount = command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }
        private string CreateWherePartForReadQuery(int MinId , int MaxId , string title , string contactInformation)
        {
            StringBuilder query;
            if(MinId!=DefValInt || MaxId!= DefValInt || title!=null || contactInformation!=null)
            {
                query = new StringBuilder();
                
                query.AddWhereWord();
                
                query.AddWhereParam(MinId, MaxId, "id");

                query.AddWhereParam(title, "title");

                query.AddWhereParam(contactInformation, "contactInformation");

                return query.ToString();
            }
            else
            {
                return null;
            }
        }
        private string CreateSetPartForUpdateQuery(string title, string contactInfo)
        {
            if (title != null && contactInfo != null)
            {
                StringBuilder query = new StringBuilder();

                query.AddSetWord();

                query.AddSetParam(title, "title");

                query.AddSetParam(contactInfo, "contactInformation");

                return query.ToString();
            }
            else
            {
                return null;
            }
        }
    }
}
