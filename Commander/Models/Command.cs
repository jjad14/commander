using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Commander.Models
{
    public class Command
    {
        // PK
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Task { get; set; }
        public Instruction Instructions { get; set; }

        public Platform Platform { get; set; }
        public int PlatformId { get; set; }
        
    }
}