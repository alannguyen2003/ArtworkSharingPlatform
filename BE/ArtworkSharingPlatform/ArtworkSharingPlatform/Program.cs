using ArtworkSharingHost.Middleware;
using ArtworkSharingPlatform.Application.Interfaces;
using ArtworkSharingPlatform.Application.Services;
using ArtworkSharingPlatform.Domain.Entities.Users;
using ArtworkSharingPlatform.Domain.Migrations;
using ArtworkSharingPlatform.Infrastructure;
using ArtworkSharingPlatform.Infrastructure.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

const string artworkSharingPlatformCors = "_artworkSharingPlatformCors";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ArtworkSharingPlatformDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddDependencyInjection();
builder.Services.AddIdentityCore<User>(opt =>
{
    opt.Password.RequiredLength = 5;

    opt.User.RequireUniqueEmail = true;
})
    .AddRoles<Role>()
    .AddRoleManager<RoleManager<Role>>()
    .AddEntityFrameworkStores<ArtworkSharingPlatformDbContext>()
    .AddSignInManager<SignInManager<User>>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            RequireExpirationTime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ValidateIssuerSigningKey = true
        };
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: artworkSharingPlatformCors,
        policy =>
        {
            policy.WithOrigins("*")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.AddScoped<IAuthService, AuthService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(artworkSharingPlatformCors);
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<ArtworkSharingPlatformDbContext>();
    var userManager = services.GetRequiredService<UserManager<User>>();
    var roleManager = services.GetRequiredService<RoleManager<Role>>();
    await context.Database.MigrateAsync();
    await Seed.SeedUser(userManager, roleManager);
    await Seed.SeedArtwork(context);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error while seeding data");
}

app.Run();