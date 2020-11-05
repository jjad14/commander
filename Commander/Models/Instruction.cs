using System.ComponentModel.DataAnnotations;

namespace Commander.Models
{
    public class Instruction
    {
        // PK
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        // FK
        public int CommandId { get; set; }
        public Command Command { get; set; }
    }
}