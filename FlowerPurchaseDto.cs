using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventFlowerExchange.services.DTO
{
    public class FlowerPurchaseDto
    {
        public int FlowerId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }

}
