using ArtworkSharingPlatform.Domain.Entities.Artworks;
using ArtworkSharingPlatform.Domain.Migrations;
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
        public static async Task SeedArtwork(ArtworkSharingPlatformDbContext context)
        {
            if (await context.Artworks.AnyAsync())
            {
                return;
            }
            var artworks = await File.ReadAllTextAsync("../Infrastructure/ArtworkSharingPlatform.Infrastructure/ArtworkSeed.json");
            var jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var arts = JsonSerializer.Deserialize<List<Artwork>>(artworks, jsonOptions);
            foreach (var art in arts)
            {
                await context.Artworks.AddAsync(art);
            }
            await context.SaveChangesAsync();
        }
    }
}
