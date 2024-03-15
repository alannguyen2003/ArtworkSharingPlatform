using ArtworkSharingPlatform.Domain.Entities.Artworks;
using ArtworkSharingPlatform.Domain.Entities.Users;
using ArtworkSharingPlatform.Domain.Migrations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using ArtworkSharingPlatform.Domain.Entities.Commissions;
using ArtworkSharingPlatform.Domain.Entities.PackagesInfo;

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
                new Genre {Name = "Anime"},
                new Genre {Name = "Fiction"}
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
            await userManager.AddToRolesAsync(admin, new[] {"Admin", "Artist", "Audience"});

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
            await userManager.AddToRolesAsync(artist, new[] {"Artist", "Audience" });
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

            /*if (await context.PackageInformation.AnyAsync())
            {
                return;
            }
            //Status Package: "Active", "Inactive", "Draft", "Deleted", "Modifying"
            var packInfos = new List<PackageInformation>
            {
                new PackageInformation
                {
                    Credit = 100,
                    Price = 100,
                    Status = 1,
                    ConfigManagers = null,
                    PackageBillings = null
                },
                new PackageInformation
                {
                    Credit = 100,
                    Price = 100,
                    Status = 0,
                    ConfigManagers = null,
                    PackageBillings = null
                }
            };
            foreach (var packInfo in packInfos)
            {
                await context.PackageInformation.AddAsync(packInfo);
            }*/


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
            if (await context.Transactions.AnyAsync())
            {
                return;
            }
            var transaction = await File.ReadAllTextAsync("../ArtworkSharingPlatform.Infrastructure/TransationSeed.json");
            var _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var trans = JsonSerializer.Deserialize<List<PackageBilling>>(transaction, jsonOptions);
            foreach (var tran in trans)
            {
                await context.PackageBilling.AddAsync(tran);
            }
        }

        public static async Task SeedPackageInformation(ArtworkSharingPlatformDbContext context)
        {
            if (await context.PackageInformation.AnyAsync()) { return; }
            var package = new List<PackageInformation>
            {
                new PackageInformation
                {
                    Name = "Basic",
                    Credit = 5,
                    Price = 20000,
                    Status = 1
                },
                new PackageInformation
                {
                    Name = "Advance",
                    Credit = 15,
                    Price = 60000,
                    Status = 1
                },
                new PackageInformation
                {
                    Name = "Super",
                    Credit = 25,
                    Price = 100000,
                    Status = 1
                }
            };
            foreach(var packageInformation in package)
            {
                await context.PackageInformation.AddAsync(packageInformation);
            }
            await context.SaveChangesAsync();
        }
    }
}
