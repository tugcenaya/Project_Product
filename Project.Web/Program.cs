using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Service.Mapping;
using Project.Web;
using Project.Web.Services;
using Project.Web.Hubs;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddSignalR();
builder.Services.AddAuthorization();

builder.Services.AddAutoMapper(typeof(MapProfile));
builder.Services.AddDbContext<ProjectDbContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("MsSQLConnection")));

builder.Host.UseServiceProviderFactory
    (new AutofacServiceProviderFactory());

builder.Services.AddControllers();

var app = builder.Build();

app.UseExceptionHandler("/Home/Error");
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();
app.MapHub<SignalServer>("/SignalServer");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();