using DL.Entities;
using DL.Extensions;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using DL.Repositories.Abstract;

namespace DL.Repositories
{
    public class WorkerEntityRepo : Repository,  IWorkerEntityRepo
    {
        private string addString = "INSERT INTO Workers (PassportNumber, PersonalData) values (@passport_n, @passport_d);";
        
        private string deleteString = "Delete from Workers where PassportNumber=@passportNumber;";
        
        private string readString = "select * from Workers;";

        private string readByPassportNumberString = "select * from Workers where PassportNumber = @passportNumber;";
        
        private string updateString = "update Workers set PersonalData = @passport_d where PassportNumber=@passportNumber;";

        public WorkerEntityRepo(string connectionString) :
            base(connectionString)
        {
        }
        
        public int Create(WorkerEntity worker)
        {
            connection.Open();
            var command = new SqlCommand(addString);

            var numberParam = new SqlParameter("@passport_n", worker.PassportNumber);
            
            var dataParam = new SqlParameter("@passport_d", worker.PersonalData);
            
            command.Parameters.Add(numberParam);
            
            command.Parameters.Add(dataParam);

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

            worker.PassportNumber = id;

            return id;
        }

        public void Delete(int passportNumber)
        {
            connection.Open();
            var command = new SqlCommand(deleteString);
            var parameter = new SqlParameter("@passportNumber", passportNumber);
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

        public List<WorkerEntity> Read()
        {
            var command= new SqlCommand(readString);
            
            command.Connection = connection;
            
            connection.Open();
            
            List<WorkerEntity> result = new List<WorkerEntity>();

            try
            {
                var reader = command.ExecuteReader();
                
                while (reader.Read())
                {
                    object passportNumber = reader["PassportNumber"];
                    
                    object personalDataFromDb = reader["PersonalData"];
                    
                    WorkerEntity worker = new WorkerEntity
                    {
                        PassportNumber = System.Convert.ToInt32(passportNumber),
                        PersonalData = System.Convert.ToString(personalDataFromDb),
                    };
                    result.Add(worker);
                }
            }
            finally
            {
                connection.Close();
            }
            return result;
        }

        public WorkerEntity ReadById(int passportNumber)
        {
            var command = new SqlCommand(readByPassportNumberString);

            command.Connection = connection;

            var numberParam = new SqlParameter("@passportNumber", passportNumber);

            command.Parameters.Add(numberParam);

            connection.Open();

            List<WorkerEntity> result = new List<WorkerEntity>();

            try
            {
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    object personalDataFromDb = reader["PersonalData"];

                    WorkerEntity worker = new WorkerEntity
                    {
                        PassportNumber = passportNumber,
                        PersonalData = System.Convert.ToString(personalDataFromDb),
                    };
                    result.Add(worker);
                }
            }
            finally
            {
                connection.Close();
            }
            return result[0];
        }

        public void Update(WorkerEntity worker)
        {
            connection.Open();
            
            var command = new SqlCommand(updateString);

            var dataParametr = new SqlParameter("@passport_d", worker.PersonalData);

            var idParametr = new SqlParameter("@passportNumber", worker.PassportNumber);

            command.Parameters.Add(dataParametr);

            command.Parameters.Add(idParametr);

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
