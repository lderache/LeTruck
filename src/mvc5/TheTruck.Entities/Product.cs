using System.ComponentModel.DataAnnotations;

namespace TheTruck.Entities
{
    public class Product
    {
        public int Id { get; set; }

        [Required]
        public Genre Category { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

    }
}
