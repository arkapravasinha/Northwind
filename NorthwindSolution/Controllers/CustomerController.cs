﻿using NorthwindSolution.Repository;
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
    public class CustomerController : ApiController
    {
        ICustomerRepository _customerRepository;
        public CustomerController()
        {
            _customerRepository = new CustomerRepository();
        }
        // GET api/<controller>
        public async Task<IHttpActionResult> Get()
        {

            List<Customer> customers = await _customerRepository.GetCustomersAsync();
            return Ok(customers);
        }

        //public IHttpActionResult Get()
        //{
        //    return Ok(new CustomerDAL().GetCustomers());
        //}

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}