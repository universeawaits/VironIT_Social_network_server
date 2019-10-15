using Microsoft.AspNetCore.Identity;
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

            builder.Entity<User>(
                entity => 
                {
                    entity.ToTable("Users");
                });
            builder.Entity<IdentityRole>(
                entity =>
                {
                    entity.ToTable("Roles");
                });
            builder.Entity<IdentityUserRole<string>>(
                entity =>
                {
                    entity.ToTable("UserRoles");
                });
            builder.Entity<IdentityUserClaim<string>>(
                entity =>
                {
                    entity.ToTable("UserClaims");
                });
            builder.Entity<IdentityUserLogin<string>>(
                entity =>
                {
                    entity.ToTable("Logins");
                });
            builder.Entity<IdentityUserToken<string>>(
                entity =>
                {
                    entity.ToTable("Tokens");
                });
            builder.Entity<IdentityRoleClaim<string>>(
                entity =>
                {
                    entity.ToTable("RoleClaims");
                });
        }

        public static IdentityContext Create()
        {
            return new IdentityContext();
        }
    }
}
