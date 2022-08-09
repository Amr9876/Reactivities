using Microsoft.EntityFrameworkCore;
using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Persistence;

public class DataContext : IdentityDbContext<AppUser>
{
    public DataContext(DbContextOptions options) 
        : base(options){}

    public DbSet<Activity> Activities => Set<Activity>();

    public DbSet<ActivityAttendee> ActivityAttendees => Set<ActivityAttendee>();

    public DbSet<Photo> Photos => Set<Photo>();

    public DbSet<Comment> Comments => Set<Comment>();

    public DbSet<UserFollowing> UserFollowings => Set<UserFollowing>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ActivityAttendee>()
            .HasKey(aa => new { aa.ActivityId, aa.AppUserId });

        builder.Entity<ActivityAttendee>()
            .HasOne(a => a.AppUser)
            .WithMany(au => au.Activities)
            .HasForeignKey(a => a.AppUserId);

        builder.Entity<ActivityAttendee>()
            .HasOne(a => a.Activity)
            .WithMany(a => a.Attendees)
            .HasForeignKey(a => a.ActivityId);

        builder.Entity<Comment>()
            .HasOne(a => a.Activity)
            .WithMany(a => a.Comments)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<UserFollowing>(b => 
        {
            b.HasKey(x => new { x.ObserverId, x.TargetId });

            b.HasOne(x => x.Observer)
                .WithMany(x => x.Followings)
                .HasForeignKey(x => x.ObserverId)
                .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(x => x.Target)
                .WithMany(x => x.Followers)
                .HasForeignKey(x => x.TargetId)
                .OnDelete(DeleteBehavior.Cascade);    
            
        });        
    }
}