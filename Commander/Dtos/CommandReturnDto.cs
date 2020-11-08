using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Commander.Models;

namespace Commander.Dtos
{
    public class CommandReturnDto
    {
        public int Id { get; set; }
        public string Task { get; set; }
        public string Instructions { get; set; }
        public int PlatformId { get; set; }
        public string PlatformName { get; set; }
    }
}