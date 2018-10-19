using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Caching;
using System.Web.Mvc;
using System.Web.UI;
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
        [OutputCache(Duration = 0,Location = OutputCacheLocation.Client, VaryByParam = "*", NoStore = true)] // it cache html not the date but duration duration zero means don't cache 
        // GET: Customers
        public ActionResult Index()
        {
            // Remember caching is using lots of your memory and may result memory performance while solving network call performance.
            // Don't use blindly any concept unless you really have to use it. first check Glimpse and find the performance issue than use optimization
//            if (MemoryCache.Default["Genres"] == null)
//            {
//                MemoryCache.Default["Genres"] = _context.MovieGenres.ToList();
//            }
//
//            var genres = MemoryCache.Default["Genres"] as IEnumerable<MovieGenre>;


            //var customers = _context.Customers.Include(c => c.MembershipType).ToList();
            //return View(customers);
            return View(User.IsInRole(RoleName.CanManageCustomers) ? "List" : "ReadOnlyList");
        }

        [Authorize(Roles = RoleName.CanManageCustomers)]
        public ActionResult New()
        {
            var membershipType = _context.MembershipTypes.ToList();

            var viewModel = new CustomerFormViewModel
            {
                Customers = new Customers(),
                // MembershipTypes = _context.MembershipTypes.ToList()
                MembershipTypes = membershipType
            };

            return View("CustomerForm", viewModel);
        }

        [Authorize(Roles = RoleName.CanManageCustomers)]
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
            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(CustomerFormViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.MembershipTypes = _context.MembershipTypes.ToList();
                return View("CustomerForm", viewModel);
            }

            if (viewModel.Customers.Id == 0)
                _context.Customers.Add(viewModel.Customers);
            else
            {
                var customerInDb = _context.Customers.SingleOrDefault(c => c != null && c.Id == viewModel.Customers.Id);

                //TryUpdateModel(customerInDb);  All columns are editable which leave security gap
                //Mapper.Map(ViewModel, customerInDb); Auto mapper is another way of mapping. you have to create object with clomns you want to map in database first.
                customerInDb.Name = viewModel.Customers.Name;
                customerInDb.BirthDate = viewModel.Customers.BirthDate;
                customerInDb.MembershipTypeId = viewModel.Customers.MembershipTypeId;
                customerInDb.IsSubscribedToNewsLetter = viewModel.Customers.IsSubscribedToNewsLetter;

                _context.Entry(customerInDb).State = EntityState.Modified;

            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }
    }
}