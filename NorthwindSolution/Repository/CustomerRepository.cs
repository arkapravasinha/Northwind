using NorthwindSolution.Repository.Interfaces;
using NothwindDAL;
using NothwindDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;

namespace NorthwindSolution.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        CustomerDAL customerDAL = null;

        public CustomerRepository()
        {
            customerDAL = new CustomerDAL();
        }
        public async Task<List<Customer>> GetCustomersAsync()
        {
            return await Task.Run(()=>
            {
                return customerDAL.GetCustomers();
            });         
        }
    }
}