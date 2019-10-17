using Microsoft.EntityFrameworkCore;

using VironIT_Social_network_server.DAL.Model;


namespace VironIT_Social_network_server.DAL.Context
{
    public class ContactContext : DbContext
    {
        public virtual DbSet<Contact> Contacts { get; set; }
        public virtual DbSet<Block> Blocks { get; set; }
        public virtual DbSet<Pseudonym> Pseudonyms { get; set; }

        public ContactContext()
        {
        }

        public ContactContext(DbContextOptions<ContactContext> options)
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
