using DutchTreat.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchTreatRepository : IDutchTreatRepository
    {
        private readonly DutchTreatDbContext _ctx;

        public DutchTreatRepository(DutchTreatDbContext ctx)
        {
            _ctx = ctx;
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _ctx.Orders
                .Include(i => i.Items)
                .ThenInclude(i=>i.Product)
                .ToList();
        }

        public Order GetOrderById(int id)
        {
            return _ctx.Orders
                .Include(i => i.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _ctx.Products
                .OrderBy(o => o.Title)
                .ToList();
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return _ctx.Products
                .Where(x => x.Category == category)
                .ToList();
        }

        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }

        public void AddEntity(object model)
        {
            _ctx.Add(model);
        }
    }
}
