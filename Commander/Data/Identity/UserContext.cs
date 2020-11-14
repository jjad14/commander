using Commander.Models;
using Commander.Models.Identity;
using Microsoft.EntityFrameworkCore;

namespace Commander.Data.Identity
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> opt) 
        : base(opt)
        {            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
 
        }
    }
}

//     public class AppIdentityDbContext : IdentityDbContext<User, Role, int,
//         IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>,
//         IdentityRoleClaim<int>, IdentityUserToken<int>>
//     {
//         public AppIdentityDbContext(DbContextOptions<AppIdentityDbContext> options) 
//         : base(options)
//         {
//         }

//         protected override void OnModelCreating(ModelBuilder builder) 
//         {
//             base.OnModelCreating(builder);

//             builder.Entity<User>()
//                 .HasMany(ur => ur.UserRoles)
//                 .WithOne(u => u.User)
//                 .HasForeignKey(ur => ur.UserId)
//                 .IsRequired();

//             builder.Entity<Role>()
//                 .HasMany(ur => ur.UserRoles)
//                 .WithOne(u => u.Role)
//                 .HasForeignKey(ur => ur.RoleId)
//                 .IsRequired();

//             builder.Entity<User>()
//                 .HasOne(u => u.Address)
//                 .WithOne(u => u.User)
//                 .HasForeignKey<Address>(a => a.UserId)
//                 .IsRequired();

//             builder.Entity<Address>()
//                 .HasOne(a => a.User)
//                 .WithOne(a => a.Address)
//                 .IsRequired();
//         }
//     }
// }