using NothwindDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NorthwindSolution.Repository.Interfaces
{
    interface ICustomerRepository
    {
        Task<List<Customer>> GetCustomersAsync();
    }
}
