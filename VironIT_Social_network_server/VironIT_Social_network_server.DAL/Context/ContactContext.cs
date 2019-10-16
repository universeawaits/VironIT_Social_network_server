using Microsoft.EntityFrameworkCore;

using VironIT_Social_network_server.DAL.Model;


namespace VironIT_Social_network_server.DAL.Context
{
    public class ContactContext : DbContext
    {
        public virtual DbSet<ContactUser> ContactedUsers { get; set; }
        public virtual DbSet<BlockedUser> BlockedUsers { get; set; }

        public ContactContext()
        {
        }

        public ContactContext(DbContextOptions<ImageContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
