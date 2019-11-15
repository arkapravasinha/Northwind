using AutoMapper;
using NorthwindSolution.Models;
using NothwindDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NorthwindSolution.Repository.MappingProfile
{
    public class CustomerMappingProfile:Profile
    {
        public CustomerMappingProfile()
        {
            CreateMap<Customer, CustomerModel>().ForMember(c => c.CustomerID, opt => opt.MapFrom(c => c.CustomerID));
        }
    }
}