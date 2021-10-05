using DL.Entities;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using DL.Extensions;

namespace DL.Repositories
{
    public class UserRoleRepository : Abstract.Repository,  Abstract.IUserRoleRepository
    {
        private string addString = "INSERT INTO UserRoles (UserId, RoleId) values (@UserId, @RoleId);SELECT LAST_INSERT_ID();";
        private string deleteString = "Delete from UserRoles where id=@id";
        private string readString = "select * from UserRoles ";
        private string updateString = "update UserRoles ";
        public UserRoleRepository(string connectionString) : base(connectionString) {; }
        public void Create(UserRoleEntity userRole)
        {
            connection.Open();
            var command = new SqlCommand(addString);
            var titleParam = new SqlParameter("@UserId", userRole.UserId);
            var contactInfoParam = new SqlParameter("@RoleId", userRole.RoleId);
            
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
            int id = Convert.ToInt32(obj);
            userRole.Id = id;
        }

        public void Delete(UserRoleEntity userRole)
        {
            connection.Open();
            var command = new SqlCommand(deleteString);
            var parameter = new SqlParameter("@id", userRole.Id.ToString());
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
        public List<UserRoleEntity> Read(int minId= DefValInt, int maxId = DefValInt, int minUserId = DefValInt, int maxUserId = DefValInt, int minRoleId = DefValInt, int maxRoleId = DefValInt)
        {
            string stringWithWhere = CreateWherePartForReadQuery(minId, maxId, minUserId, maxUserId, minRoleId, maxRoleId);
            var command = new SqlCommand(readString + stringWithWhere);
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

        public void Update(UserRoleEntity userRole, int userId = DefValInt, int roleId = DefValInt)
        {
            connection.Open();
            
            string setString = CreateSetPartForUpdateQuery(userId, roleId);
            
            var command = new SqlCommand(updateString + setString + $" where id = {userRole.Id};");

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
        private string CreateWherePartForReadQuery(int minId, int maxId, int minUserId, int maxUserId, int minRoleId, int maxRoleId)
        {
            if(minId!= DefValInt || maxId!= DefValInt || minUserId != DefValInt || maxUserId != DefValInt || minRoleId != DefValInt || maxRoleId != DefValInt)
            {
                StringBuilder query = new StringBuilder();
                query.AddWhereWord();

                query.AddWhereParam(minId, maxId, "id");

                query.AddWhereParam(minUserId, maxUserId, "UserId");

                query.AddWhereParam(minRoleId, maxRoleId, "RoleId");

                return query.ToString();
            }
            else
            {
                return null;
            }
        }
        private string CreateSetPartForUpdateQuery(int userId, int roleId)
        {
            if(userId== DefValInt && roleId == DefValInt)
            {
                return null;
            }
            else
            {
                StringBuilder query = new StringBuilder();
                
                query.AddSetWord();
                
                query.AddSetParam(userId, "userId");

                query.AddSetParam(roleId, "roleId");
                
                return query.ToString();
            }
        }
    }
}
