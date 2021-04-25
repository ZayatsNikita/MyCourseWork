using DL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
namespace DL.Repositories
{
    public class OrderEntityRepository : Abstract.IOrderEntityRepository
    {
        private MySqlConnection connection;
        private string addString = "INSERT INTO Orders (StartDate, ManagerId, MasterId, ClientId) values (@startDate, @managerId, @masterId,@clientId);SELECT LAST_INSERT_ID();";
        private string deleteString = "Delete from Orders where id=@id; ";
        private string readString = "select * from Orders ";
        private string updateString = "update Orders ";
       
        public OrderEntityRepository(string connectionString = @"Server=localhost;Port=3306;Database=work_fac;Uid=ForSomeCase;password=Kukrakuska713")  
        {
            connection = new MySqlConnection(connectionString);
        }
        public void Create(OrderEntity order)
        {
            connection.Open();

            MySqlCommand command = new MySqlCommand(addString);
            MySqlParameter startDateParam = new MySqlParameter("@startDate", order.StartDate);
            MySqlParameter managerParam = new MySqlParameter("@managerId", order.ManagerId);
            MySqlParameter masterParam = new MySqlParameter("@masterId", order.MasterId);
            MySqlParameter clientParam = new MySqlParameter("@clientId", order.ClientId);
            
            command.Parameters.Add(startDateParam);
            command.Parameters.Add(managerParam);
            command.Parameters.Add(masterParam);
            command.Parameters.Add(clientParam);

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
                order.Id = id;
            }
            else
            {
                throw new ArgumentException("error of creating");
            }
        }

        public void Delete(OrderEntity order)
        {
            connection.Open();
            MySqlCommand command = new MySqlCommand(deleteString);
            MySqlParameter parameter = new MySqlParameter("@id", order.Id.ToString());
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
        public List<OrderEntity> ReadOutstandingOrders()
        {
            string stringWithWhere = CreateStringForOutstandingOrders();

            MySqlCommand command = new MySqlCommand(readString + stringWithWhere);
            command.Connection = connection;

            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            List<OrderEntity> result = new List<OrderEntity>();
            while (reader.Read())
            {
                object id = reader["id"];
                object masterIdFromDb = reader["MasterId"];
                object clientIdFromDb = reader["ClientId"];
                object managerIdFromDb = reader["ManagerId"];
                object startDateFromDb = reader["StartDate"];
                object completionDateFromDb = reader["CompletionDate"];

                OrderEntity order = new OrderEntity
                {
                    Id = System.Convert.ToInt32(id),
                    MasterId = System.Convert.ToInt32(masterIdFromDb),
                    ManagerId = System.Convert.ToInt32(managerIdFromDb),
                    StartDate = System.Convert.ToDateTime(startDateFromDb),
                    CompletionDate = System.Convert.ToDateTime(completionDateFromDb),
                    ClientId = System.Convert.ToInt32(clientIdFromDb),
                };
                result.Add(order);
            }
            connection.Close();

            return result;
        }

        public List<OrderEntity> Read(
            int minId=-1,
            int maxId=-1, 
            
            int minMasterId=-1,
            int maxMasterId = -1,

            int minManagerId = -1,
            int maxManagerId = -1,

            DateTime? minStartDate = null,
            DateTime? maxStartDate = null,

            DateTime? minCompletionDate = null,
            DateTime? maxCompletionDate = null,

            int minClientId = -1,
            int maxClientId = -1
            )
        {
            string stringWithWhere = null;
            try
            {
                stringWithWhere = CreateWherePartForReadQuery(minId, maxId, minMasterId, maxMasterId, minManagerId, maxManagerId, minStartDate, maxStartDate, minCompletionDate, maxCompletionDate, minClientId, maxClientId);
            }
            catch (Exception)
            {
                throw;
            }
            MySqlCommand command = new MySqlCommand(readString + stringWithWhere);
            command.Connection = connection;
            
            connection.Open();
            MySqlDataReader reader =  command.ExecuteReader();
            List<OrderEntity> result = new List<OrderEntity>();
            while (reader.Read())
            {
                object id = reader["id"];
                object masterIdFromDb = reader["MasterId"];
                object clientIdFromDb = reader["ClientId"];
                object managerIdFromDb = reader["ManagerId"];
                object startDateFromDb = reader["StartDate"];
                object completionDateFromDb = reader["CompletionDate"];

                OrderEntity order = new OrderEntity
                {
                    Id = System.Convert.ToInt32(id),
                    MasterId = System.Convert.ToInt32(masterIdFromDb),
                    ManagerId = System.Convert.ToInt32(managerIdFromDb),
                    StartDate = System.Convert.ToDateTime(startDateFromDb),
                    CompletionDate = System.Convert.ToDateTime(completionDateFromDb),
                    ClientId = System.Convert.ToInt32(clientIdFromDb),
                };
                result.Add(order);
            }
            connection.Close();
            
            return result;
        }

        public void Update(
            OrderEntity order, int ClientId,
            int MasterId=-1,
            int ManagerId = -1,
            DateTime? StartDate = null,
            DateTime? CompletionDate= null)
        {
            connection.Open();
            
            string setString = CreateSetPartForUpdateQuery(ClientId, MasterId, ManagerId, StartDate, CompletionDate);
            
            MySqlCommand command = new MySqlCommand(updateString + setString + $" where id = {order.Id};");

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

        private string CreateWherePartForReadQuery(
            int minId,
            int maxId,

            int minMasterId,
            int maxMasterId,

            int minManagerId,
            int maxManagerId,

            DateTime? minStartDate,
            DateTime? maxStartDate,

            DateTime? minCompletionDate,
            DateTime? maxCompletionDate,

            int minClientId,
            int maxClientId)
        {
            
            StringBuilder query;
            if(
                minId != -1 || maxId != -1 
                || minMasterId != -1 || minMasterId != -1 
                || minManagerId != -1 || maxManagerId != -1 
                || minStartDate != null || maxStartDate != null 
                || minCompletionDate != null || maxCompletionDate != null 
                || minClientId != -1 || maxClientId != -1)
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

                #region MasterIdFilter
                if (minMasterId != maxMasterId)
                {
                    if (minMasterId != -1)
                    {
                        if (query.Length > 7)
                        {
                            query.Append(" and ");
                        }
                        query.Append(" MasterId >" + minMasterId.ToString());
                    }
                    if (maxMasterId != -1)
                    {
                        if (query.Length > 7)
                        {
                            query.Append(" and ");
                        }
                        query.Append(" MasterId <" + maxMasterId.ToString());
                    }
                }
                else
                {
                    if (maxMasterId != -1)
                    {
                        if (query.Length > 7)
                        {
                            query.Append(" and ");
                        }
                        query.Append(" MasterId = " + maxMasterId.ToString());
                    }
                }
                #endregion

                #region ManagerIdFilter
                if (minManagerId != maxManagerId)
                {
                    if (minManagerId != -1)
                    {
                        if (query.Length > 7)
                        {
                            query.Append(" and ");
                        }
                        query.Append(" ManagerId >" + minManagerId.ToString());
                    }
                    if (maxManagerId != -1)
                    {
                        if (query.Length > 7)
                        {
                            query.Append(" and ");
                        }
                        query.Append(" ManagerId <" + maxManagerId.ToString());
                    }
                }
                else
                {
                    if (maxManagerId != -1)
                    {
                        if (query.Length > 7)
                        {
                            query.Append(" and ");
                        }
                        query.Append(" ManagerId = " + maxManagerId.ToString());
                    }
                }
                #endregion

                #region StartDateFilter
                if (minStartDate != maxStartDate)
                {
                    if (minStartDate != null)
                    {
                        if (query.Length > 7)
                        {
                            query.Append(" and ");
                        }
                        query.Append(" StartDate >" + minStartDate.ToString());
                    }
                    if (maxStartDate != null)
                    {
                        if (query.Length > 7)
                        {
                            query.Append(" and ");
                        }
                        query.Append(" StartDate <" + maxStartDate.ToString());
                    }
                }
                else
                {
                    if (maxStartDate != null)
                    {
                        if (query.Length > 7)
                        {
                            query.Append(" and ");
                        }
                        query.Append(" StartDate = " + maxStartDate.ToString());
                    }
                }
                #endregion

                #region CompletionDateFilter
                if (minCompletionDate != maxCompletionDate)
                {
                    if (minCompletionDate != null)
                    {
                        if (query.Length > 7)
                        {
                            query.Append(" and ");
                        }
                        query.Append(" CompletionDate >" + minCompletionDate.ToString());
                    }
                    if (maxCompletionDate != null)
                    {
                        if (query.Length > 7)
                        {
                            query.Append(" and ");
                        }
                        query.Append(" CompletionDate <" + maxCompletionDate.ToString());
                    }
                }
                else
                {
                    if (maxCompletionDate != null)
                    {
                        if (query.Length > 7)
                        {
                            query.Append(" and ");
                        }
                        query.Append(" CompletionDate = " + maxCompletionDate.ToString());
                    }
                }
                #endregion

                #region clientIdFilter
                if (minClientId != maxClientId)
                {
                    if (minClientId != -1)
                    {
                        if (query.Length > 7)
                        {
                            query.Append(" and ");
                        }
                        query.Append(" ClientId >" + minClientId.ToString());
                    }
                    if (maxClientId != -1)
                    {
                        if (query.Length > 7)
                        {
                            query.Append(" and ");
                        }
                        query.Append(" ClientId <" + maxClientId.ToString());
                    }
                }
                else
                {
                    if (maxClientId != -1)
                    {
                        if (query.Length > 7)
                        {
                            query.Append(" and ");
                        }
                        query.Append(" ClientId = " + maxClientId.ToString());
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
        private string CreateSetPartForUpdateQuery(int ClientId,
            int MasterId,
            int ManagerId,
            DateTime? StartDate,
            DateTime? CompletionDate)
        {
            if(StartDate == null && CompletionDate == null && MasterId == -1 && ManagerId ==-1 && ClientId == -1)
            {
                return null;
            }
            else
            {
                StringBuilder where = new StringBuilder();
                where.Append(" set ");
                if (ClientId != -1)
                {
                    if (where.Length > 5)
                    {
                        where.Append(" , ");
                    }
                    where.Append(" ClientId = " + ClientId.ToString());
                }
                if (MasterId != -1)
                {
                    if (where.Length > 5)
                    {
                        where.Append(" , ");
                    }
                    where.Append(" MasterId = " + MasterId.ToString());
                }
                if (ManagerId != -1)
                {
                    if (where.Length > 5)
                    {
                        where.Append(" , ");
                    }
                    where.Append(" ManagerId = " + ManagerId.ToString());
                }
                if (StartDate != null)
                {
                    if (where.Length > 5)
                    {
                        where.Append(" , ");
                    }
                    where.Append(" StartDate = " + StartDate.ToString());
                }
                if (CompletionDate != null)
                {
                    if (where.Length > 5)
                    {
                        where.Append(" , ");
                    }
                    where.Append(" CompletionDate = " + CompletionDate.ToString());
                }
                return where.ToString();
            }
        }
        private string CreateStringForOutstandingOrders()
        {
            return " where  CompletionDate = null";
        }


    }

   


}
