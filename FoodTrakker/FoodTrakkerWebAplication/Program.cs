using FoodTrakker.Core.Model;
using FoodTrakker.Repository;
using FoodTrakker.Repository.Data;
using FoodTrakker.Services;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<FoodTruckService>();
builder.Services.AddScoped<EventService>();

var option = builder.Configuration.GetConnectionString("FoodTrakkerDb");
builder.Services.AddDbContext<FoodTrakkerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FoodTrakkerDb")));


builder.Services.AddTransient<IRepository<User>, UserRepository>();
builder.Services.AddTransient<IRepository<Event>, EventRepository>();
builder.Services.AddTransient<IRepository<FoodTruck>, FoodTruckRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
