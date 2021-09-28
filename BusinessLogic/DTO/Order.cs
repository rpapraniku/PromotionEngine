using DataAccess.Entities;
using System.Collections.Generic;

namespace BusinessLogic.DTO
{
    public class Order
    {
        public List<OrderItem> Items { get; set; }
    }
}