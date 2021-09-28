using DataAccess.Entities;
using System.Collections.Generic;

namespace BusinessLogic.DTO
{
    public class Order
    {
        public Order()
        {
            Items = new List<OrderItem>();
        }

        public List<OrderItem> Items { get; set; }
    }
}