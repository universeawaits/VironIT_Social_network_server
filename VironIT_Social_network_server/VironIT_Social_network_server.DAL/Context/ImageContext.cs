using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

using VironIT_Social_network_server.DAL.Model;


namespace VironIT_Social_network_server.DAL.Context
{
    public partial class ImageContext : DbContext
    {
        private readonly IConfiguration _configuration;
        private readonly IHostingEnvironment _env;

        public ImageContext(IConfiguration configuration, IHostingEnvironment env)
        {
            Database.EnsureCreated();
            _configuration = configuration;
            _env = env;
        }

        public ImageContext(DbContextOptions<ImageContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Image> Images { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // ????
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = _env.IsProduction() ? _configuration["ConnectionStrings:Prod"]
                    : _configuration["ConnectionStrings:Dev"];
                optionsBuilder.UseMySQL(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Image>().HasKey(image => image.Id);

            modelBuilder.Entity<Image>().Property(image => image.RelativePath).IsRequired(true);
        }
    }
}
