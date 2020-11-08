using Commander.Models;
using Microsoft.EntityFrameworkCore;

namespace Commander.Data
{
    public class CommanderContext : DbContext
    {
        public CommanderContext(DbContextOptions<CommanderContext> opt) 
        : base(opt)
        {            
        }

        public DbSet<Command> Commands { get; set; }
        public DbSet<Instruction> Instructions { get; set; }
        public DbSet<Platform> Platforms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
 
            modelBuilder.Entity<Command>()
                .HasOne(b => b.Instructions)
                .WithOne(i => i.Command)
                .HasForeignKey<Instruction>(b => b.CommandId);

            modelBuilder.Entity<Command>()
                .HasOne(b => b.Platform)
                .WithMany()
                .HasForeignKey(p => p.PlatformId);
        }

    }
}