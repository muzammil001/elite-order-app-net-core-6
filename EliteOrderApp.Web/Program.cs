using EliteOrderApp.Database;
using EliteOrderApp.Service;
using EliteOrderApp.Web.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Reflection;
using DevExpress.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<ItemService>();
builder.Services.AddTransient<CustomerService>();
builder.Services.AddTransient<CartService>();
builder.Services.AddTransient<OrderService>();
builder.Services.AddTransient<PaymentService>();
builder.Services.AddTransient<ReportService>();
builder.Services.AddTransient<DbInitializer>();
builder.Services.AddScoped(_ =>
    new BackupService(builder.Configuration.GetConnectionString("DefaultConnection"),
        $@"{Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "backups")}"));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddMvc();
builder.Services.AddDevExpressControls();



var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseDevExpressControls();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
var initializer = services.GetRequiredService<DbInitializer>();
initializer.Run();


app.Run();
