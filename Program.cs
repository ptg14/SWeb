using CloudinaryDotNet;
using dotenv.net;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using CloudinaryDotNet.Actions;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using SWeb;
using SWeb.Models;
using SWeb.Repositories;
using SWeb.Services;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAllOrigins",
        configurePolicy: policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
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
