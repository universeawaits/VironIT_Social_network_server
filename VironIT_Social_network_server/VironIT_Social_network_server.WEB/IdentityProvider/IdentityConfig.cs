using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace VironIT_Social_network_server.WEB.Identity
{
    public class IdentityContext : IdentityDbContext<User>
    {
        public IdentityContext()
        {
        }

        public IdentityContext(DbContextOptions<IdentityContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public static IdentityContext Create()
        {
            return new IdentityContext();
        }
    }
}
