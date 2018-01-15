using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DutchTreat2.Data.Entities;
using DutchTreat2.Data;

namespace DutchTreat2.Controllers.API
{
    [Route("api/[Controller]")]
    public class ProductsController : Controller
    {
        private readonly IDutchRepository _dutchRepo;
        public ProductsController(IDutchRepository dutchRepo)
        {
            _dutchRepo = dutchRepo;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _dutchRepo.GetAllProducts();
        }
    }
}