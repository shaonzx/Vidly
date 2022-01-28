using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
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

        // GET: Customer
        public ActionResult Index()
        {
            //var customers = GetCustomers();
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();

            return View(customers);
        }

        //[Route("Customers/{id}")]
        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c => c.MembershipType).FirstOrDefault(x => x.Id == id);

            return View(customer);
        }

        private IEnumerable<Customer> GetCustomers()
        {
            var customers = new List<Customer>
            {
                new Customer { Id = 1, Name = "John Smith"},
                new Customer { Id = 2, Name = "Marry Williams"}
            };

            return customers;
        }

        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();

            var viewModel = new CustomerFormViewModel
            {
                MembershipTypes = membershipTypes,
                Customer = new Customer()
            };

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        public ActionResult Save(CustomerFormViewModel vm)
        {
            if (vm.Customer.Id == 0)
            {
                _context.Customers.Add(vm.Customer);
            }
            else
            {
                var customerInDb = _context.Customers.Single(x => x.Id == vm.Customer.Id);
                customerInDb.Name = vm.Customer.Name;
                customerInDb.DateOfBirth = vm.Customer.DateOfBirth;
                customerInDb.MembershipTypeId = vm.Customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = vm.Customer.IsSubscribedToNewsletter;
            }

            _context.SaveChanges();


            return RedirectToAction("Index", "Customers");
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(x => x.Id == id);

            if (customer == null)
            {
                return HttpNotFound();
            }

            var vm = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", vm);
        }
    }
}