using DL.Entities;
using System;
using DL.Extensions;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using DL.Repositories.Abstract;

namespace DL.Repositories
{
    public class UserEntityRepo : Repository, IUserEntityRepo
    {
        private string addString = "INSERT INTO Users (Login, Password, WorkerId) values (@login, @password, @worker_id);SET @id=SCOPE_IDENTITY();";
        
        private string deleteString = "Delete from Users where id=@id;";
        
        private string readString = "select * from Users;";
        
        private string readByIdString = "select * from Users where id=@id;";
        
        private string updateString = "update Users set Login = @login, Password = @password, WorkerId = @worker_id where id = @id;";
        
        public UserEntityRepo(string connectionString):
            base(connectionString)
        {
        }

        public int Create(UserEntity user)
        {
            connection.Open();
            var command = new SqlCommand(addString);
            var loginParam = new SqlParameter("@login", user.Login);
            var passwordParam = new SqlParameter("@password", user.Password);
            var workerParam = new SqlParameter("@worker_id", user.WorkerId);
            var idParameter = new SqlParameter
            {
                ParameterName = "@id",
                Direction = System.Data.ParameterDirection.Output,
                DbType = System.Data.DbType.Int32,
            };

            command.Parameters.Add(idParameter);
            command.Parameters.Add(loginParam);
            command.Parameters.Add(passwordParam);
            command.Parameters.Add(workerParam);

            command.Connection = connection;

            object obj = null;
            try
            {
                obj = command.ExecuteScalar();
            }
            finally
            {
                connection.Close();
            }
            int id = Convert.ToInt32(idParameter.Value);
            user.Id = id;
            return id;
        }

        public void Delete(int id)
        {
            connection.Open();
     
            var command = new SqlCommand(deleteString) { Connection = connection };

            var idParam = new SqlParameter("@id", id);
            
            command.Parameters.Add(idParam);

            try
            {
                int delCount = command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }

        public List<UserEntity> Read()
        {            
            var command = new SqlCommand(readString);

            command.Connection = connection;

            connection.Open();
            
            List<UserEntity> result = new List<UserEntity>();

            try
            {
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    object id = reader["id"];
                    object passwordFromDb = reader["password"];
                    object loginInformation = reader["login"];
                    object workerIDInformation = reader["workerId"];

                    UserEntity user = new UserEntity
                    {
                        Id = System.Convert.ToInt32(id),
                        Password = System.Convert.ToString(passwordFromDb),
                        Login = System.Convert.ToString(loginInformation),
                        WorkerId = System.Convert.ToInt32(workerIDInformation)
                    };
                    result.Add(user);
                }
            }
            finally
            {
                connection.Close();
            }

            return result;
        }

        public UserEntity ReadById(int id)
        {
            var command = new SqlCommand(readByIdString);

            var idParam = new SqlParameter("@id", id);

            command.Connection = connection;

            command.Parameters.Add(idParam);

            connection.Open();

            List<UserEntity> result = new List<UserEntity>();

            try
            {
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    object passwordFromDb = reader["password"];
                    object loginInformation = reader["login"];
                    object workerIDInformation = reader["workerId"];

                    UserEntity user = new UserEntity
                    {
                        Id = id,
                        Password = System.Convert.ToString(passwordFromDb),
                        Login = System.Convert.ToString(loginInformation),
                        WorkerId = System.Convert.ToInt32(workerIDInformation)
                    };
                    result.Add(user);
                }
            }
            finally
            {
                connection.Close();
            }

            return result[0];
        }

        public void Update(UserEntity user)
        {
            connection.Open();

            
            var command = new SqlCommand(updateString);

            var idParam = new SqlParameter("@id", user.Id);
            var loginParam = new SqlParameter("@login", user.Login);
            var passwordParam = new SqlParameter("@password", user.Password);
            var workerParam = new SqlParameter("@worker_id", user.WorkerId);

            command.Parameters.Add(loginParam);
            command.Parameters.Add(passwordParam);
            command.Parameters.Add(workerParam);
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
