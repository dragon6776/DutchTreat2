using DutchTreat.Data;
using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Controllers
{
    [Route("api/[Controller]")]
    public class OrdersController : Controller
    {
        private readonly IDutchTreatRepository _dutchTreatRepository;

        public OrdersController(IDutchTreatRepository dutchTreatRepository)
        {
            this._dutchTreatRepository = dutchTreatRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var orders = _dutchTreatRepository.GetAllOrders();
                return Ok(orders);
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to get orders");
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var order = _dutchTreatRepository.GetOrderById(id);
                if (order == null)
                    return NotFound();

                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to get order #{id}");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]Order model)
        {
            try
            {
                _dutchTreatRepository.AddEntity(model);

                if(_dutchTreatRepository.SaveAll())
                    return Created($"/api/orders/{model.Id}", model);
            }
            catch (Exception ex)
            {
            }

            return BadRequest("Failed to save new order");

        }
    }
}
