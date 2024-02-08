using API.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    /// <summary>
    /// This class represents the application's database context
    /// </summary>
    public class ApplicationDbContext:IdentityDbContext<User>
    {
        /// <summary>
        /// Initializes a new instance of the ApplicationDbContext class with the specified DbContextOptions.
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options){}
        /// <summary>
        /// A DbSet representing the Comment entity in the database.
        /// </summary>
        public DbSet<Comment> Comments { get; set; }
        /// <summary>
        /// A DbSet representing the Post entity in the database.
        /// </summary>
        public DbSet<Post> Posts { get; set; }
        /// <summary>
        /// A DbSet representing the PostLike entity in the database.
        /// </summary>
        public DbSet<PostLike> Likes{get;set;}
        /// <summary>
        /// A DbSet representing the Follow entity in the database.
        /// </summary>
        public DbSet<Follow> Follows{get;set;}
        /// <summary>
        /// A DbSet representing the Message entity in the database.
        /// </summary>
        public DbSet<Message> Messages{get;set;}
        /// <summary>
        /// A DbSet representing the Notification entity in the database.
        /// </summary>
        public DbSet<Notification> Notifications{get;set;}
        /// <summary>
        /// Configures the relationships between entities and their corresponding tables in the database.
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
             builder.Entity<User>()
            .HasMany(u => u.Posts)
            .WithOne(p => p.Author)
            .HasForeignKey(p => p.AuthorId);
            builder.Entity<User>()
            .HasMany(u => u.Followed)
            .WithOne(p => p.Follower)
            .HasForeignKey(p => p.FollowerId);
            builder.Entity<User>()
            .HasMany(u => u.Followers)
            .WithOne(p => p.Followed)
            .HasForeignKey(p => p.FollowedId);
            builder.Entity<User>()
            .HasMany(u => u.MessagesReceived)
            .WithOne(m => m.Receiver)
            .HasForeignKey(m => m.ReceiverId);
            builder.Entity<User>()
            .HasMany(u=> u.MessagesSend)
            .WithOne(p => p.Sender)
            .HasForeignKey(p => p.SenderId);
            
        }
    }
}