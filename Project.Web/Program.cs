using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Service.Mapping;
using Project.Web;
using Project.Web.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddAuthorization();

// Add services to the container.
builder.Services.AddAutoMapper(typeof(MapProfile));
builder.Services.AddDbContext<ProjectDbContext>(x =>
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("MsSQLConnection"), option =>
    {
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(ProjectDbContext)).GetName().Name);
    });
});

builder.Services.AddHttpClient<ProductApiService>(opt =>
{
    opt.BaseAddress = new Uri(builder.Configuration["BaseUrl"]);

});
builder.Services.AddHttpClient<CategoryApiService>(opt =>
{
    opt.BaseAddress = new Uri(builder.Configuration["BaseUrl"]);

});

builder.Host.UseServiceProviderFactory
    (new AutofacServiceProviderFactory());

builder.Services.AddAuthorization();
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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();