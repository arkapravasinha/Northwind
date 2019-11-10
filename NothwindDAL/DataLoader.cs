using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NothwindDAL
{
    static class DataLoader
    {
        public static void LoadConnection(SqlConnection sqlConnection)
        {
            try
            {
                sqlConnection.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"].ToString();
            }
            catch (Exception)
            {
                throw;   
            }
        }

        public static void OpenConnection(SqlConnection sqlConnection)
        {
            try
            {
                sqlConnection.Open();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void CloseConnection(SqlConnection sqlConnection)
        {
            try
            {
                if (sqlConnection.State!=ConnectionState.Closed)
                {
                    sqlConnection.Close();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static void DisposeSQLObj(IDisposable disposable)
        {
            try
            {
                disposable.Dispose();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static SqlCommand CreateSQLCommand(SqlConnection con,CommandType commandType,string commandText,List<SqlParameter> sqlParameters=null)
        {
            SqlCommand sqlCommand = null;
            try
            {
                sqlCommand = new SqlCommand();
                sqlCommand.Connection = con;
                sqlCommand.CommandType = commandType;
                sqlCommand.CommandText = commandText;
                if((sqlParameters != null)&&(sqlParameters.Count>0))
                {
                    foreach (SqlParameter item in sqlParameters)
                    {
                        sqlCommand.Parameters.Add(item);
                    }
                }
                
            }
            catch (Exception)
            {

                return null;
        
            }

            return sqlCommand;
        }
    }
}
