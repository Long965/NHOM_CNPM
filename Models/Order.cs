using System;
using System.Collections.Generic;

namespace FlowerShop.Models
{
    public class Order
    {
        public int Id { get; set; }
        public List<Flower> Flowers { get; set; } // Danh sách các hoa trong đơn hàng
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
    }
}
