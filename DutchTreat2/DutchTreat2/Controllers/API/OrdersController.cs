using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DutchTreat2.Data;
using DutchTreat2.Data.Entities;

namespace DutchTreat2.Controllers.API
{
    [Route("api/[Controller]")]
    public class OrdersController : Controller
    {
        private readonly IDutchRepository _repo;
        public OrdersController(IDutchRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_repo.GetAllOrders());
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var order = _repo.GetOrderById(id);
            if (order == null)
                return NotFound();
            return Ok(order);
        }
    }
}