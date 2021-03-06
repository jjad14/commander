using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Commander.Models;

namespace Commander.Dtos
{
    public class CommandCreateDto
    {
        [Required]
        [MaxLength(200)]
        public string Task { get; set; }

        [Required]
        public string Instructions { get; set; }

        [Required]
        public int PlatformId { get; set; }

        [Required]
        public string PlatformName { get; set; }
    }
}