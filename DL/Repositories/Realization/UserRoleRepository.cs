using DL.Entities;
using DL.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace DL.Repositories
{
    public class UserRoleRepository : Repository,  IUserRoleRepository
    {
        private string addString = "INSERT INTO UserRoles (UserId, RoleId) values (@UserId, @RoleId);SET @id=SCOPE_IDENTITY();";
        
        private string deleteString = "Delete from UserRoles where id=@id";
        
        private string readString = "select * from UserRoles;";
        
        private string readByIdString = "select * from UserRoles where id=@id;";
        
        private string updateString = "update UserRoles set UserId = @UserId, RoleId = @RoleId where id=@id;";
        
        public UserRoleRepository(string connectionString) : base(connectionString) {; }
        
        public int Create(UserRoleEntity userRole)
        {
            connection.Open();
            var command = new SqlCommand(addString);
            
            var titleParam = new SqlParameter("@UserId", userRole.UserId);
            var contactInfoParam = new SqlParameter("@RoleId", userRole.RoleId);
            var idParameter = new SqlParameter
            {
                ParameterName = "@id",
                Direction = System.Data.ParameterDirection.Output,
                DbType = System.Data.DbType.Int32,
            };

            command.Parameters.Add(idParameter);
            command.Parameters.Add(titleParam);
            command.Parameters.Add(contactInfoParam);

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

            userRole.Id = id;

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

        public List<UserRoleEntity> Read()
        {
            var command = new SqlCommand(readString);

            command.Connection = connection;

            connection.Open();
            List<UserRoleEntity> result = new List<UserRoleEntity>();
            try
            {
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    object idFromDb = reader["Id"];
                    object userIdFromDb = reader["UserId"];
                    object roleIdFromDb = reader["RoleId"];
                    UserRoleEntity userRole = new UserRoleEntity
                    {
                        Id = System.Convert.ToInt32(idFromDb),
                        UserId = System.Convert.ToInt32(userIdFromDb),
                        RoleId = System.Convert.ToInt32(roleIdFromDb)
                    };
                    result.Add(userRole);
                }
            }
            finally
            {
                connection.Close();
            }
            return result;
        }

        public UserRoleEntity ReadById(int id)
        {
            var command = new SqlCommand(readByIdString);

            command.Connection = connection;

            var idParam = new SqlParameter("@id", id);

            command.Parameters.Add(idParam);

            connection.Open();
            List<UserRoleEntity> result = new List<UserRoleEntity>();
            try
            {
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    object idFromDb = reader["Id"];
                    object userIdFromDb = reader["UserId"];
                    object roleIdFromDb = reader["RoleId"];
                    UserRoleEntity userRole = new UserRoleEntity
                    {
                        Id = System.Convert.ToInt32(idFromDb),
                        UserId = System.Convert.ToInt32(userIdFromDb),
                        RoleId = System.Convert.ToInt32(roleIdFromDb)
                    };
                    result.Add(userRole);
                }
            }
            finally
            {
                connection.Close();
            }
            return result[0];
        }

        public void Update(UserRoleEntity userRole)
        {
            connection.Open();
            
            var command = new SqlCommand(updateString);

            var idParam = new SqlParameter("@id", userRole.Id);
            var titleParam = new SqlParameter("@UserId", userRole.UserId);
            var contactInfoParam = new SqlParameter("@RoleId", userRole.RoleId);

            command.Parameters.Add(titleParam);
            command.Parameters.Add(contactInfoParam);
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
