using DL.Entities;
using DL.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DL.Repositories
{
    public class ClientEntiryRepo : Repository, IClientEntiryRepo
    {
        private string addString = "INSERT INTO Clients (Title, ContactInformation) values (@title, @c_info);SET @id=SCOPE_IDENTITY();";
        
        private string deleteString = "Delete from Clients where id=@id; ";
        
        private string readString = "select * from Clients;";

        private string readByIdString = "select * from Clients where id=@id;";
        
        private string updateString = "update Clients set Title = @title, ContactInformation = @c_info where id = @id";
        
        public ClientEntiryRepo(string connectionString) : base(connectionString) {; }
        
        public int Create(ClientEntity client)
        {
            connection.Open();
            SqlCommand command = new SqlCommand(addString);
            SqlParameter titleParam = new SqlParameter("@title", client.Title);
            SqlParameter contactInfoParam = new SqlParameter("@c_info", client.ContactInformation);
            var idParameter = new SqlParameter
            {
                ParameterName = "@id",
                Direction = System.Data.ParameterDirection.Output,
                DbType = System.Data.DbType.Int32,
            };

            command.Parameters.Add(idParameter);
            command.Parameters.Add(titleParam);
            command.Parameters.Add(contactInfoParam);
            command.Connection = connection;

            
            try
            {
               command.ExecuteScalar();
            }
            finally
            {
                connection.Close();
            }

            int id = Convert.ToInt32(idParameter.Value);
            
            client.Id = id;

            return id;
        }
        
        public void Delete(int id)
        {
            connection.Open();
            var command = new SqlCommand(deleteString);
            var parameter = new SqlParameter("@id", id);
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

        public List<ClientEntity> Read()
        {
            var command= new SqlCommand(readString);
            
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

        public ClientEntity ReadById(int id)
        {
            var command = new SqlCommand(readByIdString);

            var parameter = new SqlParameter("@id", id);

            command.Parameters.Add(parameter);

            command.Connection = connection;

            List<ClientEntity> result = new List<ClientEntity>();

            try
            {
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    object returnedId = reader["id"];
                    object titleFromDb = reader["Title"];
                    object ContactInformation = reader["ContactInformation"];
                    ClientEntity client = new ClientEntity
                    {
                        Id = System.Convert.ToInt32(returnedId),
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

            return result[0];
        }

        public void Update(ClientEntity clientEntity)
        {
            connection.Open();
            
            SqlCommand command = new SqlCommand(addString);
            
            var titleParam = new SqlParameter("@title", clientEntity.Title);
            var contactInfoParam = new SqlParameter("@c_info", clientEntity.ContactInformation);
            var idParam = new SqlParameter("@id", clientEntity.Id);


            command.Parameters.Add(titleParam);
            command.Parameters.Add(contactInfoParam);
            command.Parameters.Add(idParam);

            command.Connection = connection;

            try
            {
                int updateCount = command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
