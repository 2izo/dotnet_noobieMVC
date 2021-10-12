using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NorthwindMvc.Models;
using Packt.Shared;

namespace NorthwindMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private Northwind db;
        public HomeController(ILogger<HomeController> logger, Northwind inj)
        {
            _logger = logger;
            db = inj;
        }

        public IActionResult Index()
        {
            var model = new HomeIndexViewModel{
                VisitorCount = (new Random()).Next(1, 1001),
                Categories = db.Categories.ToList(),
                Products = db.Products.ToList()
                };
            return View(model);
        }
        [Route("privacy")]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult ModelBinding()
        {
        return View(); // the page with a form to submit
        }

        [HttpPost]
        public IActionResult ModelBinding(Thing Thing)
        {
        // return View(thing); // show the model bound thing

        
        return View(Thing);
        }
        public IActionResult ProductDetail(long? id)
        {
            if(!id.HasValue)
            {
                return NotFound("Not valid Id");
            }
            var product = db.Products.SingleOrDefault(x=>x.ProductID==id);
            if(product==null)
            {
                return NotFound("Not valid Id");
            }
            return View(product);
        }
    }
}
