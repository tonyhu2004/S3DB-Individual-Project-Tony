using CloudinaryAccess.Repositories;
using CloudinaryDotNet;
using Core.Interfaces;
using Core.Models;
using Core.Services;
using DataAccess.Data;
using DataAccess.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using S3DB_Individual_Project_Tony;
using S3DB_Individual_Project_Tony.CustomFilter;
using S3DB_Individual_Project_Tony.Hub;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("ShopHopConnection")
                       ?? throw new InvalidOperationException("Connection string ShopHopConnection not found.");

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseMySQL(connectionString));

// Add services to the container.
builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
    .AddRoles<IdentityRole>()
    .AddRoleManager<RoleManager<IdentityRole>>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllers();

// var cloudinaryOptions = builder.Configuration.GetSection("Cloudinary").Get<CloudinaryOptions>()
//                         ?? throw new InvalidOperationException("Cloudinary settings not found.");
//
// builder.Services.AddSingleton(new Cloudinary(new Account(
//     cloudinaryOptions!.CloudName,
//     cloudinaryOptions.ApiKey,
//     cloudinaryOptions.ApiSecret)));
var cloudinaryOptions = new CloudinaryOptions
{
    CloudName = "dxkq4oonm",
    ApiKey = "811531388986798",
    ApiSecret = "fo4F8ZwgYAXumWyZDZy1iY1qfwk"
};
builder.Services.AddSingleton(new Cloudinary(new Account(
    cloudinaryOptions!.CloudName,
    cloudinaryOptions.ApiKey,
    cloudinaryOptions.ApiSecret)));

builder.Services.AddScoped<ICloudinaryRepository, CloudinaryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<IChatRepository, ChatRepository>();
builder.Services.AddScoped<ChatService>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<RoleService>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<ReviewService>();

builder.Services.AddScoped<CustomExceptionFilter>();

builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policyBuilder =>
        policyBuilder.WithOrigins("http://localhost:5173")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

var app = builder.Build();

app.MapGroup("/User").MapIdentityApi<ApplicationUser>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigin");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapHub<ChatHub>("/chatHub");

app.Run();