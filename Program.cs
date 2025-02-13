using Microsoft.EntityFrameworkCore;
using MyAuto.Services;

var builder = WebApplication.CreateBuilder(args);
var connString = builder.Configuration.GetConnectionString("CarDbConnection");

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<ICARRepository, SqlCarRepository>();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(connString, x => x.MigrationsAssembly("MyAuto")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseStaticFiles();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();

app.Run();
