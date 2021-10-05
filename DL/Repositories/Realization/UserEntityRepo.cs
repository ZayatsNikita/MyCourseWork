﻿using DL.Entities;
using System;
using DL.Extensions;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace DL.Repositories
{
    public class UserEntityRepo : Abstract.Repository, Abstract.IUserEntityRepo
    {
        private string addString = "INSERT INTO Users (Login, Password, WorkerId) values (@login, @password, @worker_id);SELECT LAST_INSERT_ID();";
        private string deleteString = "Delete from Users ";
        private string readString = "select * from Users ";
        private string updateString = "update Users ";
        public UserEntityRepo(string connectionString): base(connectionString) { ; }
        public UserEntity Create(UserEntity user)
        {
            connection.Open();
            var command = new SqlCommand(addString);
            var loginParam = new SqlParameter("@login", user.Login);
            var passwordParam = new SqlParameter("@password", user.Password);
            var workerParam = new SqlParameter("@worker_id", user.WorkerId);

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
            int id = Convert.ToInt32(obj);
            user.Id = id;
            return user;
        }
        public void Delete(UserEntity user, int workerId = DefValInt)
        {
            connection.Open();

            string whereForComand = CreateWherePartForDeleteQuery(user.Id, workerId);
     
            var command = new SqlCommand(deleteString + whereForComand) { Connection = connection };
            
            try
            {
                int delCount = command.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
        }
        public List<UserEntity> Read(int MinId = DefValInt, int MaxId = DefValInt, string login = null, string password = null, int workerId = DefValInt)
        {
            string stringWithWhere = CreateWherePartForReadQuery(MinId, MaxId, login, password, workerId);
            
            var command = new SqlCommand(readString + stringWithWhere);

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
        public void Update(UserEntity user, string login = null, string password = null, int workerId = -1)
        {
            connection.Open();

            string setString = CreateSetPartForUpdateQuery(login, password, workerId);

            var command = new SqlCommand(updateString + setString + $" where id = {user.Id};");

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
        private string CreateWherePartForReadQuery(int MinId, int MaxId, string login, string password, int workerId)
        {
            if (MinId != DefValInt || MaxId != DefValInt || login != null || password != null || workerId!= DefValInt)
            {
                StringBuilder query = new StringBuilder();

                query.AddWhereWord();

                query.AddWhereParam(MinId, MaxId,"id");

                query.AddWhereParam(workerId, workerId, "workerId");

                query.AddWhereParam(login, "login");
                
                query.AddWhereParam(password, "password");

                return query.ToString();
            }
            else
            {
                return null;
            }
        }
        private string CreateSetPartForUpdateQuery(string login, string password, int workerId)
        {
            if(login==null && password == null)
            {
                return null;
            }
            else
            {
                StringBuilder query = new StringBuilder();
                
                query.AddSetWord();
                
                query.AddSetParam(login,"login");
                
                query.AddSetParam(password, "password");
                
                query.AddSetParam(workerId, "workerId");
                
                return query.ToString();
            }

        }
        private string CreateWherePartForDeleteQuery(int id, int workerId)
        {
            StringBuilder builder = new StringBuilder();
            builder.AddWhereWord();
            if (workerId == DefValInt)
            {
                builder.Append(" id = " + id.ToString());
            }
            else
            {
                builder.Append($" workerId = {workerId.ToString()}");   
            }
            return builder.ToString();
        }
    }
}
