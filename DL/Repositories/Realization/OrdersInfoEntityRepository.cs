using DL.Entities;
using System;
using System.Collections.Generic;
using DL.Extensions;
using System.Text;
using System.Data.SqlClient;

namespace DL.Repositories
{
    public class OrderInfoEntityRepository : Abstract.Repository, Abstract.IOrderInfoEntityRepository
    {
        
        private string addString = "INSERT INTO OrdersInfo (OrderNumber, CountOfServicesRendered, ServiceId) values (@orderNumber, @countOfServices, @serviceId);SELECT LAST_INSERT_ID();";
        
        private string deleteString = "Delete from OrdersInfo where id=@id; ";
        
        private string readString = "select * from OrdersInfo ";
        
        private string updateString = "update OrdersInfo ";
       
        public OrderInfoEntityRepository(string connectionString) :base(connectionString) {; }
        public void Create(OrderInfoEntity orderInfo)
        {
            connection.Open();

            var command = new SqlCommand(addString);
            
            var orderNumParam = new SqlParameter("@orderNumber", orderInfo.OrderNumber);
            var CountOfServicesRenderedParam = new SqlParameter("@countOfServices", orderInfo.CountOfServicesRendered);
            var ServiceIdParam = new SqlParameter("@serviceId", orderInfo.ServiceId);
            
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
            int id = Convert.ToInt32(obj);
            orderInfo.Id = id;
        }

        public void Delete(OrderInfoEntity orderInfo)
        {
            connection.Open();
            var command = new SqlCommand(deleteString);
            var parameter = new SqlParameter("@id", orderInfo.Id.ToString());
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

        public List<OrderInfoEntity> Read(
            int minId= DefValInt,
            int maxId = DefValInt,
            int minCountOfServicesRendered = DefValInt,
            int maxCountOfServicesRendered = DefValInt,
            int minServiceId = DefValInt,
            int maxServiceId = DefValInt,
            int minOrderNumber = DefValInt,
            int maxOrderNumber = DefValInt)
        {
            string stringWithWhere = CreateWherePartForReadQuery(minId, maxId, minCountOfServicesRendered, maxCountOfServicesRendered, minServiceId,
                maxServiceId, minOrderNumber, maxOrderNumber);
            
            
            var command= new SqlCommand(readString + stringWithWhere);
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

        public void Update(OrderInfoEntity orderInfo, int orderNumber = DefValInt, int countOfServicesRendered = DefValInt, int serviceId= DefValInt)
        {
            connection.Open();
            
            string setString = CreateSetPartForUpdateQuery(orderNumber, countOfServicesRendered, serviceId);
            
            var command = new SqlCommand(updateString + setString + $" where id = {orderInfo.Id};");

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

        private string CreateWherePartForReadQuery(int minId, int maxId,
            int minCountOfServicesRendered, int maxCountOfServicesRendered,
            int minServiceId, int maxServiceId,
            int minOrderNumber, int maxOrderNumber)
        {
            StringBuilder query;
            if(minId!= DefValInt || maxId!= DefValInt || minCountOfServicesRendered != DefValInt || maxCountOfServicesRendered!= DefValInt
                || minServiceId!= DefValInt || maxServiceId!= DefValInt || minOrderNumber!= DefValInt || maxOrderNumber!= DefValInt)
            {
                query = new StringBuilder();
                
                query.AddWhereWord();

                query.AddWhereParam(minId,maxId,"id");

                query.AddWhereParam(minCountOfServicesRendered, maxCountOfServicesRendered, "CountOfServicesRendered");

                query.AddWhereParam(minServiceId, maxServiceId, "ServiceId");
                
                query.AddWhereParam(minOrderNumber, maxOrderNumber, "OrderNumber");

                return query.ToString();
            }
            else
            {
                return null;
            }
        }
        private string CreateSetPartForUpdateQuery(int orderNumber, int countOfServicesRendered , int serviceId)
        {
            if(orderNumber == DefValInt && countOfServicesRendered == DefValInt && serviceId!= DefValInt)
            {
                return null;
            }
            else
            {
                StringBuilder query = new StringBuilder();
                
                query.AddSetWord();
                
                query.AddSetParam(orderNumber, "orderNumber");
                
                query.AddSetParam(countOfServicesRendered, "countOfServicesRendered");
                
                query.AddSetParam(serviceId, "serviceId");
                
                return query.ToString();
            }
        }
    }
}
