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

        public IEnumerable<Order> GetAllOrders(bool includeItems)
        {
            if (!includeItems)
                return _ctx.Orders.ToList();
            else
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

        public IEnumerable<Product> GetAllProducts(int? amountToTake)
        {
            var query = _ctx.Products;

            if (amountToTake > 0)
            {
                return query
                    .Take(amountToTake.Value)
                    .ToList();
            }
            else
            {
                return query
               .ToList();
            }
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

        public IEnumerable<Order> GetAllOrdersByUser(string username, bool includeItems)
        {
            var query = _ctx.Orders
                .Include(i => i.User)
                .Where(x => x.User.UserName == username);

            if (!includeItems)
            {
                return query.ToList();
            }
            else
            {
                return query
                    .Include(i => i.Items)
                    .ThenInclude(i => i.Product)
                    .ToList();
            }
        }

        public void AddOrder(Order order)
        {
            // convert new products to lookup of product
            foreach(var item in order.Items)
            {
                item.Product = _ctx.Products.Find(item.Product.Id);
            }

            AddEntity(order);
        }
    }
}
