using NothwindDAL.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NothwindDAL
{
    public class CustomerDAL
    {
        public List<Customer> GetCustomers()
        {
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;
            string query = "SELECT * FROM Customers";
            List <Customer> customerList= null;
            try
            {
                sqlConnection = new SqlConnection();
                DataLoader.LoadConnection(sqlConnection);
                DataLoader.OpenConnection(sqlConnection);
                sqlCommand = DataLoader.CreateSQLCommand(sqlConnection, System.Data.CommandType.Text, query);
                customerList = new List<Customer>();
                if (sqlCommand!=null)
                {
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while(sqlDataReader.Read())
                    {
                        Customer customer = new Customer()
                        {
                            CustomerID = sqlDataReader.GetString(0),
                            CompanyName = sqlDataReader.IsDBNull(1) ? null : sqlDataReader.GetString(1),
                            ContactName = sqlDataReader.IsDBNull(2) ? null : sqlDataReader.GetString(2),
                            ContactTitle = sqlDataReader.IsDBNull(3) ? null : sqlDataReader.GetString(3),
                            Address = sqlDataReader.IsDBNull(4) ? null : sqlDataReader.GetString(4),
                            City = sqlDataReader.IsDBNull(5) ? null : sqlDataReader.GetString(5),
                            Region = sqlDataReader.IsDBNull(6)?null:sqlDataReader.GetString(6),
                            PostalCode = sqlDataReader.IsDBNull(7) ? null : sqlDataReader.GetString(7),
                            Country = sqlDataReader.IsDBNull(8) ? null : sqlDataReader.GetString(8),
                            Phone = sqlDataReader.IsDBNull(9) ? null : sqlDataReader.GetString(9),
                            Fax = sqlDataReader.IsDBNull(10) ? null : sqlDataReader.GetString(10)                            
                        };
                        customerList.Add(customer);
                    }
                }
                else 
                {
                    throw new Exception("Unable to Create SQL Command");
                }
            }
            catch (Exception)
            {

                return null;
            }
            finally
            {
                DataLoader.CloseConnection(sqlConnection);
                DataLoader.DisposeSQLObj(sqlCommand);
            }
            return customerList;
        }

        public List<Customer> GetCustomers(int pageSize,out int totalCount, int pageNo = 1)
        {
            totalCount = 0;
            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;
            SqlCommand sqlCommand1 = null;
            string query = "SELECT * FROM Customers ORDER BY CustomerID OFFSET ((@PageNo-1)*@PageSize) ROWS FETCH NEXT @PageSize ROWS ONLY";
            string query1 = "SELECT COUNT(1) FROM Customers";
            List<Customer> customerList = null;
            try
            {
                sqlConnection = new SqlConnection();
                DataLoader.LoadConnection(sqlConnection);
                DataLoader.OpenConnection(sqlConnection);
                List<SqlParameter> sqlParameters = new List<SqlParameter>();
                sqlParameters.Add(new SqlParameter() { ParameterName="@PageNo",Value=pageNo,DbType=System.Data.DbType.Int32});
                sqlParameters.Add(new SqlParameter() { ParameterName="@PageSize",Value=pageSize,DbType=System.Data.DbType.Int32});
                sqlCommand = DataLoader.CreateSQLCommand(sqlConnection, System.Data.CommandType.Text, query,sqlParameters);
                customerList = new List<Customer>();
                if (sqlCommand != null)
                {
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Customer customer = new Customer()
                        {
                            CustomerID = sqlDataReader.GetString(0),
                            CompanyName = sqlDataReader.IsDBNull(1) ? null : sqlDataReader.GetString(1),
                            ContactName = sqlDataReader.IsDBNull(2) ? null : sqlDataReader.GetString(2),
                            ContactTitle = sqlDataReader.IsDBNull(3) ? null : sqlDataReader.GetString(3),
                            Address = sqlDataReader.IsDBNull(4) ? null : sqlDataReader.GetString(4),
                            City = sqlDataReader.IsDBNull(5) ? null : sqlDataReader.GetString(5),
                            Region = sqlDataReader.IsDBNull(6) ? null : sqlDataReader.GetString(6),
                            PostalCode = sqlDataReader.IsDBNull(7) ? null : sqlDataReader.GetString(7),
                            Country = sqlDataReader.IsDBNull(8) ? null : sqlDataReader.GetString(8),
                            Phone = sqlDataReader.IsDBNull(9) ? null : sqlDataReader.GetString(9),
                            Fax = sqlDataReader.IsDBNull(10) ? null : sqlDataReader.GetString(10)
                        };
                        customerList.Add(customer);
                    }
                    if(!sqlDataReader.IsClosed)
                    {
                        sqlDataReader.Close();
                    }
                }
                else
                {
                    throw new Exception("Unable to Create SQL Command");
                }
                sqlCommand1 = DataLoader.CreateSQLCommand(sqlConnection, System.Data.CommandType.Text, query1);
                if (sqlCommand1 != null)
                {
                    int rowCount = Convert.ToInt32(sqlCommand1.ExecuteScalar());
                    if((rowCount%pageSize)==0)
                    {
                        totalCount = rowCount / pageSize;
                    }
                    else 
                    {
                        totalCount = rowCount / pageSize + 1;
                    }
                    
                }
                else
                {
                    throw new Exception("Unable to Create SQL Command");
                }
                

            }
            catch (Exception)
            {

                return null;
            }
            finally
            {
                DataLoader.CloseConnection(sqlConnection);
                DataLoader.DisposeSQLObj(sqlCommand);
            }
            return customerList;
        }
    }
}
