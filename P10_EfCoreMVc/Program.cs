using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Interfaces;
using WebApplication1.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<SqlDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("main")));



builder.Services.AddSingleton<ITokenService, TokenService>();
builder.Services.AddSingleton<IMailService, EmailService>();
builder.Services.AddScoped<ICloudinaryService, CloudinaryService>();




var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();

}




app.UseHttpsRedirection();
app.UseStaticFiles();
// app.UseSession();

app.UseRouting();


app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
