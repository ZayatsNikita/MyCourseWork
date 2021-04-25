using DL.Entities;
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
        public СomponetServiceEntityRepo(string connectionString = @"Server=localhost;Port=3306;Database=work_fac;Uid=ForSomeCase;password=Kukrakuska713")
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
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
            if (obj != null)
            {

                int id = Convert.ToInt32(obj);
                сomponetServiceEntity.Id = id;
            }
            else
            {
                throw new ArgumentException("error of creating");
            }
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
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public List<СomponetServiceEntity> Read(int minId = -1, int maxId = -1, int minServiceId = -1, int maxServiceId = -1, int minComponetId = -1, int maxComponetId = -1)
        {
            string stringWithWhere = null;
            try
            {
                stringWithWhere = CreateWherePartForReadQuery(minId, maxId, minServiceId, maxServiceId, minComponetId, maxComponetId);
            }
            catch (Exception)
            {
                throw;
            }
            MySqlCommand command = new MySqlCommand(readString + stringWithWhere);
            command.Connection = connection;

            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            List<СomponetServiceEntity> result = new List<СomponetServiceEntity>();
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
            connection.Close();

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
            catch (Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        private string CreateWherePartForReadQuery(int minId, int maxId, int minServiceId, int maxServiceId, int minComponetId, int maxComponetId)
        {
            if (minId > maxId)
            {
                throw new ArgumentException("Wrong id params");
            }
            StringBuilder query;
            if (minId != -1 || maxId != -1 || minServiceId != -1 || maxServiceId != -1 || minComponetId != -1 || maxComponetId != -1)
            {
                query = new StringBuilder();
                query.Append(" where ");

                #region IdFilter
                if (minId != maxId)
                {
                    if (minId != -1)
                    {
                        if (query.Length > 7)
                        {
                            query.Append(" and ");
                        }
                        query.Append(" Id >" + minId.ToString());
                    }
                    if (maxId != -1)
                    {
                        if (query.Length > 7)
                        {
                            query.Append(" and ");
                        }
                        query.Append("Id <" + maxId.ToString());
                    }
                }
                else
                {
                    if (maxId != -1)
                    {
                        if (query.Length > 7)
                        {
                            query.Append(" and ");
                        }
                        query.Append(" Id = " + maxId.ToString());
                    }
                }
                #endregion

                #region ServiceIdFilter
                if (minServiceId != maxServiceId)
                {
                    if (minServiceId != -1)
                    {
                        if (query.Length > 7)
                        {
                            query.Append(" and ");
                        }
                        query.Append(" ServiceId >" + minServiceId.ToString());
                    }
                    if (maxServiceId != -1)
                    {
                        if (query.Length > 7)
                        {
                            query.Append(" and ");
                        }
                        query.Append(" ServiceId <" + maxServiceId.ToString());
                    }
                }
                else
                {
                    if (maxServiceId != -1)
                    {
                        if (query.Length > 7)
                        {
                            query.Append(" and ");
                        }
                        query.Append(" ServiceId = " + maxServiceId.ToString());
                    }
                }
                #endregion

                #region ComponetIdFilter
                if (minComponetId != maxComponetId)
                {
                    if (minComponetId != -1)
                    {
                        if (query.Length > 7)
                        {
                            query.Append(" and ");
                        }
                        query.Append(" ComponetId >" + minComponetId.ToString());
                    }
                    if (maxComponetId != -1)
                    {
                        if (query.Length > 7)
                        {
                            query.Append(" and ");
                        }
                        query.Append(" ComponetId <" + maxComponetId.ToString());
                    }
                }
                else
                {
                    if (maxComponetId != -1)
                    {
                        if (query.Length > 7)
                        {
                            query.Append(" and ");
                        }
                        query.Append(" ComponetId = " + maxComponetId.ToString());
                    }
                }
                #endregion

                return query.ToString();
            }
            else
            {
                return null;
            }
        }
        private string CreateSetPartForUpdateQuery(int ServiceId, int ComponetId)
        {
            if (ServiceId == -1 && ComponetId == -1)
            {
                return null;
            }
            else
            {
                StringBuilder where = new StringBuilder();
                where.Append(" set ");
                if (ServiceId != -1)
                {
                    where.Append("ServiceId = " + ServiceId.ToString());
                }
                if (ComponetId != -1)
                {
                    if (where.Length > 5)
                    {
                        where.Append(" , ");
                    }
                    where.Append("ComponetId = " + ComponetId.ToString());
                }
                return where.ToString();
            }
        }
    }
}
