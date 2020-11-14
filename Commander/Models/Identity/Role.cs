using System.Collections.Generic;

namespace Commander.Models.Identity
{
    public class Role
    {
        public ICollection<UserRole> UserRoles { get; set; }
    }
}