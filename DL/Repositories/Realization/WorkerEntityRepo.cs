using DL.Entities;
using DL.Extensions;
using System.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
namespace DL.Repositories
{
    public class WorkerEntityRepo : Abstract.Repository,  Abstract.IWorkerEntityRepo
    {
        private string addString = "INSERT INTO Workers (PassportNumber, PersonalData) values (@passport_n, @passport_d);SELECT LAST_INSERT_ID();";
        private string deleteString = "Delete from Workers where PassportNumber=@passportNumber; ";
        private string readString = "select * from Workers ";
        private string updateString = "update Workers ";
        public WorkerEntityRepo(string connectionString) : base(connectionString) {; }
        public void Create(WorkerEntity worker)
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
        }

        public void Delete(WorkerEntity worker)
        {
            connection.Open();
            var command = new SqlCommand(deleteString);
            var parameter = new SqlParameter("@passportNumber", worker.PassportNumber.ToString());
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

        public List<WorkerEntity> Read(int minPassportNumber= DefValInt, int maxPassportNumber= DefValInt, string PersonalData=null)
        {
            string stringWithWhere = CreateWherePartForReadQuery(minPassportNumber, maxPassportNumber, PersonalData);

            var command= new SqlCommand(readString+ stringWithWhere);
            
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

        public void Update(WorkerEntity worker, string PersonalData = null)
        {
            connection.Open();
            
            string setString = CreateSetPartForUpdateQuery(PersonalData);
            
            var command = new SqlCommand(updateString + setString + $" where PassportNumber = {worker.PassportNumber};");

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

        private string CreateWherePartForReadQuery(int minPassportNumber , int maxPassportNumber , string personalData)
        {
            if(minPassportNumber!= DefValInt || maxPassportNumber!= DefValInt || personalData!=null)
            {
                StringBuilder query = new StringBuilder();

                query.AddWhereWord();

                query.AddWhereParam(minPassportNumber, maxPassportNumber, "PassportNumber");

                query.AddWhereParam(personalData, "PersonalData");

                return query.ToString();
            }
            else
            {
                return null;
            }
        }
        private string CreateSetPartForUpdateQuery(string personalData)
        {
            if(personalData==null)
            {
                return null;
            }
            else
            {
                StringBuilder where = new StringBuilder();
                where.AddSetWord();

                where.AddSetParam(personalData, "PersonalData");
                
                return where.ToString();
            }
        }
    }
}
