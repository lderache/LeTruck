using TheTruck.Entities;

namespace TheTruck.Web.Models
{
    public class CartViewModel
    {
        public int Id { get; set; }
        public Genre Category { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
        public decimal SubTotal { get; set; }
    }
}