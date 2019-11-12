using NorthwindSolution.Models;
using NothwindDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindSolution.Repository.Interfaces
{
    public interface ICustomerRepository
    {
        int PageCountAsync { get; set; } 

        Customer CustomerMapper(CustomerModel customerModel);

        CustomerModel CustomerMapper(Customer customer);

        Task<List<Customer>> GetCustomersAsync();

        List<Customer> GetCustomers();

        Task<List<Customer>> GetCustomersAsync(int pageSize, int pageNo = 1);

        List<Customer> GetCustomers(int pageSize, out int pageCount, int pageNo = 1);

        Task<Customer> GetCustomerDetailsAsync(string customerId);

        Customer GetCustomerDetails(string customerId);

        Task<bool> DeleteCustomerAsync(string customerId);

        bool DeleteCustomer(string customerId);

        Task<bool> UpdateCustomerAsync(CustomerModel customerModel);

        bool UpdateCustomer(CustomerModel customerModel);
    }
}
