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

                throw;
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

                throw;
            }
            finally
            {
                DataLoader.CloseConnection(sqlConnection);
                DataLoader.DisposeSQLObj(sqlCommand);
                DataLoader.DisposeSQLObj(sqlCommand1);
            }
            return customerList;
        }

        public Customer GetCustomerDetails(string customerId)
        {
            
                
                SqlConnection sqlConnection = null;
                SqlCommand sqlCommand = null;
                
                string query = "SELECT * FROM Customers WHERE CustomerID=@CustomerID";
                Customer customer = null;
                try
                {
                    sqlConnection = new SqlConnection();
                    DataLoader.LoadConnection(sqlConnection);
                    DataLoader.OpenConnection(sqlConnection);
                    List<SqlParameter> sqlParameters = new List<SqlParameter>();
                    sqlParameters.Add(new SqlParameter() { ParameterName = "@CustomerID", Value = customerId, DbType = System.Data.DbType.String });
                    sqlCommand = DataLoader.CreateSQLCommand(sqlConnection, System.Data.CommandType.Text, query, sqlParameters);
                    customer = new Customer();
                    if (sqlCommand != null)
                    {
                        SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                        while (sqlDataReader.Read())
                        {
                        customer.CustomerID = sqlDataReader.GetString(0);
                        customer.CompanyName = sqlDataReader.IsDBNull(1) ? null : sqlDataReader.GetString(1);
                        customer.ContactName = sqlDataReader.IsDBNull(2) ? null : sqlDataReader.GetString(2);
                        customer.ContactTitle = sqlDataReader.IsDBNull(3) ? null : sqlDataReader.GetString(3);
                        customer.Address = sqlDataReader.IsDBNull(4) ? null : sqlDataReader.GetString(4);
                        customer.City = sqlDataReader.IsDBNull(5) ? null : sqlDataReader.GetString(5);
                        customer.Region = sqlDataReader.IsDBNull(6) ? null : sqlDataReader.GetString(6);
                        customer.PostalCode = sqlDataReader.IsDBNull(7) ? null : sqlDataReader.GetString(7);
                        customer.Country = sqlDataReader.IsDBNull(8) ? null : sqlDataReader.GetString(8);
                        customer.Phone = sqlDataReader.IsDBNull(9) ? null : sqlDataReader.GetString(9);
                        customer.Fax = sqlDataReader.IsDBNull(10) ? null : sqlDataReader.GetString(10);                            
                        }
                        if (!sqlDataReader.IsClosed)
                        {
                            sqlDataReader.Close();
                        }
                    }
                    else
                    {
                        throw new Exception("Unable to Create SQL Command");
                    }
                    
                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    DataLoader.CloseConnection(sqlConnection);
                    DataLoader.DisposeSQLObj(sqlCommand);
                }
                return customer;
            }

        public bool DeleteCustomer(string customerId)

        {
            bool flag = false;

            SqlConnection sqlConnection = null;
            SqlCommand sqlCommand = null;

            string query = "DELETE FROM Customers WHERE CustomerID=@CustomerID";
            try
            {
                sqlConnection = new SqlConnection();
                DataLoader.LoadConnection(sqlConnection);
                DataLoader.OpenConnection(sqlConnection);
                List<SqlParameter> sqlParameters = new List<SqlParameter>();
                sqlParameters.Add(new SqlParameter() { ParameterName = "@CustomerID", Value = customerId, DbType = System.Data.DbType.String });
                sqlCommand = DataLoader.CreateSQLCommand(sqlConnection, System.Data.CommandType.Text, query, sqlParameters);
                
                if (sqlCommand != null)
                {
                    int n = sqlCommand.ExecuteNonQuery();
                    
                    if (n>0)
                    {
                        flag = true;
                    }
                }
                else
                {
                    throw new Exception("Unable to Create SQL Command");
                }

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                DataLoader.CloseConnection(sqlConnection);
                DataLoader.DisposeSQLObj(sqlCommand);
            }
            return flag;
        }

        public bool UpdateCustomer(Customer customer)
        {
            bool flag = false;
            if ((!string.IsNullOrEmpty(customer.CustomerID))&&(!string.IsNullOrEmpty(customer.Fax)))
            {
                SqlConnection sqlConnection = null;
                SqlCommand sqlCommand = null;

                string query = "UPDATE Customers SET Fax=@FAX WHERE CustomerID=@CustomerID";
               
                try
                {
                    sqlConnection = new SqlConnection();
                    DataLoader.LoadConnection(sqlConnection);
                    DataLoader.OpenConnection(sqlConnection);
                    List<SqlParameter> sqlParameters = new List<SqlParameter>();
                    sqlParameters.Add(new SqlParameter() { ParameterName = "@CustomerID", Value = customer.CustomerID, DbType = System.Data.DbType.String });
                    sqlParameters.Add(new SqlParameter() { ParameterName = "@FAX", Value = customer.Fax, DbType = System.Data.DbType.String });
                    sqlCommand = DataLoader.CreateSQLCommand(sqlConnection, System.Data.CommandType.Text, query, sqlParameters);
                    
                    if (sqlCommand != null)
                    {
                        int n = sqlCommand.ExecuteNonQuery();
                       
                        if (n>0)
                        {
                            flag=true;
                        }
                    }
                    else
                    {
                        throw new Exception("Unable to Create SQL Command");
                    }

                }
                catch (Exception)
                {

                    throw;
                }
                finally
                {
                    DataLoader.CloseConnection(sqlConnection);
                    DataLoader.DisposeSQLObj(sqlCommand);
                }
            }

            return flag;
        }

        
    }
}
