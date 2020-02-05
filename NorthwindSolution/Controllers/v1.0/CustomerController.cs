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
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
   [Authorize]
    public class CustomerController : ApiController
    {
        ICustomerRepository _customerRepository;
        IMapper _mapper;
        public CustomerController(ICustomerRepository customerRepository,IMapper mapper)
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
      //  [MapToApiVersion("1.0")]
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

        [Route("GetCustomers")]
        [HttpGet]
        [MapToApiVersion("1.2")]
        public async Task<IHttpActionResult> GetCustomers()
        {
            try
            {
                List<Customer> customers = await _customerRepository.GetCustomersAsync().ConfigureAwait(false);

                //Custome Build
                //var query = from c in customers
                //            select _customerRepository.CustomerMapper(c);

                if ((customers != null) && (customers.Count != 0))
                {
                    //Using Auto Mapper
                    var results = _mapper.Map<IEnumerable<CustomerModel>>(customers);
                    return Ok( new { success=true,time=DateTime.UtcNow,data=results});
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