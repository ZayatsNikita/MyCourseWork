using DL.Entities;
using DL.Extensions;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DL.Repositories.Abstract;

namespace DL.Repositories
{
    public class OrderEntityRepository : Repository, IOrderEntityRepository
    {
        private string addString = "INSERT INTO Orders (StartDate, ManagerId, MasterId, ClientId) values (@startDate, @managerId, @masterId,@clientId);SET @id=SCOPE_IDENTITY();";
        
        private string deleteString = "Delete from Orders where id=@id;";
        
        private string readString = "select * from Orders;";

        private string readByIdString = "select * from Orders where id=@id;";

        private string updateString = "update Orders set StartDate = @startDate, ManagerId = @managerId, MasterId =  @masterId, ClientId = @clientId where id=@id;";

        private string outstandingOrders = "select * from Orders where CompletionDate is null";

        private string completedOrders = "select * from Orders where wøhere CompletionDate is not null;";

        public OrderEntityRepository(string connectionString) : base(connectionString) {; }
        
        public int Create(OrderEntity order)
        {
            connection.Open();

            var command = new SqlCommand(addString);
            var startDateParam = new SqlParameter("@startDate", order.StartDate);
            var managerParam = new SqlParameter("@managerId", order.ManagerId);
            var masterParam = new SqlParameter("@masterId", order.MasterId);
            var clientParam = new SqlParameter("@clientId", order.ClientId);
            var idParameter = new SqlParameter
            {
                ParameterName = "@id",
                Direction = System.Data.ParameterDirection.Output,
                DbType = System.Data.DbType.Int32,
            };

            command.Parameters.Add(idParameter);
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

            int id = Convert.ToInt32(idParameter.Value);
            order.Id = id;
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

        public List<OrderEntity> Read()
        {   
            var command = new SqlCommand(readString);
            command.Connection = connection;
            
            connection.Open();
            var reader =  command.ExecuteReader();
            List<OrderEntity> result = GetOrderEntitiesFromDb(reader);

            connection.Close();
            return result;
        }

        public OrderEntity ReadById(int id)
        {
            var command = new SqlCommand(readByIdString);
            command.Connection = connection;
            var idParam = new SqlParameter("@id", id);
            command.Parameters.Add(idParam);

            connection.Open();
            var reader = command.ExecuteReader();
            List<OrderEntity> result = GetOrderEntitiesFromDb(reader);

            connection.Close();
            return result[0];
        }

        public List<OrderEntity> ReadComplitedOrders()
        {
            var command = new SqlCommand(completedOrders);
            command.Connection = connection;

            connection.Open();
            var reader = command.ExecuteReader();
            List<OrderEntity> result = GetOrderEntitiesFromDb(reader);

            connection.Close();
            return result;
        }

        public List<OrderEntity> ReadOutstandingOrders()
        {
            var command = new SqlCommand(outstandingOrders);
            command.Connection = connection;

            connection.Open();
            var reader = command.ExecuteReader();
            List<OrderEntity> result = GetOrderEntitiesFromDb(reader);

            connection.Close();
            return result;
        }

        public void Update(OrderEntity order)
        {
            connection.Open();
                        
            var command = new SqlCommand(updateString);

            var startDateParam = new SqlParameter("@startDate", order.StartDate);
            var managerParam = new SqlParameter("@managerId", order.ManagerId);
            var masterParam = new SqlParameter("@masterId", order.MasterId);
            var clientParam = new SqlParameter("@clientId", order.ClientId);
            var idParam = new SqlParameter("@id", order.Id);

            command.Parameters.Add(startDateParam);
            command.Parameters.Add(managerParam);
            command.Parameters.Add(masterParam);
            command.Parameters.Add(clientParam);
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

        private List<OrderEntity> GetOrderEntitiesFromDb(SqlDataReader reader)
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
