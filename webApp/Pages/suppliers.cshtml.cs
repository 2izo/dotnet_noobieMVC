using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Packt.Shared;

namespace webApp.Pages
{
    public class SuppliersModel:PageModel
    {

        public IEnumerable<string> Suppliers{set;get;}
        
        private Northwind db;
        public SuppliersModel(Northwind injectedContext)
        {
        db = injectedContext;
        }
        public void OnGet()
        {
            Suppliers = db.Suppliers.Select(s=>s.CompanyName);
        }
        [BindProperty]
        public Supplier Supplier{set;get;}
        public IActionResult OnPost()
        {
            if(ModelState.IsValid)
            {
                db.Suppliers.Add(Supplier);
                db.SaveChanges();
                return RedirectToPage("/suppliers");
            }
            return Page();
        }
    }
}