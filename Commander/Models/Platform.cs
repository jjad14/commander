using System.ComponentModel.DataAnnotations;

namespace Commander.Models
{
    public class Platform
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }
    }
}