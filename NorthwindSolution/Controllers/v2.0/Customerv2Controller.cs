using AutoMapper;
using Microsoft.Web.Http;
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
    [RoutePrefix("api/v{version:apiVersion}/Customer")]
    [ApiVersion("2.0")]
    public class Customerv2Controller : ApiController
    {
        ICustomerRepository _customerRepository;
        IMapper _mapper;
        public Customerv2Controller(ICustomerRepository customerRepository,IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }
        
        /// <summary>
        /// Get All Customers
        /// </summary>
        /// <returns>List of Customers</returns>
        [Route("GetCustomers")]
        [HttpGet]
      //[MapToApiVersion("2.0")] //we can put the specific version here as well
        public async Task<IHttpActionResult> GetCustomerss()
        {
            try
            {
                List<Customer> customers = await _customerRepository.GetCustomersAsync().ConfigureAwait(false);

                //Custom Build
                //var query = from c in customers
                //            select _customerRepository.CustomerMapper(c);

                if((customers !=null )&&( customers.Count!=0))
                {
                    //Using Auto Mapper
                    var results = _mapper.Map<IEnumerable<CustomerModel>>(customers);

                    
                    return Ok(new { success=true, data=results });
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