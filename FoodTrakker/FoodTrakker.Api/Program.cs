
using FoodTrakker.Core.Model;
using FoodTrakker.Repository;
using FoodTrakker.Repository.Contracts;
using FoodTrakker.Repository.Data;
using FoodTrakker.Services;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("FoodTrakker.Api.IntegrationTests")]

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<EventService>();
builder.Services.AddScoped<IFoodTruckRepository, FoodTruckRepository>();
builder.Services.AddScoped<FoodTruckService>();

builder.Services.AddScoped<IRepository<User, string>, UserRepository>();
builder.Services.AddScoped<IRepository<FoodTruckType, int>, Repository<FoodTruckType, int>>();
builder.Services.AddScoped<IRepository<Review, int>, Repository<Review, int>>();

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHttpContextAccessor();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<FoodTrakkerContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("FoodTrakkerDb"),
    builder =>
    {
        builder.EnableRetryOnFailure(2, TimeSpan.FromSeconds(5), null);
    });
});
    // options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
