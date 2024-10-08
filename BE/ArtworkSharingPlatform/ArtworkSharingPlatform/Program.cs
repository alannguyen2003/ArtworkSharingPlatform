using ArtworkSharingHost.CloudinaryService;
using ArtworkSharingHost.EmailService;
using ArtworkSharingHost.EmailService.Settings;
using ArtworkSharingHost.Middleware;
using ArtworkSharingHost.SignalR;
using ArtworkSharingPlatform.Application.Interfaces;
using ArtworkSharingPlatform.Application.Services;
using ArtworkSharingPlatform.Domain.Entities.Users;
using ArtworkSharingPlatform.Domain.Helpers;
using ArtworkSharingPlatform.Domain.Migrations;
using ArtworkSharingPlatform.Infrastructure;
using ArtworkSharingPlatform.Infrastructure.Configuration;
using ArtworkSharingPlatform.Repository.Interfaces;
using ArtworkSharingPlatform.Repository.Repository;
using ArtworkSharingPlatform.Repository.Repository.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ArtworkSharingHost.StripePaymentService;
using ArtworkSharingHost.StripePaymentService.Interfaces;
using Stripe;
using ArtworkSharingHost.StripePaymentService.Settings;

//const string artworkSharingPlatformCors = "_artworkSharingPlatformCors";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ArtworkSharingPlatformDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddSingleton<PresenceTracker>();
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

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateActor = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        RequireExpirationTime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration.GetSection("Jwt:Issuer").Value,
        ValidAudience = builder.Configuration.GetSection("Jwt:Audience").Value,
        IssuerSigningKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value))
    };
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];
            var path = context.HttpContext.Request.Path;
            if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hub"))
            {
                context.Token = accessToken;
            }
            return Task.CompletedTask;
        }
    };
});
builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Admin"));
	options.AddPolicy("RequireManagerRole", policy => policy.RequireRole("Manager"));
	options.AddPolicy("RequireArtistRole", policy => policy.RequireRole("Artist"));
	options.AddPolicy("RequireAdminManagerRole", policy =>
		policy.RequireAssertion(context =>
			context.User.IsInRole("Admin") || context.User.IsInRole("Manager")));
});
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(name: artworkSharingPlatformCors,
//        policy =>
//        {
//            policy.AllowAnyHeader()
//            .AllowAnyMethod()
//            .AllowCredentials()
//            .WithOrigins("https://localhost:4200");
//        });
//});
builder.Services.AddSignalR();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.Configure<SendInBlue>(builder.Configuration.GetSection("SendInBlue"));
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("Stripe"));

builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
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

StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();
app.UseHttpsRedirection();
app.UseCors(builder => builder
.AllowAnyHeader()
.AllowAnyMethod()
.AllowCredentials()
.WithOrigins("http://localhost:4200"));
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHub<PresenceHub>("hub/presence");
app.MapHub<MessageHub>("hub/message");

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<ArtworkSharingPlatformDbContext>();
    var userManager = services.GetRequiredService<UserManager<User>>();
    var roleManager = services.GetRequiredService<RoleManager<Role>>();
    await context.Database.MigrateAsync();
    await context.Database.ExecuteSqlRawAsync("DELETE FROM Connections");
    await Seed.SeedUser(userManager, roleManager);
    await Seed.SeedArtwork(context);
    await Seed.SeedCommissionStatus(context);
    await Seed.SeedCommissionRequest(context);
    await Seed.SeedCommissionImage(context);
    await Seed.SeedPackageInformation(context);
    await Seed.SeedTransaction(context);
    await Seed.SeedPackage(context);
    await Seed.SeedConfigManager(context);
    await Seed.SeedReport(context);

}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error while seeding data");
}

app.Run();