using DutchTreat2.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DutchTreat2.Data
{
    public class DutchRepository : IDutchRepository
    {
        private readonly DutchTreatDbContext _ctx;
        public DutchRepository(DutchTreatDbContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _ctx.Orders
                .Include(i => i.Items)
                .ThenInclude(i => i.Product)
                .ToList();
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _ctx.Products
                .OrderBy(p => p.Id)
                .ToList();
        }

        public Order GetOrderById(int id)
        {
            return _ctx.Orders
              .Include(i => i.Items)
              .ThenInclude(i => i.Product)
              .FirstOrDefault();
        }
    }
}
