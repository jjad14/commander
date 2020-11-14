using System.Collections.Generic;
using Commander.Models.Identity;

namespace Commander.Models
{
    public class User
    {
        public string DisplayName { get; set; }
        public ICollection<Command> Commands { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}