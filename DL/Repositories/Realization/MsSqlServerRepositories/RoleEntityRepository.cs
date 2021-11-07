using DL.Entities;
using DL.Extensions;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using DL.Repositories.Abstract;

namespace DL.Repositories
{
    public class RoleEntityRepository : Repository, IRoleEntityRepository
    {
        private string addString = "INSERT INTO Roles(Title, Description, userid) values (@title, @descrip);SET @id=SCOPE_IDENTITY();";
        
        private string deleteString = "Delete from Roles where id=@id;";
        
        private string readString = "select * from Roles;";

        private string readByIdString = "select * from Roles where id=@id;";

        private string updateString = "update Roles set Title = @title, Description = @descrip where id = @id";
        
        public RoleEntityRepository(string connectionString) 
            : base(connectionString)
        {
        }
        
        public int Create(RoleEntity role)
        {
            connection.Open();

            var command = new SqlCommand(addString);
            var titleParam = new SqlParameter("@title", role.Title);
            var descriptionParam = new SqlParameter("@descrip", role.Description);
            var idParameter = new SqlParameter
            {
                ParameterName = "@id",
                Direction = System.Data.ParameterDirection.Output,
                DbType = System.Data.DbType.Int32,
            };

            command.Parameters.Add(idParameter);
            command.Parameters.Add(titleParam);
            command.Parameters.Add(descriptionParam);

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

            role.Id = id;

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

        public List<RoleEntity> Read()
        {
            var command= new SqlCommand(readString);
            
            command.Connection = connection;
            
            List<RoleEntity> result = new List<RoleEntity>();

            connection.Open();

            try
            {
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    object id = reader["id"];
                    object titleFromDb = reader["Title"];
                    object descriptionFromDb = reader["Description"];
                    object accsesLevelFromDb = 1;
                    RoleEntity role = new RoleEntity
                    {
                        Id = System.Convert.ToInt32(id),
                        Title = System.Convert.ToString(titleFromDb),
                        Description = System.Convert.ToString(descriptionFromDb),
                    };
                    result.Add(role);
                }
            }
            finally
            {
                connection.Close();
            }

            return result;
        }

        public RoleEntity ReadById(int id)
        {
            var command = new SqlCommand(readByIdString);

            var idParam = new SqlParameter("@id", id);

            command.Parameters.Add(idParam);

            command.Connection = connection;

            List<RoleEntity> result = new List<RoleEntity>();

            connection.Open();

            try
            {
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    object titleFromDb = reader["Title"];
                    object descriptionFromDb = reader["Description"];
                    object accsesLevelFromDb = 1;
                    RoleEntity role = new RoleEntity
                    {
                        Id = id,
                        Title = System.Convert.ToString(titleFromDb),
                        Description = System.Convert.ToString(descriptionFromDb),
                    };
                    result.Add(role);
                }
            }
            finally
            {
                connection.Close();
            }

            return result[0];
        }

        public void Update(RoleEntity role)
        {
            connection.Open();

            var command = new SqlCommand(updateString);

            var titleParam = new SqlParameter("@title", role.Title);
            var descriptionParam = new SqlParameter("@descrip", role.Description);
            var idParam = new SqlParameter("@id", role.Id);

            command.Parameters.Add(titleParam);
            command.Parameters.Add(descriptionParam);
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
