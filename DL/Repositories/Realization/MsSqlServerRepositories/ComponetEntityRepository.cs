using DL.Entities;
using DL.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DL.Repositories
{
    public class ComponetEntityRepository : Repository, IComponetEntityRepository
    {
        private string addString = "INSERT INTO Components(Title,productionStandards, Price) values (@title,@st, @price);SET @id=SCOPE_IDENTITY();";
        
        private string deleteString = "Delete from Components where id=@id;";
        
        private string readString = "select * from Components";

        private string readByIdString = "select * from Components where id=@id;";
        
        private string updateString = "update Components set Title = @title, productionStandards = @st, Price = @price where id=@id;";

        public ComponetEntityRepository(string connectionString) : base(connectionString) {; }
        
        public int Create(ComponetEntity componet)
        {
            connection.Open();
            var command = new SqlCommand(addString);

            var titleParam = new SqlParameter("@title", componet.Title);
            var standartParam = new SqlParameter("@st", componet.ProductionStandards);
            var priceParam = new SqlParameter("@price", componet);
            var idParameter = new SqlParameter
            {
                ParameterName = "@id",
                Direction = System.Data.ParameterDirection.Output,
                DbType = System.Data.DbType.Int32,
            };

            command.Parameters.Add(idParameter);
            command.Parameters.Add(titleParam);
            command.Parameters.Add(priceParam);
            command.Parameters.Add(standartParam);


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
            
            componet.Id = id;

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

        public List<ComponetEntity> Read()
        {
            var command= new SqlCommand(readString);
            
            command.Connection = connection;

            List<ComponetEntity> result = new List<ComponetEntity>();

            try
            {
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    object id = reader["id"];
                    object titleFromDb = reader["Title"];
                    object priceFromDb = reader["Price"];
                    object productionStandardsFromDb = reader["productionStandards"];
                    ComponetEntity component = new ComponetEntity
                    {
                        Id = System.Convert.ToInt32(id),
                        Title = System.Convert.ToString(titleFromDb),
                        Price = System.Convert.ToDecimal(priceFromDb),
                        ProductionStandards = System.Convert.ToString(productionStandardsFromDb)
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

        public ComponetEntity ReadById(int id)
        {
            var command = new SqlCommand(readByIdString);

            command.Connection = connection;

            var idParam = new SqlParameter("@id", id);
            command.Parameters.Add(idParam);

            List<ComponetEntity> result = new List<ComponetEntity>();

            try
            {
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    object titleFromDb = reader["Title"];
                    object priceFromDb = reader["Price"];
                    object productionStandardsFromDb = reader["productionStandards"];
                    ComponetEntity component = new ComponetEntity
                    {
                        Id = id,
                        Title = System.Convert.ToString(titleFromDb),
                        Price = System.Convert.ToDecimal(priceFromDb),
                        ProductionStandards = System.Convert.ToString(productionStandardsFromDb)
                    };
                    result.Add(component);
                }
            }
            finally
            {
                connection.Close();
            }

            return result[0];
        }

        public void Update(ComponetEntity componet)
        {
            connection.Open();

            var command = new SqlCommand(updateString);
            command.Connection = connection;

            var titleParam = new SqlParameter("@title", componet.Title);
            var standartParam = new SqlParameter("@st", componet.ProductionStandards);
            var priceParam = new SqlParameter("@price", componet);
            var idParam = new SqlParameter("@id", componet.Id);

            command.Parameters.Add(titleParam);
            command.Parameters.Add(priceParam);
            command.Parameters.Add(standartParam);
            command.Parameters.Add(idParam);

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
