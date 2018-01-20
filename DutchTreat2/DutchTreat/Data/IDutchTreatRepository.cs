using System.Collections.Generic;
using DutchTreat.Data.Entities;

namespace DutchTreat.Data
{
    public interface IDutchTreatRepository
    {
        IEnumerable<Product> GetAllProducts(int? amountToTake);
        IEnumerable<Product> GetProductsByCategory(string category);
        bool SaveAll();
        IEnumerable<Order> GetAllOrders(bool includeItems);
        IEnumerable<Order> GetAllOrdersByUser(string username, bool includeItems);
        Order GetOrderById(int id);
        void AddEntity(object model);
        void AddOrder(Order order);
    }
}