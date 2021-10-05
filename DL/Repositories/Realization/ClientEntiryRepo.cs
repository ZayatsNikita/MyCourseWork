using DL.Entities;
using DL.Extensions;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
namespace DL.Repositories
{
    public class ClientEntiryRepo : Abstract.Repository, Abstract.IClientEntiryRepo
    {
        private string addString = "INSERT INTO Clients (Title, ContactInformation) values (@title, @c_info);SELECT LAST_INSERT_ID();";
        
        private string deleteString = "Delete from Clients where id=@id; ";
        
        private string readString = "select * from Clients ";
        
        private string updateString = "update Clients ";
        
        public ClientEntiryRepo(string connectionString) : base(connectionString) {; }
        
        public void Create(ClientEntity client)
        {
            connection.Open();
            SqlCommand command = new SqlCommand(addString);
            SqlParameter titleParam = new SqlParameter("@title", client.Title);
            SqlParameter contactInfoParam = new SqlParameter("@c_info", client.ContactInformation);
            
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
            var command = new SqlCommand(deleteString);
            var parameter = new SqlParameter("@id", clientEntity.Id.ToString());
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
            
            var command= new SqlCommand(readString+ stringWithWhere);
            
            command.Connection = connection;

            List<ClientEntity> result = new List<ClientEntity>();

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

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

            var command = new SqlCommand(updateString + setString + $" where id = {clientEntity.Id};")
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
