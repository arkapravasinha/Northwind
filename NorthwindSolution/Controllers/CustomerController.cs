using AutoMapper;
using NorthwindSolution.Models;
using NorthwindSolution.Repository;
using NorthwindSolution.Repository.Interfaces;
using NothwindDAL;
using NothwindDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace NorthwindSolution.Controllers
{
    [RoutePrefix("api/Customer")]
    public class CustomerController : ApiController
    {
        ICustomerRepository _customerRepository;
        IMapper _mapper;
        public CustomerController(ICustomerRepository customerRepository,IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }
        
        [Route("GetCustomers")]
        [HttpGet]
        public async Task<IHttpActionResult> GetCustomerss()
        {
            try
            {
                List<Customer> customers = await _customerRepository.GetCustomersAsync().ConfigureAwait(false);

                //Custome Build
                //var query = from c in customers
                //            select _customerRepository.CustomerMapper(c);

                if((customers !=null )&&( customers.Count!=0))
                {
                    //Using Auto Mapper
                    var results = _mapper.Map<IEnumerable<CustomerModel>>(customers);
                    return Ok(results);
                }
                else
                {
                    return NotFound();
                }                
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }

       
    }
}