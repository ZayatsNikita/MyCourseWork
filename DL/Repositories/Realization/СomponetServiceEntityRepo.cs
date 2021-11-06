using DL.Entities;
using DL.Extensions;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using DL.Repositories.Abstract;

namespace DL.Repositories
{
    public class СomponetServiceEntityRepo : Repository, IСomponetServiceEntityRepo
    {
        private string addString = "INSERT INTO ServiceComponents (ComponetId, ServiceId) values (@ComponetId, @ServiceId);SET @id=SCOPE_IDENTITY();";
        
        private string deleteString = "Delete from ServiceComponents where id=@id;";
        
        private string readString = "select * from ServiceComponents;";

        private string readByIdString = "select * from ServiceComponents where id=@id;";
        
        private string updateString = "update ServiceComponents set ComponetId = @ComponetId, ServiceId = @ServiceId where id=@id;";
        
        public СomponetServiceEntityRepo(string connectionString) 
            : base(connectionString)
        {
        }
        
        public int Create(ServiceComponentsEntity сomponetServiceEntity)
        {
            connection.Open();
            var command = new SqlCommand(addString);
            var titleParam = new SqlParameter("@ComponetId", сomponetServiceEntity.ComponetId);
            var contactInfoParam = new SqlParameter("@ServiceId", сomponetServiceEntity.ServiceId);
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

            object obj = null;
            try
            {
                obj = command.ExecuteScalar();
            }
            finally
            {
                connection.Close();
            }
            int id = Convert.ToInt32(idParameter.Value);
            
            сomponetServiceEntity.Id = id;

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

        public List<ServiceComponentsEntity> Read()
        {
            var command = new SqlCommand(readString);
            
            command.Connection = connection;

            connection.Open();
            List<ServiceComponentsEntity> result = new List<ServiceComponentsEntity>();
            try
            {
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    object idFromDb = reader["Id"];
                    object ServiceIdFromDb = reader["ServiceId"];
                    object ComponetIdFromDb = reader["ComponetId"];
                    ServiceComponentsEntity componentServes = new ServiceComponentsEntity
                    {
                        Id = System.Convert.ToInt32(idFromDb),
                        ServiceId = System.Convert.ToInt32(ServiceIdFromDb),
                        ComponetId = System.Convert.ToInt32(ComponetIdFromDb)
                    };
                    result.Add(componentServes);
                }
            }
            finally
            {
                connection.Close();
            }
            return result;
        }

        public ServiceComponentsEntity ReadById(int id)
        {
            var command = new SqlCommand(readByIdString);

            command.Connection = connection;

            connection.Open();

            var idParam = new SqlParameter("@id", id);
            
            command.Parameters.Add(idParam);

            List<ServiceComponentsEntity> result = new List<ServiceComponentsEntity>();
            
            try
            {
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    object idFromDb = reader["Id"];
                    object ServiceIdFromDb = reader["ServiceId"];
                    object ComponetIdFromDb = reader["ComponetId"];
                    ServiceComponentsEntity componentServes = new ServiceComponentsEntity
                    {
                        Id = System.Convert.ToInt32(idFromDb),
                        ServiceId = System.Convert.ToInt32(ServiceIdFromDb),
                        ComponetId = System.Convert.ToInt32(ComponetIdFromDb)
                    };
                    result.Add(componentServes);
                }
            }
            finally
            {
                connection.Close();
            }
            return result[0];
        }

        public void Update(ServiceComponentsEntity сomponetServiceEntity)
        {
            connection.Open();

            var command = new SqlCommand(updateString);
            var idParam = new SqlParameter("@id", сomponetServiceEntity.Id);
            var titleParam = new SqlParameter("@ComponetId", сomponetServiceEntity.ComponetId);
            var contactInfoParam = new SqlParameter("@ServiceId", сomponetServiceEntity.ServiceId);

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
