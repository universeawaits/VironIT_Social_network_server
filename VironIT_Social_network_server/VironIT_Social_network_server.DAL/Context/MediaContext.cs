using Microsoft.EntityFrameworkCore;

using VironIT_Social_network_server.DAL.Model;


namespace VironIT_Social_network_server.DAL.Context
{
    public class MediaContext : DbContext
    {
        public virtual DbSet<Avatar> Avatars { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<Video> Videos { get; set; }
        public virtual DbSet<Audio> Audios { get; set; }

        public MediaContext()
        {
        }

        public MediaContext(DbContextOptions<MediaContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
