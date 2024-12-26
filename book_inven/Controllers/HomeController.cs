using book_inven.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace book_inven.Controllers
{
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }

        
        public IActionResult Books()
        {
            return RedirectToAction("Index", "Book");
        }

        
        public IActionResult Members()
        {
            return RedirectToAction("Index", "Member");
        }
        
    }

}
