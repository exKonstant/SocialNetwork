using Microsoft.EntityFrameworkCore;
using SocialNetwork.DAL.Entities;

namespace SocialNetwork.DAL.EF
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }        
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<UserFriend> UserFriends { get; set; }
        public DbSet<UserConversation> UserConversations { get; set; }
        public DbSet<FriendRequest> FriendRequests { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> dbContextOptions) : base(dbContextOptions)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {          
            modelBuilder.Entity<UserConversation>().HasKey(a => new { a.UserId, a.ConversationId });

            modelBuilder.Entity<UserFriend>().HasKey(a => new { a.UserId, a.FriendId });

            modelBuilder.Entity<FriendRequest>().HasKey(a => new { a.SenderId, a.ReceiverId });

            modelBuilder.Entity<UserFriend>()
                .HasOne(a => a.User)
                .WithMany(a => a.Friends)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict);            

            modelBuilder.Entity<FriendRequest>()
                .HasOne(a => a.Sender)
                .WithMany(a => a.FriendRequestSent)
                .HasForeignKey(a => a.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FriendRequest>()
                .HasOne(a => a.Receiver)
                .WithMany(a => a.FriendRequestReceived)
                .HasForeignKey(a => a.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            DatabaseSeeder.Seed(modelBuilder);            
        }
    }
}
