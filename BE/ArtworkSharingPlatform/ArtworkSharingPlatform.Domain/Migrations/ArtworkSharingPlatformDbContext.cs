﻿using System.Data;
using System.Security.AccessControl;
using ArtworkSharingPlatform.Domain.Entities.Artworks;
using ArtworkSharingPlatform.Domain.Entities.Configs;
using ArtworkSharingPlatform.Domain.Entities.Orders;
using ArtworkSharingPlatform.Domain.Entities.Packages;
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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlServer(
                "Data Source=(local); database=ASPDatabase;uid=sa;pwd=12345;TrustServerCertificate=True");

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Artwork>()
            .HasMany(e => e.Likes)
            .WithOne(e => e.Artwork);
        modelBuilder.Entity<Artwork>()
            .HasMany(e => e.Ratings)
            .WithOne(e => e.Artwork);
        modelBuilder.Entity<Artwork>()
            .HasMany(e => e.Comments)
            .WithOne(e => e.Artwork);
        modelBuilder.Entity<Artwork>()
            .HasOne(e => e.PreOrder)
            .WithOne(e => e.Artwork)
            .HasForeignKey<PreOrder>(e=>e.ArtworkId);
        modelBuilder.Entity<Artwork>()
            .HasMany(e => e.ArtworkImages)
            .WithOne(e => e.Artwork);
        modelBuilder.Entity<Artwork>()
            .HasMany(e => e.Genres)
            .WithOne(e => e.Artwork);
        
        modelBuilder.Entity<PackageInformation>()
            .HasMany(e => e.PackageBillings)
            .WithMany(e => e.PackageInformation);
        modelBuilder.Entity<PackageInformation>()
            .HasMany(e => e.ConfigManagers)
            .WithMany(e => e.PackageConfigs);
       
        modelBuilder.Entity<User>()
            .HasMany(e => e.UserRoles)
            .WithOne(e=>e.User)
            .HasForeignKey(e =>e.UserId)
            .IsRequired();
        modelBuilder.Entity<Role>()
           .HasMany(e => e.UserRoles)
           .WithOne(e => e.Role)
           .HasForeignKey(e =>e.RoleId)
           .IsRequired();

        modelBuilder.Entity<User>()
            .HasMany(e => e.Artworks)
            .WithOne(e => e.Owner);
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
    }
}