//528 | 304
using DL.Entities;
using DL.Extensions;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace DL.Repositories
{
    public class OrderEntityRepository : Abstract.IOrderEntityRepository
    {
        private MySqlConnection connection;
        private string addString = "INSERT INTO Ordes (StartDate, ManagerId, MasterId, ClientId) values (@startDate, @managerId, @masterId,@clientId);SELECT LAST_INSERT_ID();";
        private string deleteString = "Delete from Ordes where id=@id; ";
        private string readString = "select * from Ordes ";
        private string updateString = "update Ordes ";
       
        public OrderEntityRepository(string connectionString)  
        {
            connection = new MySqlConnection(connectionString);
        }
        public int Create(OrderEntity order)
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
            finally
            {
                connection.Close();
            }

            int id = Convert.ToInt32(obj);
            order.Id = id;
            return id;

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
            finally
            {
                connection.Close();
            }
        }
        public List<OrderEntity> ReadOutstandingOrders(DateTime? from, DateTime? to)
        {
            string stringWithWhere = CreateStringForOutstandingOrders();

            MySqlCommand command = new MySqlCommand(readString + stringWithWhere);
            command.Connection = connection;

            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            IEnumerable<OrderEntity> result = GetOrderEntitiesFromDb(reader);
            connection.Close();
            if (from != null)
            {
                result = result.Where(x => x.StartDate >= from);
            }
            if (to != null)
            {
                result = result.Where(x => x.StartDate <= to);
            }

            return result.ToList();
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
            
            stringWithWhere = CreateWherePartForReadQuery(minId, maxId, minMasterId, maxMasterId, minManagerId, maxManagerId, minStartDate, maxStartDate, minCompletionDate, maxCompletionDate, minClientId, maxClientId);
            
            MySqlCommand command = new MySqlCommand(readString + stringWithWhere);
            command.Connection = connection;
            
            connection.Open();
            MySqlDataReader reader =  command.ExecuteReader();
            List<OrderEntity> result = GetOrderEntitiesFromDb(reader);

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
                
                query.AddWhereWord();
                
                query.AddWhereParam(minId, maxId, "id");
                
                query.AddWhereParam(minMasterId, maxMasterId, "MasterId");
                
                query.AddWhereParam(minManagerId, maxManagerId, "ManagerId");
                
                query.AddWhereParam(minClientId, maxClientId, "ClientId");
                
                query.AddWhereParam(minStartDate, maxStartDate, "StartDate");

                query.AddWhereParam(minCompletionDate, maxCompletionDate, "CompletionDate");

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
                where.AddSetWord();

                where.AddSetParam(ClientId, "ClientId");
                
                where.AddSetParam(MasterId, "MasterId");

                where.AddSetParam(ManagerId, "ManagerId");

                where.AddSetParam(CompletionDate ,"CompletionDate");

                where.AddSetParam(StartDate , "StartDate");
                
                return where.ToString();
            }
        }
        private string CreateStringForOutstandingOrders()
        {
            return " where  CompletionDate is null";
        }
        private string CreateStringForComplitedOrders()
        {
            return " where  CompletionDate is not null";
        }
        public List<OrderEntity> ReadComplitedOrders(DateTime? from, DateTime? to)
        {
            string stringWithWhere = CreateStringForComplitedOrders();
            MySqlCommand command = new MySqlCommand(readString + stringWithWhere);
            command.Connection = connection;
            connection.Open();
            MySqlDataReader reader = command.ExecuteReader();
            IEnumerable<OrderEntity> result = GetOrderEntitiesFromDb(reader);
            connection.Close();
            if (from != null)
            {
                result = result.Where(x => x.StartDate >= from);
            }
            if (to != null)
            {
                result = result.Where(x => x.CompletionDate <= to);
            }
            return result.ToList();
        }
        private List<OrderEntity> GetOrderEntitiesFromDb(MySqlDataReader reader)
        {
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
                    CompletionDate = null,
                    ClientId = System.Convert.ToInt32(clientIdFromDb),
                };
                if (completionDateFromDb != null)
                {
                    try
                    {
                        order.CompletionDate = System.Convert.ToDateTime(completionDateFromDb);
                    }
                    catch (Exception)
                    {
                        order.CompletionDate = null;
                    }
                }
                result.Add(order);
            }
            return result;
        }
    }
}
