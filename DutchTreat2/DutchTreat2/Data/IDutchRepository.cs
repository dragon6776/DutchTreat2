using System.Collections.Generic;
using DutchTreat2.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DutchTreat2.Data
{
    public interface IDutchRepository
    {
        IEnumerable<Product> GetAllProducts();
        IEnumerable<Order> GetAllOrders();
        Order GetOrderById(int id);
    }
}