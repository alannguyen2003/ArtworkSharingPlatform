﻿using ArtworkSharingPlatform.Domain.Entities.Artworks;
using ArtworkSharingPlatform.Domain.Entities.Users;
using ArtworkSharingPlatform.Domain.Migrations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using ArtworkSharingPlatform.Domain.Entities.Commissions;
using ArtworkSharingPlatform.Domain.Entities.PackagesInfo;
using ArtworkSharingPlatform.Domain.Entities.Transactions;
using ArtworkSharingPlatform.Domain.Entities.Configs;
using System.Net;

namespace ArtworkSharingPlatform.Infrastructure
{
    public class Seed
    {
        public static async Task SeedArtwork(ArtworkSharingPlatformDbContext context)
        {
            if (await context.Genres.AnyAsync())
            {
                return;
            }
            var genres = new List<Genre>
            {
                new Genre {Name = "Landscape"},
                new Genre {Name = "Portrait"},
                new Genre {Name = "Sculpture"},
                new Genre {Name = "Illustration"},
                new Genre {Name = "Textile Art"},
                new Genre {Name = "Wood Sculpture"},
                new Genre {Name = "Expressionism"},
                new Genre {Name = "Abstract Art"},
                new Genre {Name = "Surrealism"},
                new Genre {Name = "Realism Art"},
                new Genre {Name = "Animal Art"},
                new Genre {Name = "History Painting"}

            };
            foreach (var genre in genres)
            {
                await context.Genres.AddAsync(genre);
            }
            await context.SaveChangesAsync();

            if (await context.Artworks.AnyAsync())
            {
                return;
            }
            var artworks = await File.ReadAllTextAsync("../ArtworkSharingPlatform.Infrastructure/ArtworkSeed.json");
            var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var arts = JsonSerializer.Deserialize<List<Artwork>>(artworks, jsonOptions);
            foreach (var art in arts)
            {
                art.ArtworkImages.First().IsThumbnail = true;
                await context.Artworks.AddAsync(art);
            }
            await context.SaveChangesAsync();
        }

        public static async Task SeedUser(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            if (await userManager.Users.AnyAsync()) return;

            var userData = await File.ReadAllTextAsync("../ArtworkSharingPlatform.Infrastructure/UserSeed.json");
            var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var users = JsonSerializer.Deserialize<List<User>>(userData, jsonOptions);

            var roles = new List<Role>
            {
                new Role { Name = "Audience"},
                new Role {Name = "Artist"},
                new Role {Name = "Manager"},
                new Role {Name = "Admin"}
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            foreach (var user in users)
            {
                user.UserName = user.Email;
                user.EmailConfirmed = true;
                var result = await userManager.CreateAsync(user, "Pa$$w0rd");
                await userManager.AddToRoleAsync(user, "Audience");
            }

            var admin = new User
            {
                UserName = "admin@gmail.com",
                Name = "admin",
                Email = "admin@gmail.com",
                Status = 1,
                EmailConfirmed = true,
                Description = "I am an admin"
            };

            var resultAdmin = await userManager.CreateAsync(admin, "Pa$$w0rd");
            await userManager.AddToRolesAsync(admin, new[] { "Admin", "Artist", "Audience" });

            var manager = new User
            {
                UserName = "manager@gmail.com",
                Name = "manager",
                Email = "manager@gmail.com",
                Status = 1,
                EmailConfirmed = true,
                Description = "I am a manager"
            };
            await userManager.CreateAsync(manager, "Pa$$w0rd");
            await userManager.AddToRolesAsync(manager, new[] { "Manager", "Artist", "Audience" });

            var artist = new User
            {
                UserName = "picasso@gmail.com",
                Name = "Pablo Picasso",
                Email = "picasso@gmail.com",
                PhoneNumber = "0123456789",
                Status = 1,
                EmailConfirmed = true,
                Description = "I am an artist",
                UserImage = new UserImage
                {
                    Url = "https://images.pexels.com/photos/11098559/pexels-photo-11098559.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2"
                }
            };
            await userManager.CreateAsync(artist, "Pa$$w0rd");
            await userManager.AddToRolesAsync(artist, new[] { "Artist", "Audience" });
        }

        public static async Task SeedCommissionStatus(ArtworkSharingPlatformDbContext context)
        {
            if (await context.CommissionStatus.AnyAsync()) { return; }
            var commissionStatusList = new List<CommissionStatus>
            {
                new CommissionStatus {Description = "Pending"},
                new CommissionStatus {Description = "Accepted"},
                new CommissionStatus {Description = "Completed"},
                new CommissionStatus {Description = "Rejected"},
            };
            foreach (var commissionStatus in commissionStatusList)
            {
                await context.CommissionStatus.AddAsync(commissionStatus);
            }
            await context.SaveChangesAsync();
        }
        public static async Task SeedPackage(ArtworkSharingPlatformDbContext context)
        {
            if (await context.PackageBilling.AnyAsync())
            {
                return;
            }
            var packages = await File.ReadAllTextAsync("../ArtworkSharingPlatform.Infrastructure/PackageSeed.json");
            var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var packs = JsonSerializer.Deserialize<List<PackageBilling>>(packages, jsonOptions);
            foreach (var pack in packs)
            {
                await context.PackageBilling.AddAsync(pack);
            }
            await context.SaveChangesAsync();
        }


        public static async Task SeedTransaction(ArtworkSharingPlatformDbContext context)
        {
            if (await context.Transactions.AnyAsync())
            {
                return;
            }
            var transaction = await File.ReadAllTextAsync("../ArtworkSharingPlatform.Infrastructure/TransationSeed.json");
            var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var trans = JsonSerializer.Deserialize<List<Transaction>>(transaction, jsonOptions);
            foreach (var tran in trans)
            {
                await context.Transactions.AddAsync(tran);
            }
            await context.SaveChangesAsync();
        }

        public static async Task SeedPackageInformation(ArtworkSharingPlatformDbContext context)
        {
            if (await context.PackageInformation.AnyAsync()) { return; }
            var package = new List<PackageInformation>
            {
                new PackageInformation
                {
                    Name = "Basic",
                    Credit = 3,
                    Price = 50000,
                    Status = 1
                },
                new PackageInformation
                {
                    Name = "Advance",
                    Credit = 12,
                    Price = 100000,
                    Status = 1
                },
                new PackageInformation
                {
                    Name = "Super",
                    Credit = 25,
                    Price = 200000,
                    Status = 1
                }
            };
            foreach (var packageInformation in package)
            {
                await context.PackageInformation.AddAsync(packageInformation);
            }
            await context.SaveChangesAsync();
        }

        public static async Task SeedConfigManager(ArtworkSharingPlatformDbContext context)
        {
            if (await context.ConfigManagers.AnyAsync()) { return; }
            var configs = new List<ConfigManager>
            {
                new ConfigManager
                {
                ConfigDate = DateTime.Now.AddDays(-5),
                IsServicePackageConfig = true,
                IsPhysicalImageConfig = true,
                MaxReleaseCount = 10,
                IsGeneralConfig = true,
                LogoUrl = "",
                MyPhoneNumber = "1234567893",
                Address = "FPTU",
                IsPagingConfig = true,
                TotalItemPerPage = 10,
                RowSize = 5,
                IsAdvertisementConfig = true,
                CompanyName = "FPTU",
                CompanyPhoneNumber = "0999992123",
                CompanyEmail = "fptu@fpt.edu.vn",
                Administrator = context.Users.FirstOrDefault(u => u.Id == 11)
                },
                new ConfigManager
                {
                ConfigDate = DateTime.Now,
                IsServicePackageConfig = true,
                IsPhysicalImageConfig = true,
                MaxReleaseCount = 17,
                IsGeneralConfig = true,
                LogoUrl = "",
                MyPhoneNumber = "1321312312",
                Address = "NVHSV",
                IsPagingConfig = true,
                TotalItemPerPage = 10,
                RowSize = 5,
                IsAdvertisementConfig = true,
                CompanyName = "NVHSV",
                CompanyPhoneNumber = "0999992123",
                CompanyEmail = "fptu@fpt.edu.vn",
                Administrator = context.Users.FirstOrDefault(u => u.Id == 11)
                }
        };
            foreach (var config in configs)
            {
                await context.ConfigManagers.AddAsync(config);
            }

            await context.SaveChangesAsync();
        }

        public static async Task SeedCommissionRequest(ArtworkSharingPlatformDbContext context)
        {
            if (await context.CommissionRequests.AnyAsync()) { return; }

            var commissionRequest = await File.ReadAllTextAsync("../ArtworkSharingPlatform.Infrastructure/CommissionRequestSeed.json");
            var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var commissions = JsonSerializer.Deserialize<List<CommissionRequest>>(commissionRequest, jsonOptions);

            foreach (var com in commissions)
            {
                await context.CommissionRequests.AddAsync(com);
            }

            await context.SaveChangesAsync();
        }

        public static async Task SeedCommissionImage(ArtworkSharingPlatformDbContext context)
        {
            if (await context.CommissionImages.AnyAsync()) { return; }

            var commissionRequest = await File.ReadAllTextAsync("../ArtworkSharingPlatform.Infrastructure/CommissionImageSeed.json");
            var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var commissions = JsonSerializer.Deserialize<List<CommissionImage>>(commissionRequest, jsonOptions);

            foreach (var image in commissions)
            {
                await context.CommissionImages.AddAsync(image);
            }

            await context.SaveChangesAsync();
        }
        public static async Task SeedReport(ArtworkSharingPlatformDbContext context)
        {
            if (await context.Reports.AnyAsync())
            {
                return;
            }
            var report = await File.ReadAllTextAsync("../ArtworkSharingPlatform.Infrastructure/ReportSeed.json");
            var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var reports = JsonSerializer.Deserialize<List<Report>>(report, jsonOptions);
            foreach (var reoport in reports)
            {
                await context.Reports.AddAsync(reoport);
            }
            await context.SaveChangesAsync();
        }
    }
}
