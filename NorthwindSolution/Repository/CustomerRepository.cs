using NorthwindSolution.Repository.Interfaces;
using NothwindDAL;
using NothwindDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;
using NorthwindSolution.Models;

namespace NorthwindSolution.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        CustomerDAL customerDAL = null;

        public int PageCountAsync { get; set; }

        public CustomerRepository()
        {
            customerDAL = new CustomerDAL();
        }

        public Customer CustomerMapper(CustomerModel customerModel)
        {
            try
            {
                return new Customer()
                {
                    CustomerID = customerModel.CustomerID,
                    CompanyName = customerModel.CompanyName,
                    ContactTitle = customerModel.ContactTitle,
                    ContactName = customerModel.ContactName,
                    City = customerModel.City,
                    Country = customerModel.Country,
                    Region = customerModel.Region,
                    Address = customerModel.Address,
                    Fax = customerModel.Fax,
                    Phone = customerModel.Phone,
                    PostalCode = customerModel.PostalCode
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public CustomerModel CustomerMapper(Customer customer)
        {
            try
            {
                return new CustomerModel()
                {
                    CustomerID = customer.CustomerID,
                    CompanyName = customer.CompanyName,
                    ContactTitle = customer.ContactTitle,
                    ContactName = customer.ContactName,
                    City = customer.City,
                    Country = customer.Country,
                    Region = customer.Region,
                    Address = customer.Address,
                    Fax = customer.Fax,
                    Phone = customer.Phone,
                    PostalCode = customer.PostalCode
                };
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<Customer>> GetCustomersAsync()
        {
            try
            {
                return await Task.Run(() =>
                {
                    return customerDAL.GetCustomers();
                }).ConfigureAwait(false);
            }
            catch (Exception)
            {

                throw;

            }
                    
        }

        public List<Customer> GetCustomers()
        {
            try
            {
                return customerDAL.GetCustomers();
            }
            catch (Exception)
            {

                throw;

            }
        }

        public async Task<List<Customer>> GetCustomersAsync(int pageSize,int pageNo=1)
        {
            int n;
            try
            {
                return await Task.Run(() =>
                {
                    List<Customer> customers = customerDAL.GetCustomers(pageSize, out n, pageNo);
                    PageCountAsync = n;
                    return customers;
                }).ConfigureAwait(false);
            }
            catch (Exception)
            {

                throw;
            }

        }

        public List<Customer> GetCustomers(int pageSize,out int pageCount,int pageNo=1)
        {
            try
            {
                return customerDAL.GetCustomers(pageSize, out pageCount, pageNo);
            }
            catch (Exception)
            {

                throw;

            }
        }

        public async Task<Customer> GetCustomerDetailsAsync(string customerId)
        {
            try
            {
                return await Task.Run(() =>
                {
                    return customerDAL.GetCustomerDetails(customerId);
                }).ConfigureAwait(false);
            }
            catch (Exception)
            {

                throw;

            }
        }

        public Customer GetCustomerDetails(string customerId)
        {
            try
            {
                return customerDAL.GetCustomerDetails(customerId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> DeleteCustomerAsync(string customerId)
        {
            try
            {
                return await Task.Run(() =>
               {
                   return customerDAL.DeleteCustomer(customerId);
               }).ConfigureAwait(false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool DeleteCustomer(string customerId)
        {
            try
            {
                return customerDAL.DeleteCustomer(customerId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> UpdateCustomerAsync(CustomerModel customerModel)
        {
            try
            {
                return await Task.Run(() =>
                {
                    return customerDAL.UpdateCustomer(CustomerMapper(customerModel));
                }).ConfigureAwait(false);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool UpdateCustomer(CustomerModel customerModel)
        {
            try
            {
                return customerDAL.UpdateCustomer(CustomerMapper(customerModel));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}