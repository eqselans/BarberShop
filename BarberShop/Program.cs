using BarberShop.Data;
using BarberShop.Models;
using BarberShop.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Veritabanı bağlantısını yapılandır
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity hizmetlerini ekle
builder.Services.AddIdentity<User,Role>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

// Razor Pages ve MVC desteği
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IRegisterService, RegisterService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IAppointmentService, AppointmentService>();
builder.Services.AddTransient<IServiceService, ServiceService>();
builder.Services.AddTransient<IEmployeeService, EmployeeService>();


builder.Services.AddSingleton<HairstyleApiService>();



// CORS yapılandırması
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("https://localhost:44348") // API'nin erişimine izin vereceğiniz kaynak
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors("AllowSpecificOrigin");

app.UseAuthentication(); // Kimlik doğrulama
app.UseAuthorization();  // Yetkilendirme

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
