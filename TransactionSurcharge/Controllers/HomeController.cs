using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TransactionSurcharge.Controllers
{
    public class HomeController : Controller
    {
        private Surchage control;
        private List<CustomerModel> customer;

        public HomeController()
        {
            control = new Surchage();
            customer = new List<CustomerModel>();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(int value)
        {
            if (ModelState.IsValid)
            {
                var result = control.FileReader(value);
                customer.Add(result);
                if (result==null)
                {
                    ViewBag.Error = "The input value for payment is incorrect please correct the values and try again.";

                }
                return View("IndexPost", customer);
            }
            return View();

        }



    }
}