using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
namespace DL.Repositories
{
    public class OrderInfoEntityRepository : Abstract.IOrderInfoEntityRepository
    {
        private MySqlConnection connection;
        private string addString = "INSERT INTO OrderInfo(OrderNumber, CountOfServicesRendered, ServiceId) values (@orderNumber, @countOfServices, @serviceId);SELECT LAST_INSERT_ID();";
        private string deleteString = "Delete from OrderInfo where id=@id; ";
        private string readString = "select * from OrderInfo ";
        private string updateString = "update OrderInfo ";
       
        public OrderInfoEntityRepository(string connectionString = @"Server=localhost;Port=3306;Database=work_fac;Uid=ForSomeCase;password=Kukrakuska713")  
        {
            connection = new MySqlConnection(connectionString);
        }
        public void Create(OrderInfoEntity orderInfo)
        {
            connection.Open();

            MySqlCommand command = new MySqlCommand(addString);
            
            MySqlParameter orderNumParam = new MySqlParameter("@orderNumber", orderInfo.OrderNumber);
            MySqlParameter CountOfServicesRenderedParam = new MySqlParameter("@countOfServices", orderInfo.CountOfServicesRendered);
            MySqlParameter ServiceIdParam = new MySqlParameter("@serviceId", orderInfo.ServiceId);
            
            command.Parameters.Add(orderNumParam);
            command.Parameters.Add(CountOfServicesRenderedParam);
            command.Parameters.Add(ServiceIdParam);

            command.Connection = connection;

            object obj= null;
            try
            {
               obj = command.ExecuteScalar();
            }
            catch(Exception)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
            if(obj!=null)
            {

                int id = Convert.ToInt32(obj);
                orderInfo.Id = id;
            }
            else
            {
                throw new ArgumentException("error of creating");
            }
        }

        public void Delete(OrderInfoEntity orderInfo)
        {
            connection.Open();
            MySqlCommand command = new MySqlCommand(deleteString);
            MySqlParameter parameter = new MySqlParameter("@id", orderInfo.Id.ToString());
            command.Parameters.Add(parameter);
            command.Connection = connection;
            try
            {
                int delCount = command.ExecuteNonQuery();
            }
            catch(Exception) {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        public List<OrderInfoEntity> Read(
            int minId=-1,
            int maxId = -1,
            int minCountOfServicesRendered = -1,
            int maxCountOfServicesRendered = -1,
            int minServiceId = -1,
            int maxServiceId = -1,
            int minOrderNumber = -1,
            int maxOrderNumber = -1)
        {
            string stringWithWhere = null;
            try
            {
                stringWithWhere = CreateWherePartForReadQuery(
                minId,
                maxId,
                minCountOfServicesRendered,
                maxCountOfServicesRendered,
                minServiceId,
                maxServiceId,
                minOrderNumber,
                maxOrderNumber
                );
            }
            catch (Exception)
            {
                throw;
            }
            MySqlCommand command= new MySqlCommand(readString + stringWithWhere);
            command.Connection = connection;
            
            connection.Open();
            MySqlDataReader reader =  command.ExecuteReader();
            List<OrderInfoEntity> result = new List<OrderInfoEntity>();
            while (reader.Read())
            {
                object id = reader["id"];
                object orderNumberFromDb = reader["OrderNumber"];
                object countOfServicesRenderedFromDb = reader["CountOfServicesRendered"];
                object serviceIdFromDb = reader["ServiceId"];
                OrderInfoEntity orderInfo = new OrderInfoEntity
                {
                    Id = System.Convert.ToInt32(id),
                    OrderNumber = System.Convert.ToInt32(orderNumberFromDb),
                    CountOfServicesRendered = System.Convert.ToInt32(countOfServicesRenderedFromDb),
                    ServiceId = Convert.ToInt32(serviceIdFromDb)
                };
                result.Add(orderInfo);
            }
            connection.Close();
            
            return result;
        }

        public void Update(OrderInfoEntity orderInfo, int orderNumber = -1, int countOfServicesRendered = -1, int serviceId=-1)
        {
            connection.Open();
            
            string setString = CreateSetPartForUpdateQuery(orderNumber, countOfServicesRendered, serviceId);
            
            MySqlCommand command = new MySqlCommand(updateString + setString + $" where id = {orderInfo.Id};");

            command.Connection = connection;
            try
            {
                int updateCount = command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }
        }

        private string CreateWherePartForReadQuery(int minId, int maxId,
            int minCountOfServicesRendered, int maxCountOfServicesRendered,
            int minServiceId, int maxServiceId,
            int minOrderNumber, int maxOrderNumber)
        {
            StringBuilder query;
            if(minId!=-1 || maxId!= -1 || minCountOfServicesRendered != -1 || maxCountOfServicesRendered!= -1 
                || minServiceId!=-1 || maxServiceId!=-1 || minOrderNumber!=-1 || maxOrderNumber!=-1)
            {
                query = new StringBuilder();
                query.Append(" where ");

                #region idFilter
                if (maxId != minId)
                {
                    if (minId != -1)
                    {
                        query.Append(" id>" + minId.ToString());
                    }
                    if (maxId != -1)
                    {
                        if(minId != -1)
                        {
                            query.Append(" and ");
                        }
                        query.Append(" id<" + maxId.ToString());
                    }
                }
                else
                {
                    if (maxId != -1)
                    {
                        query.Append(" id = " + maxId.ToString());
                    }
                }
                #endregion

                #region countOfServicesFileter
                if (minCountOfServicesRendered != maxCountOfServicesRendered)
                {
                    if (query.Length > 7)
                    {
                        query.Append(" and ");
                    }
                    if (minCountOfServicesRendered != -1)
                    {
                        query.Append(" CountOfServicesRendered>" + minCountOfServicesRendered.ToString());
                    }
                    if (maxCountOfServicesRendered != -1)
                    {
                        if (query.Length > 7)
                        {
                            query.Append(" and ");
                        }
                        query.Append(" CountOfServicesRendered <" + maxCountOfServicesRendered.ToString());
                    }
                }
                else
                {
                    if (maxCountOfServicesRendered != -1)
                    {
                        query.Append(" CountOfServicesRendered = " + maxCountOfServicesRendered.ToString());
                    }
                }
                #endregion

                #region serviseId
                if (minServiceId != maxServiceId)
                {
                    if (query.Length > 7)
                    {
                        query.Append(" and ");
                    }
                    if (minServiceId != -1)
                    {
                        query.Append(" ServiceId >" + minServiceId.ToString());
                    }
                    if (maxServiceId != -1)
                    {
                        if (query.Length > 7)
                        {
                            query.Append(" and ");
                        }
                        query.Append("ServiceId <" + maxServiceId.ToString());
                    }
                }
                else
                {
                    if (maxServiceId != -1)
                    {
                        query.Append(" ServiceId = " + maxServiceId.ToString());
                    }
                }
                #endregion

                #region OrderNumber
                if (minOrderNumber != maxOrderNumber)
                {
                    if (query.Length > 7)
                    {
                        query.Append(" and ");
                    }
                    if (minOrderNumber != -1)
                    {
                        query.Append(" OrderNumber >" + minOrderNumber.ToString());
                    }
                    if (maxOrderNumber != -1)
                    {
                        if (query.Length > 7)
                        {
                            query.Append(" and ");
                        }
                        query.Append(" OrderNumber <" + maxOrderNumber.ToString());
                    }
                }
                else
                {
                    if (maxOrderNumber != -1)
                    {
                        query.Append(" OrderNumber = " + maxOrderNumber.ToString());
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
        private string CreateSetPartForUpdateQuery(int orderNumber, int countOfServicesRendered , int serviceId)
        {
            if(orderNumber == -1 && countOfServicesRendered == -1 && serviceId!=-1)
            {
                return null;
            }
            else
            {
                StringBuilder where = new StringBuilder();
                where.Append(" set ");
                if (orderNumber != -1)
                {
                    where.Append("orderNumber = " + orderNumber.ToString());
                }
                if (countOfServicesRendered != -1)
                {
                    if(where.Length>5)
                    {
                        where.Append(" , ");
                    }
                    where.Append("countOfServicesRendered = " + countOfServicesRendered );
                }
                if (serviceId != -1)
                {
                    if (where.Length > 5)
                    {
                        where.Append(" , ");
                    }
                    where.Append("serviceId = " + serviceId);
                }
                return where.ToString();
            }
        }
    }

   


}
