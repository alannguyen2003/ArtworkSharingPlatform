using System.Data;
using System.Security.AccessControl;
using ArtworkSharingPlatform.Domain.Entities.Artworks;
using ArtworkSharingPlatform.Domain.Entities.Commissions;
using ArtworkSharingPlatform.Domain.Entities.Configs;
using ArtworkSharingPlatform.Domain.Entities.Messages;
using ArtworkSharingPlatform.Domain.Entities.Orders;
using ArtworkSharingPlatform.Domain.Entities.PackagesInfo;
using ArtworkSharingPlatform.Domain.Entities.Transactions;
using ArtworkSharingPlatform.Domain.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ArtworkSharingPlatform.Domain.Migrations;

public class ArtworkSharingPlatformDbContext : IdentityDbContext<User,
    Role,
    int,
    IdentityUserClaim<int>,
    UserRole,
    IdentityUserLogin<int>,
    IdentityRoleClaim<int>,
    IdentityUserToken<int>
>

{
    public ArtworkSharingPlatformDbContext()
    {
    }

    public ArtworkSharingPlatformDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Artwork>? Artworks { get; set; }
    public DbSet<ArtworkImage>? ArtworkImages { get; set; }
    public DbSet<Like>? Likes { get; set; }
    public DbSet<Comment>? Comments { get; set; }
    public DbSet<Rating>? Ratings { get; set; }
    public DbSet<ConfigManager>? ConfigManagers { get; set; }
    public DbSet<PreOrder>? PreOrders { get; set; }
    public DbSet<PackageBilling>? PackageBilling { get; set; }
    public DbSet<PackageInformation>? PackageInformation { get; set; }
    public DbSet<Transaction>? Transactions { get; set; }
    public DbSet<Role>? Roles { get; set; }
    public DbSet<User>? Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<CommissionImage> CommissionImages { get; set; }
    public DbSet<CommissionRequest> CommissionRequests { get; set; }
    public DbSet<CommissionStatus> CommissionStatus { get; set; }
    public DbSet<Message> Messages{ get; set; }
    public DbSet<Connection> Connections{ get; set; }
    public DbSet<Group> Groups{ get; set; }
    public DbSet<Follow> Follows { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlServer(
                "Data Source=(local); database=ASPDatabase;" +
                "uid=sa;pwd=12345;" +
                "TrustServerCertificate=True;" +
                "MultipleActiveResultSets=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Like>().HasKey(
            k => new {k.UserId, k.ArtworkId} 
            );
        modelBuilder.Entity<Like>()
                .HasOne(a => a.Artwork)
                .WithMany(a => a.Likes)
                .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Like>()
                .HasOne(a => a.User)
                .WithMany(u => u.Likes)
                .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<Comment>()
                .HasOne(a => a.Artwork)
                .WithMany(a => a.Comments)
                .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Comment>()
            .HasOne(a => a.User)
            .WithMany(u => u.Comments)
            .OnDelete(DeleteBehavior.Cascade);
        //--------------------------------------
        modelBuilder.Entity<Follow>()
                .HasKey(k => new { k.SourceUserId, k.TargetUserId });
        modelBuilder.Entity<Follow>()
            .HasOne(fl => fl.SourceUser)
            .WithMany(u => u.FollowedUsers)
            .HasForeignKey(u => u.SourceUserId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Follow>()
            .HasOne(fl => fl.TargetUser)
            .WithMany(u => u.IsFollowedByUsers)
            .HasForeignKey(u => u.TargetUserId)
            .OnDelete(DeleteBehavior.NoAction);
        //--------------------------------------
        modelBuilder.Entity<Rating>().HasKey(
            k => new { k.UserId, k.ArtworkId }
            );
        modelBuilder.Entity<Rating>()
                .HasOne(a => a.Artwork)
                .WithMany(a => a.Ratings)
                .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Rating>()
                .HasOne(a => a.User)
                .WithMany(u => u.Ratings)
                .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Artwork>()
            .HasMany(e => e.Likes)
            .WithOne(e => e.Artwork)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Artwork>()
            .HasMany(e => e.Ratings)
            .WithOne(e => e.Artwork);
        modelBuilder.Entity<Artwork>()
            .HasMany(e => e.Comments)
            .WithOne(e => e.Artwork);
        modelBuilder.Entity<Artwork>()
            .HasOne(e => e.PreOrder)
            .WithOne(e => e.Artwork)
            .HasForeignKey<PreOrder>(e => e.ArtworkId);
        modelBuilder.Entity<Artwork>()
            .HasMany(e => e.ArtworkImages)
            .WithOne(e => e.Artwork);
        modelBuilder.Entity<Artwork>()
            .HasOne(e => e.Genre)
            .WithMany(e => e.Artworks);


        modelBuilder.Entity<PackageInformation>()
            .HasMany(e => e.PackageBillings)
            .WithMany(e => e.PackageInformation);
        modelBuilder.Entity<PackageInformation>()
            .HasMany(e => e.ConfigManagers)
            .WithMany(e => e.PackageConfigs);


        modelBuilder.Entity<Role>()
            .HasMany(e => e.UserRoles)
            .WithOne(e => e.Role)
            .HasForeignKey(e => e.RoleId)
            .IsRequired();


        modelBuilder.Entity<CommissionStatus>()
            .HasMany(e => e.CommissionHistories)
            .WithOne(e => e.CommissionStatus);


        modelBuilder.Entity<Genre>()
            .HasMany(e => e.CommissionRequests)
            .WithOne(e => e.Genre);


        modelBuilder.Entity<CommissionRequest>()
            .HasMany(e => e.CommissionImages)
            .WithOne(e => e.CommissionRequest);

        
        modelBuilder.Entity<User>()
            .HasMany(e => e.UserRoles)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId)
            .IsRequired();
        modelBuilder.Entity<User>()
            .HasMany(e => e.Artworks)
            .WithOne(e => e.Owner);
        modelBuilder.Entity<User>()
            .HasMany(e => e.Likes)
            .WithOne(e => e.User);
        modelBuilder.Entity<User>()
            .HasMany(e => e.Comments)
            .WithOne(e => e.User);
        modelBuilder.Entity<User>()
            .HasMany(e => e.Ratings)
            .WithOne(e => e.User);
        modelBuilder.Entity<User>()
            .HasMany(e => e.PreOrders)
            .WithOne(e => e.Buyer);
        modelBuilder.Entity<User>()
            .HasMany(e => e.PackageBillings)
            .WithOne(e => e.User);
        modelBuilder.Entity<User>()
            .HasMany(e => e.Transactions)
            .WithOne(e => e.Manager);
        modelBuilder.Entity<User>()
            .HasMany(e => e.ConfigManagers)
            .WithOne(e => e.Administrator);
        modelBuilder.Entity<User>()
            .HasMany(e => e.CommissionSent)
            .WithOne(e => e.Sender)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<User>()
            .HasMany(e => e.CommissionReceived)
            .WithOne(e => e.Receiver)
            .OnDelete(DeleteBehavior.NoAction);

        
        modelBuilder.Entity<Message>()
            .HasOne(x => x.Recipient)
            .WithMany(x => x.MessageReceived)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<Message>()
            .HasOne(x => x.Sender)
            .WithMany(x => x.MessageSent)
            .OnDelete(DeleteBehavior.NoAction);

       
    }
}