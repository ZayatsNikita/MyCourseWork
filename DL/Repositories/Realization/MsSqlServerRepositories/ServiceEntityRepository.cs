using DL.Entities;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using DL.Extensions;
using DL.Repositories.Abstract;

namespace DL.Repositories
{
    public class ServiceEntityRepository : Repository, IServiceEntityRepository
    {
        private string addString = "INSERT INTO Services(Title, Description, Price) values (@title, @description, @price);SET @id=SCOPE_IDENTITY();";
        
        private string deleteString = "Delete from Services where id=@id;";
        
        private string readString = "select * from Services;";

        private string readByIdString = "select * from Services where id=@id;";

        private string updateString = "update Services set Title = @title, Description = @description, Price = @price where id=@id;";
        
        public ServiceEntityRepository(string connectionString)
            : base(connectionString)
        {
        }
       
        public int Create(ServiceEntity service)
        {
            connection.Open();
            var command = new SqlCommand(addString);
            var titleParam = new SqlParameter("@title", service.Title);
            var descriptionInfoParam = new SqlParameter("@description", service.Description);
            var priceParam = new SqlParameter("@price", service.Price.ToString().Replace(',','.'));
            var idParameter = new SqlParameter
            {
                ParameterName = "@id",
                Direction = System.Data.ParameterDirection.Output,
                DbType = System.Data.DbType.Int32,
            };

            command.Parameters.Add(idParameter);
            command.Parameters.Add(titleParam);
            command.Parameters.Add(descriptionInfoParam);
            command.Parameters.Add(priceParam);
            
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
            int id = Convert.ToInt32(idParameter.Value);

            service.Id = id;

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

        public List<ServiceEntity> Read()
        {
            var command= new SqlCommand(readString);
            
            command.Connection = connection;
            
            connection.Open();

            List<ServiceEntity> result = new List<ServiceEntity>();

            try
            {
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    object id = reader["id"];
                    object titleFromDb = reader["Title"];
                    object descriptionFromDb = reader["Description"];
                    object priceFromDb = reader["Price"];
                    ServiceEntity service = new ServiceEntity
                    {
                        Id = System.Convert.ToInt32(id),
                        Title = System.Convert.ToString(titleFromDb),
                        Description = System.Convert.ToString(descriptionFromDb),
                        Price = System.Convert.ToDecimal(priceFromDb),
                    };
                    result.Add(service);
                }
            }
            finally
            {
                connection.Close();
            }
            return result;
        }

        public ServiceEntity ReadById(int id)
        {
            var command = new SqlCommand(readString);

            command.Connection = connection;

            var idParam = new SqlParameter("@id", id);

            command.Parameters.Add(idParam);

            connection.Open();

            List<ServiceEntity> result = new List<ServiceEntity>();

            try
            {
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    object titleFromDb = reader["Title"];
                    object descriptionFromDb = reader["Description"];
                    object priceFromDb = reader["Price"];
                    ServiceEntity service = new ServiceEntity
                    {
                        Id = id,
                        Title = System.Convert.ToString(titleFromDb),
                        Description = System.Convert.ToString(descriptionFromDb),
                        Price = System.Convert.ToDecimal(priceFromDb),
                    };
                    result.Add(service);
                }
            }
            finally
            {
                connection.Close();
            }
            return result[0];
        }

        public void Update(ServiceEntity service)
        {
            connection.Open();
            
            var command = new SqlCommand(updateString);

            var idParam = new SqlParameter("@id", service.Id);
            var titleParam = new SqlParameter("@title", service.Title);
            var descriptionInfoParam = new SqlParameter("@description", service.Description);
            var priceParam = new SqlParameter("@price", service.Price.ToString().Replace(',', '.'));

            command.Parameters.Add(titleParam);
            command.Parameters.Add(descriptionInfoParam);
            command.Parameters.Add(priceParam);
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
