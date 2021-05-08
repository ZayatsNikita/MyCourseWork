using DL.Entities;
using DL.Extensions;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
namespace DL.Repositories
{
    public class ComponetEntityRepository : Abstract.IComponetEntityRepository
    {
        private MySqlConnection connection;
        private string addString = "INSERT INTO Componet(Title, Price) values (@title, @price);SELECT LAST_INSERT_ID();";
        private string deleteString = "Delete from Componet where id=@id; ";
        private string readString = "select * from Componet ";
        private string updateString = "update Componet ";
        public ComponetEntityRepository(string connectionString)  
        {
            connection = new MySqlConnection(connectionString);
        }
        public void Create(ComponetEntity componet)
        {
            connection.Open();
            MySqlCommand command = new MySqlCommand(addString);

            MySqlParameter titleParam = new MySqlParameter("@title", componet.Title);
            MySqlParameter priceParam = new MySqlParameter("@price", componet.Price.ToString().Replace(',','.'));

            command.Parameters.Add(titleParam);
            command.Parameters.Add(priceParam);

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
            componet.Id = id;
        }

        public void Delete(ComponetEntity component)
        {
            connection.Open();
            MySqlCommand command = new MySqlCommand(deleteString);
            MySqlParameter parameter = new MySqlParameter("@id", component.Id.ToString());
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

        public List<ComponetEntity> Read(int minId = -1, int maxId = -1,  string title = null, decimal minPrice = -1, decimal maxPrice = -1)
        {
            string stringWithWhere = CreateWherePartForReadQuery(minId, maxId, title, minPrice, maxPrice);
            
            MySqlCommand command= new MySqlCommand(readString + stringWithWhere);
            
            command.Connection = connection;

            List<ComponetEntity> result = new List<ComponetEntity>();

            try
            {
                connection.Open();
                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    object id = reader["id"];
                    object titleFromDb = reader["Title"];
                    object priceFromDb = reader["Price"];
                    ComponetEntity component = new ComponetEntity
                    {
                        Id = System.Convert.ToInt32(id),
                        Title = System.Convert.ToString(titleFromDb),
                        Price = System.Convert.ToDecimal(priceFromDb)
                    };
                    result.Add(component);
                }
            }
            finally
            {
                connection.Close();
            }
            return result;
        }

        public void Update(ComponetEntity componet, string title=null, decimal price = -1)
        {
            connection.Open();
            
            string setString = CreateSetPartForUpdateQuery(title, price);
            
            MySqlCommand command = new MySqlCommand(updateString + setString + $" where id = {componet.Id};");

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

        private string CreateWherePartForReadQuery(int minId , int maxId , string title , decimal minPrice, decimal maxPrice)
        {
            if(minId!=-1 || maxId!= -1 || title!=null || minPrice != -1 || maxPrice != -1)
            {
                StringBuilder query;

                query = new StringBuilder();
                
                query.AddWhereWord();
                
                query.AddWhereParam(minId, maxId, "id");

                query.AddWhereParam(minPrice, maxPrice, "Price");
                
                query.AddWhereParam(title, "title");
                
                return query.ToString();
            }
            else
            {
                return null;
            }
        }
        private string CreateSetPartForUpdateQuery(string title, decimal price)
        {
            if(title==null && price ==-1)
            {
                return null;
            }
            else
            {
                StringBuilder where = new StringBuilder();
                
                where.AddSetWord();

                where.AddSetParam(title, "title");

                where.AddSetParam(price, "Price");
                
                return where.ToString();
            }
        }
    }
}
