using System.ComponentModel.DataAnnotations;

namespace TheTruck.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public Genre Category { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public int Discount { get; set; }
    }
}
