using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchTreatSeeder
    {
        private readonly DutchTreatDbContext _ctx;
        private readonly IHostingEnvironment _hosting;
        public DutchTreatSeeder(DutchTreatDbContext ctx, IHostingEnvironment hosting)
        {
            _ctx = ctx;
            _hosting = hosting;
        }

        public void Seed()
        {
            var ensureCreated = _ctx.Database.EnsureCreated();

            if (!_ctx.Products.Any())
            {
                // Need to create a sample data
                var filePath = Path.Combine(_hosting.ContentRootPath, "Data/art.json");
                var json = File.ReadAllText(filePath);
                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);
                _ctx.AddRange(products);

                var order = new Order
                {
                    OrderDate = DateTime.Now,
                    OrderNumber = "12345",
                    Items = new List<OrderItem>
                    {
                        new OrderItem{ Product = products.First(), Quantity = 5, UnitPrice = products.First().Price }
                    }
                };

                _ctx.Orders.Add(order);

                _ctx.SaveChanges();

            }
        }
    }
}
