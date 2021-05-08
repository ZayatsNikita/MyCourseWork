using DL.Entities;
using System;
using System.Collections.Generic;
using DL.Extensions;
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
       
        public OrderInfoEntityRepository(string connectionString)  
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
            MySqlCommand command = new MySqlCommand(deleteString);
            MySqlParameter parameter = new MySqlParameter("@id", orderInfo.Id.ToString());
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
            int minId=-1,
            int maxId = -1,
            int minCountOfServicesRendered = -1,
            int maxCountOfServicesRendered = -1,
            int minServiceId = -1,
            int maxServiceId = -1,
            int minOrderNumber = -1,
            int maxOrderNumber = -1)
        {
            string stringWithWhere = CreateWherePartForReadQuery(minId, maxId, minCountOfServicesRendered, maxCountOfServicesRendered, minServiceId,
                maxServiceId, minOrderNumber, maxOrderNumber);
            
            
            MySqlCommand command= new MySqlCommand(readString + stringWithWhere);
            command.Connection = connection;

            List<OrderInfoEntity> result = new List<OrderInfoEntity>();

            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
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
            if(orderNumber == -1 && countOfServicesRendered == -1 && serviceId!=-1)
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
