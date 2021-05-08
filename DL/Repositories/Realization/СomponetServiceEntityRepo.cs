using DL.Entities;
using DL.Extensions;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace DL.Repositories
{
    public class СomponetServiceEntityRepo : Abstract.IСomponetServiceEntityRepo
    {
        private MySqlConnection connection;
        private string addString = "INSERT INTO componetservice (ComponetId, ServiceId) values (@ComponetId, @ServiceId);SELECT LAST_INSERT_ID();";
        private string deleteString = "Delete from componetservice where id=@id";
        private string readString = "select * from componetservice ";
        private string updateString = "update componetservice ";
        public СomponetServiceEntityRepo(string connectionString)
        {
            connection = new MySqlConnection(connectionString);
        }
        public void Create(СomponetServiceEntity сomponetServiceEntity)
        {
            connection.Open();
            MySqlCommand command = new MySqlCommand(addString);
            MySqlParameter titleParam = new MySqlParameter("@ComponetId", сomponetServiceEntity.ComponetId);
            MySqlParameter contactInfoParam = new MySqlParameter("@ServiceId", сomponetServiceEntity.ServiceId);

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
            int id = Convert.ToInt32(obj);
            сomponetServiceEntity.Id = id;
        }

        public void Delete(СomponetServiceEntity сomponetServiceEntity)
        {
            connection.Open();
            MySqlCommand command = new MySqlCommand(deleteString);
            MySqlParameter parameter = new MySqlParameter("@id", сomponetServiceEntity.Id.ToString());
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

        public List<СomponetServiceEntity> Read(int minId = -1, int maxId = -1, int minServiceId = -1, int maxServiceId = -1, int minComponetId = -1, int maxComponetId = -1)
        {
            string stringWithWhere = CreateWherePartForReadQuery(minId, maxId, minServiceId, maxServiceId, minComponetId, maxComponetId);
            
            MySqlCommand command = new MySqlCommand(readString + stringWithWhere);
            command.Connection = connection;

            connection.Open();
            List<СomponetServiceEntity> result = new List<СomponetServiceEntity>();
            try
            {
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    object idFromDb = reader["Id"];
                    object ServiceIdFromDb = reader["ServiceId"];
                    object ComponetIdFromDb = reader["ComponetId"];
                    СomponetServiceEntity componentServes = new СomponetServiceEntity
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

        public void Update(СomponetServiceEntity сomponetServiceEntity, int serviceId = -1, int componetId = -1)
        {
            connection.Open();

            string setString = CreateSetPartForUpdateQuery(serviceId, componetId);

            MySqlCommand command = new MySqlCommand(updateString + setString + $" where id = {сomponetServiceEntity.Id};");

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

        private string CreateWherePartForReadQuery(int minId, int maxId, int minServiceId, int maxServiceId, int minComponetId, int maxComponetId)
        {
            if (minId != -1 || maxId != -1 || minServiceId != -1 || maxServiceId != -1 || minComponetId != -1 || maxComponetId != -1)
            {
                StringBuilder query = new StringBuilder();
                
                query.AddWhereWord();

                query.AddWhereParam(minId, maxId, "Id");

                query.AddWhereParam(minServiceId, maxServiceId, "ServiceId");

                query.AddWhereParam(minComponetId, maxComponetId, "ComponetId");

                return query.ToString();
            }
            else
            {
                return null;
            }
        }
        private string CreateSetPartForUpdateQuery(int serviceId, int componetId)
        {
            if (serviceId == -1 && componetId == -1)
            {
                return null;
            }
            else
            {
                StringBuilder where = new StringBuilder();
                
                where.AddSetWord();
                
                where.AddSetParam(serviceId, "ServiceId");
                
                where.AddSetParam(componetId, "ComponetId");
                
                return where.ToString();
            }
        }
    }
}
