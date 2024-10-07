namespace FlowerShop.Models
{
    public class Flower
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; } // loại
        public int Quantity { get; set; } // số lượng
        public string Status { get; set; } // tình trạng
    }
}
