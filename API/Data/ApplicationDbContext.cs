using API.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class ApplicationDbContext:IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options){}
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostLike> Likes{get;set;}
        public DbSet<Follow> Follows{get;set;}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
             builder.Entity<User>()
            .HasMany(u => u.Posts)
            .WithOne(p => p.Author)
            .HasForeignKey(p => p.AuthorId);
            builder.Entity<User>()
            .HasMany(u => u.Followed)
            .WithOne(p => p.Followed)
            .HasForeignKey(p => p.FollowedId);
            builder.Entity<User>()
            .HasMany(u => u.Followers)
            .WithOne(p => p.Follower)
            .HasForeignKey(p => p.FollowerId);
            
        }
    }
}