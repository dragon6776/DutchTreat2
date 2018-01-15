using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DutchTreat2.Data;

namespace DutchTreat2.Controllers
{
    public class AppController : Controller
    {
        private readonly IDutchRepository _dutchRepo;
        public AppController(IDutchRepository dutchRepo)
        {
            _dutchRepo = dutchRepo;
        }

        public IActionResult Index()
        {
            var list = _dutchRepo.GetAllProducts();
            return View(list);
        }
    }
}