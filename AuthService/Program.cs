using AuthService.Data;
using AuthService.Handlers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));

var app = builder.Build(); 

app.ConfigureEndpoints();
app.UseDefaultFiles();
app.UseStaticFiles();

app.Run();