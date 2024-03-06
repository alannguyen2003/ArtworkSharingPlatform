using ArtworkSharingPlatform.Domain.Entities.Artworks;
using ArtworkSharingPlatform.Domain.Entities.Users;
using ArtworkSharingPlatform.Domain.Migrations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ArtworkSharingPlatform.Infrastructure
{
    public class Seed
    {

        /*public static async Task SeedGenre(ArtworkSharingPlatformDbContext context)
        {
            
        }*/
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
                EmailConfirmed = true
            };

            var resultAdmin = await userManager.CreateAsync(admin, "Pa$$w0rd");
            await userManager.AddToRoleAsync(admin, "Admin");

            var manager = new User
            {
                UserName = "manager@gmail.com",
                Name = "manager",
                Email = "manager@gmail.com",
                Status = 1,
                EmailConfirmed = true
            };
            await userManager.CreateAsync(manager, "Pa$$w0rd");
            await userManager.AddToRoleAsync(manager, "Manager");

            var artist = new User
            {
                UserName = "picasso@gmail.com",
                Name = "Pablo Picasso",
                Email = "picasso@gmail.com",
                PhoneNumber = "0123456789",
                Status = 1,
                EmailConfirmed = true
            };
            await userManager.CreateAsync(artist, "Pa$$w0rd");
            await userManager.AddToRoleAsync(artist, "Artist");
        }
    }
}
