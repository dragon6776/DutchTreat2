using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DutchTreat.Controllers
{
    [Route("/api/orders/{orderid}/items")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderItemsController : Controller
    {
        private readonly IDutchTreatRepository _repo;
        private readonly IMapper _mapper;

        public OrderItemsController(IDutchTreatRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public IActionResult Get(int orderId)
        {
            var order = _repo.GetOrderById(orderId);
            if (order != null)
            {
                var models = _mapper.Map<IEnumerable<OrderItem>, IEnumerable<OrderItemViewModel>>(order.Items);
                return Ok(models);
            }

            return NotFound();
        }

        [HttpGet("{id}")]
        public IActionResult Get(int orderId, int id)
        {
            var order = _repo.GetOrderById(orderId);
            if (order != null)
            {
                var item = order.Items.FirstOrDefault(x => x.Id == id);
                var model = _mapper.Map<OrderItem, OrderItemViewModel>(item);
                return Ok(model);
            }

            return NotFound();
        }
    }
}