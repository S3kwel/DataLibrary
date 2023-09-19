using DATA.Repository.Configuration;
using Microsoft.EntityFrameworkCore;
using Spike;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

IConfiguration configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory()) // Set the path to your configuration file
    .AddJsonFile("appsettings.json") // Load the appsettings.json file
    .Build();

builder.Services.AddData<SpikeContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("default"));
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
