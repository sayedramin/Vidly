using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using AutoMapper;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.App_Start
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            // Domain to Dto
            Mapper.CreateMap<Customers, CustomerDto>();
            Mapper.CreateMap<Movies, MovieDto>();

            // Dto to Domain
            Mapper.CreateMap<CustomerDto, Customers>()
                .ForMember(c => c.Id, opt => opt.Ignore());

            Mapper.CreateMap<MovieDto, Movies>()
                .ForMember(c => c.Id, opt => opt.Ignore());

        }
       
    }
}