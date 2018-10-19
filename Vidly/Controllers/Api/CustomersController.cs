using System;
using AutoMapper;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Glimpse.Mvc.AlternateType;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNetCore.Mvc;
using Vidly.Dtos;
using Vidly.Models;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;

namespace Vidly.Controllers.Api
{

    [Authorize(Roles = RoleName.CanManageCustomers)] // Only Authorized users can use API no GUEST ACCOUNT


    [Produces("application/json")]
    [System.Web.Http.Route("api/Customers")]
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }


        public ActionResult testelmah()
        {

            return testelmah();
        }

        // GET /api/customers
        [AllowAnonymous]
        [System.Web.Http.HttpGet]
        [System.Web.Http.Route("api/Customers")]
        public IHttpActionResult GetCustomers()
        {
            var customerDto = _context.Customers
                .Include(c => c.MembershipType)
                .ToList()
                .Select(Mapper.Map<Customers, CustomerDto>);

            return Ok(customerDto);
        }

        // GET /api/customers/1
        [System.Web.Mvc.HttpGet]
        [System.Web.Http.Route("api/Customers/{id}")]
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return NotFound();
            return Ok(Mapper.Map<Customers, CustomerDto>(customer));
        }

        // POST /ap/customers
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("api/Customers")]
        public IHttpActionResult CreateCustomer([System.Web.Http.FromBody] CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                //throw new HttpResponseException(HttpStatusCode.BadRequest);
                return BadRequest();

            var customer = Mapper.Map<CustomerDto, Customers>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;
            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto );
        }

        // PUT /api/customers/1
        [System.Web.Http.HttpPut]
        [System.Web.Http.Route("api/Customers/{id}")]
        public IHttpActionResult UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            
            var customerInDb = _context.Customers.SingleOrDefault(c => c != null && c.Id == id);

            if (customerInDb == null)
                return NotFound();

            Mapper.Map(customerDto, customerInDb);

            _context.SaveChanges();

            return Ok();
        }

        // DELETE /api/customer/1
        [System.Web.Http.HttpDelete]
        [System.Web.Http.Route("api/Customers/{id}")]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c != null && c.Id == id);

            if (customerInDb == null)
                return NotFound();

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();

            return Ok();
        }

    }

}
