using Microsoft.EntityFrameworkCore;

using VironIT_Social_network_server.DAL.Model;


namespace VironIT_Social_network_server.DAL.Context
{
    public class ImageContext : DbContext
    {
        public virtual DbSet<Image> Avatars { get; set; }

        public ImageContext()
        {
        }

        public ImageContext(DbContextOptions<ImageContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Image>().HasKey(image => image.Id);

            modelBuilder.Entity<Image>().Property(image => image.Link).IsRequired(true);
            modelBuilder.Entity<Image>().Property(image => image.UserEmail).IsRequired(true);
        }
    }
}
