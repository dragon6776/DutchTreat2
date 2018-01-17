using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DutchTreat.Data;
using Microsoft.AspNetCore.Mvc;

namespace DutchTreat.Controllers
{
    [Route("api/[Controller]")]
    public class ProductsController : Controller
    {
        private readonly IDutchTreatRepository _dutchRepository;

        public ProductsController(IDutchTreatRepository dutchRepository)
        {
            _dutchRepository = dutchRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_dutchRepository.GetAllProducts());
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to get products");
            }
        }
    }
}