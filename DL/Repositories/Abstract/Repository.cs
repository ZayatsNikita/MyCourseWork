using System.Data.SqlClient;

namespace DL.Repositories.Abstract
{
    public abstract class Repository
    {
        public Repository(string connectionString)
        {
            this.connection = new SqlConnection(connectionString);
        } 
        
        public const int DefValInt = 0;
        
        public const decimal DefValDec = 0;

        protected SqlConnection connection;
    }
}
