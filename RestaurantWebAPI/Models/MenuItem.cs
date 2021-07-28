using System.ComponentModel.DataAnnotations;

namespace RestaurantWebAPI.Models
{
    public class MenuItem
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }

        public double Price { get; set; }
    }
}
