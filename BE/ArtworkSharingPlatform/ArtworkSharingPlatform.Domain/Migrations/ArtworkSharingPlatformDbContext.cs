using System.Data;
using System.Security.AccessControl;
using ArtworkSharingPlatform.Domain.Entities.Artworks;
using ArtworkSharingPlatform.Domain.Entities.Configs;
using ArtworkSharingPlatform.Domain.Entities.Joins;
using ArtworkSharingPlatform.Domain.Entities.Orders;
using ArtworkSharingPlatform.Domain.Entities.Packages;
using ArtworkSharingPlatform.Domain.Entities.Transactions;
using ArtworkSharingPlatform.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ArtworkSharingPlatform.Domain.Migrations;

public class ArtworkSharingPlatformDbContext : DbContext
{
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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // IConfigurationRoot configuration = new ConfigurationBuilder()
        //     .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
        //     .AddJsonFile("appsettings.json")
        //     .Build();
        // string connectionString = configuration.GetConnectionString("ASPDbConnection")!;
        // optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Artwork>(entity =>
        {
            entity.ToTable("Artworks");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();
            entity.HasMany(e => e.Likes)
                .WithOne(e => e.Artwork)
                .HasForeignKey(e => e.Id);
            entity.HasMany(e => e.Comments)
                .WithOne(e => e.Artwork)
                .HasForeignKey(e => e.Id);
            entity.HasMany(e => e.Ratings)
                .WithOne(e => e.Artwork)
                .HasForeignKey(e => e.Id);
            entity.HasMany(e => e.ArtworkImages)
                .WithOne(e => e.Artwork)
                .HasForeignKey(e => e.Id);
            entity.HasOne(e => e.Owner)
                .WithMany(e => e.Artworks);
            entity.HasOne(e => e.PreOrder)
                .WithOne(e => e.Artwork)
                .HasForeignKey<PreOrder>(e=>e.Id);
        });

        modelBuilder.Entity<ArtworkImage>(entity =>
        {
            entity.ToTable("ArtworkImages");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();
        });
        modelBuilder.Entity<Like>(entity =>
        {
            entity.ToTable("Likes");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();
        });
        modelBuilder.Entity<Comment>(entity =>
        {
            entity.ToTable("Comments");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();
        });
        modelBuilder.Entity<Rating>(entity =>
        {
            entity.ToTable("Ratings");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();
        });
        modelBuilder.Entity<ConfigManager>(entity =>
        {
            entity.ToTable("ConfigManagers");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();
        });
        modelBuilder.Entity<PreOrder>(entity =>
        {
            entity.ToTable("PreOrders");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();
        });
        modelBuilder.Entity<PackageBilling>(entity =>
        {
            entity.ToTable("PackageBillings");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();
        });
        modelBuilder.Entity<PackageInformation>(entity =>
        {
            entity.ToTable("PackageInformation");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();
            entity.HasMany(e => e.PackageBillings)
                .WithMany(e => e.PackageInformation);
                // .UsingEntity(
                //
                //     right =>
                //         right.HasOne(typeof(PackageInformation))
                //             .WithMany()
                //             // .HasForeignKey("PackageInformationId")
                //             // .HasPrincipalKey(nameof(Entities.Packages.PackageInformation.Id)),
                //     left =>
                //         left.HasOne(typeof(PackageBilling))
                //             .WithMany()
                //             .HasForeignKey("PackageBillingId")
                //             .HasPrincipalKey(nameof(Entities.Packages.PackageBilling.Id))
                // );
                entity.HasMany(e => e.ConfigManagers)
                    .WithMany(e => e.PackageConfigs);
                // .UsingEntity(
                //     "PackageConfigsConfigManagers",
                //     right =>
                //         right.HasOne(typeof(ConfigManager))
                //             .WithMany()
                //             .HasForeignKey("ConfigManagerId")
                //             .HasPrincipalKey(nameof(ConfigManager.Id)),
                //     left =>
                //         left.HasOne(typeof(PackageInformation))
                //             .WithMany()
                //             .HasForeignKey("PackageConfigsId")
                //             .HasPrincipalKey(nameof(Entities.Packages.PackageInformation.Id)),
                //     join =>
                //         join.HasKey("PackageConfigsId", "ConfigManagerId")
                // );
        });
        modelBuilder.Entity<Transaction>(entity =>
        {
            entity.ToTable("Transactions");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();
        });
        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Roles");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();
        });
        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd();
            entity.HasMany(e => e.PackageBillings)
                .WithOne(e => e.User);
            entity.HasOne(e => e.Role)
                .WithOne(e => e.User);
            entity.HasMany(e => e.Transactions)
                .WithOne(e => e.User);
        });
    }
}