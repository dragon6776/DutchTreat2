using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Controllers
{
    [Route("api/[Controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrdersController : Controller
    {
        private readonly IDutchTreatRepository _repo;
        private readonly IMapper _mapper;

        public OrdersController(IDutchTreatRepository dutchTreatRepository, IMapper mapper)
        {
            this._repo = dutchTreatRepository;
            this._mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get(bool includeItems = true)
        {
            try
            {
                var username = User.Identity.Name; // "admin@gmail.com";
                //var orders = _dutchTreatRepository.GetAllOrders(includeItems);
                var orders = _repo.GetAllOrdersByUser(username, includeItems);
                var models = _mapper.Map<IEnumerable<Order>, IEnumerable< OrderViewModel >> (orders);
                return Ok(models);
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
                var order = _repo.GetOrderById(id);
                if (order == null)
                    return NotFound();

                var model = _mapper.Map<Order, OrderViewModel>(order);

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest($"Failed to get order #{id}");
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody]OrderViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var newOrder = _mapper.Map<OrderViewModel, Order>(model);

                    if(newOrder.OrderDate == DateTime.MinValue)
                        newOrder.OrderDate = DateTime.Now;

                    _repo.AddOrder(newOrder);

                    if (_repo.SaveAll())
                    {
                        model = _mapper.Map<Order, OrderViewModel>(newOrder);
                        return Created($"/api/orders/{model.OrderId}", model);
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return BadRequest("Failed to save new order");
        }
    }
}
