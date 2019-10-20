using Microsoft.EntityFrameworkCore;

using VironIT_Social_network_server.DAL.Model;


namespace VironIT_Social_network_server.DAL.Context
{
    public class MessageContext : DbContext
    {
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<MessageMedia> MessagesMedia { get; set; }

        public MessageContext()
        {
        }

        public MessageContext(DbContextOptions<MessageContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MessageMedia>()
                .HasOne<Message>(mediaMessage => mediaMessage.Message)
                .WithOne(message => message.MessageMedia)
                .HasForeignKey<Message>(message => message.MessageMediaId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
