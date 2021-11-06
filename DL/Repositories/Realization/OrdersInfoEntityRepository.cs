using DL.Entities;
using System;
using System.Collections.Generic;
using DL.Extensions;
using System.Text;
using System.Data.SqlClient;
using DL.Repositories.Abstract;

namespace DL.Repositories
{
    public class OrderInfoEntityRepository : Repository, IOrderInfoEntityRepository
    {
        
        private string addString = "INSERT INTO OrdersInfo (OrderNumber, CountOfServicesRendered, ServiceId) values (@orderNumber, @countOfServices, @serviceId);SET @id=SCOPE_IDENTITY();";
        
        private string deleteString = "Delete from OrdersInfo where id=@id; ";
        
        private string readString = "select * from OrdersInfo;";

        private string readByIdString = "select * from OrdersInfo where id=@id;";
        
        private string updateString = "update OrdersInfo;";
       
        public OrderInfoEntityRepository(string connectionString) :
            base(connectionString)
        {
        }

        public int Create(OrderInfoEntity orderInfo)
        {
            connection.Open();

            var command = new SqlCommand(addString);
            
            var orderNumParam = new SqlParameter("@orderNumber", orderInfo.OrderNumber);
            var CountOfServicesRenderedParam = new SqlParameter("@countOfServices", orderInfo.CountOfServicesRendered);
            var ServiceIdParam = new SqlParameter("@serviceId", orderInfo.ServiceId);
            var idParameter = new SqlParameter
            {
                ParameterName = "@id",
                Direction = System.Data.ParameterDirection.Output,
                DbType = System.Data.DbType.Int32,
            };

            command.Parameters.Add(idParameter);
            command.Parameters.Add(orderNumParam);
            command.Parameters.Add(CountOfServicesRenderedParam);
            command.Parameters.Add(ServiceIdParam);

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

            orderInfo.Id = id;

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

        public List<OrderInfoEntity> Read()
        {
            var command= new SqlCommand(readString);
            command.Connection = connection;

            List<OrderInfoEntity> result = new List<OrderInfoEntity>();

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
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
            }
            finally
            {
                connection.Close();
            }
            return result;
        }

        public OrderInfoEntity ReadById(int id)
        {
            var command = new SqlCommand(readByIdString);
            
            command.Connection = connection;

            List<OrderInfoEntity> result = new List<OrderInfoEntity>();

            var idParam = new SqlParameter("@id", id);

            command.Parameters.Add(idParam);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    object orderNumberFromDb = reader["OrderNumber"];
                    object countOfServicesRenderedFromDb = reader["CountOfServicesRendered"];
                    object serviceIdFromDb = reader["ServiceId"];
                    OrderInfoEntity orderInfo = new OrderInfoEntity
                    {
                        Id = id,
                        OrderNumber = System.Convert.ToInt32(orderNumberFromDb),
                        CountOfServicesRendered = System.Convert.ToInt32(countOfServicesRenderedFromDb),
                        ServiceId = Convert.ToInt32(serviceIdFromDb)
                    };
                    result.Add(orderInfo);
                }
            }
            finally
            {
                connection.Close();
            }

            return result[0];
        }

        public void Update(OrderInfoEntity orderInfo)
        {
            connection.Open();
            
            var command = new SqlCommand(updateString);

            var orderNumParam = new SqlParameter("@orderNumber", orderInfo.OrderNumber);
            var CountOfServicesRenderedParam = new SqlParameter("@countOfServices", orderInfo.CountOfServicesRendered);
            var ServiceIdParam = new SqlParameter("@serviceId", orderInfo.ServiceId);
            var idParam = new SqlParameter("@id", orderInfo.Id);

            command.Parameters.Add(orderNumParam);
            command.Parameters.Add(CountOfServicesRenderedParam);
            command.Parameters.Add(ServiceIdParam);
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
