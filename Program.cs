using CloudinaryDotNet;
using dotenv.net;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using CloudinaryDotNet.Actions;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using SWeb;
using SWeb.Models;
using SWeb.Repositories;
using SWeb.Services;

var builder = WebApplication.CreateBuilder(args);

// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddFile(builder.Configuration.GetSection("Logging:File"));

// Add services to the container.
builder.Services.AddRazorPages();

// Set your Cloudinary credentials
//=================================

DotEnv.Load(options: new DotEnvOptions(probeForEnv: true));

builder.Services.AddSingleton(provider =>
{
    var cloudinaryUrl = Environment.GetEnvironmentVariable("CLOUDINARY_URL");
    var cloudinary = new Cloudinary(cloudinaryUrl);
    cloudinary.Api.Secure = true;
    return cloudinary;
});

//=================================

string? hostIpAddress = Dns.GetHostAddresses(Dns.GetHostName())
                          .FirstOrDefault(ip => ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                          ?.ToString();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins($"http://{hostIpAddress}:9090") // dynamically set server IP
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

builder.Services.AddDbContext<SocialMediaContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")),
    ServiceLifetime.Scoped);

builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<PostRepository>();

builder.Services.AddScoped<FollowingManagementService>();
builder.Services.AddScoped<PostService>();
builder.Services.AddScoped<UserService>();

builder.Services.AddScoped<ImageService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    c.ExampleFilters();
});
builder.Services.AddSwaggerExamplesFromAssemblyOf<Program>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    var context = services.GetRequiredService<SocialMediaContext>();

    try
    {
        var connectionString = context.Database.GetConnectionString();
        var databaseProvider = context.Database.ProviderName;

        logger.LogInformation("Database connection successful.");
        logger.LogInformation($"Database Provider: {databaseProvider}");
        logger.LogInformation($"Connection String: {connectionString}");

        context.Database.OpenConnection();
        context.Database.CloseConnection();
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while connecting to the database.");
    }
}

app.UseCors("AllowAngularApp");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapRazorPages();

app.MapControllers();

app.UseCors("AllowAllOrigins");

app.Run();
