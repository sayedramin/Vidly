using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security.Provider;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Customers
        public ActionResult All()
        {
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();
            return View(customers);
        }

        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c != null && c.Id == id);

            return View(customer);
        }


        public ActionResult New()
        {
            var membershipType = _context.MembershipTypes.ToList();

            var viewModel = new CustomerFormViewModel
            {
                // MembershipTypes = _context.MembershipTypes.ToList()
                MembershipTypes = membershipType
            };

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(CustomerFormViewModel ViewModel)
        {
            if (ViewModel.Customers.Id == 0)
                _context.Customers.Add(ViewModel.Customers);
            else
            {
                var customerInDb = _context.Customers.SingleOrDefault(c => c != null && c.Id == ViewModel.Customers.Id);
                
                //TryUpdateModel(customerInDb);  All columns are editable which leave security gap
                //Mapper.Map(ViewModel, customerInDb); Auto mapper is another way of mapping. you have to create object with clomns you want to map in database first.
                    customerInDb.Name = ViewModel.Customers.Name;
                    customerInDb.BirthDate = ViewModel.Customers.BirthDate;
                    customerInDb.MembershipTypeId = ViewModel.Customers.MembershipTypeId;
                    customerInDb.IsSubscribedToNewsLetter = ViewModel.Customers.IsSubscribedToNewsLetter;
            }

            _context.SaveChanges();

            return RedirectToAction("All", "Customers");
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c != null && c.Id == id);
            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customers = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };
            return View("CustomerForm",viewModel);
        }
    }
}