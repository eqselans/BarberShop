using BarberShop.Data;
using BarberShop.Models;
using BarberShop.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Veritabaný baðlantýsýný yapýlandýr
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity hizmetlerini ekle
builder.Services.AddIdentity<User,Role>(options =>
{
    // Þifre politikalarýný yapýlandýrma
    options.Password.RequireDigit = false; // En az bir rakam gerektirir
    options.Password.RequiredLength = 3; 
    options.Password.RequireNonAlphanumeric = false; 
    options.Password.RequireUppercase = false; 
    options.Password.RequireLowercase = false;
    options.Password.RequiredUniqueChars = 2; 

    // Kullanýcý adý ve email politikalarý
    options.User.RequireUniqueEmail = true; // E-posta adresi benzersiz olmalýdýr
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Account/AccessDenied"; // Yetkisiz eriþim sayfasý
    options.LoginPath = "/Account/Login"; // Giriþ yapma sayfasý
    options.ReturnUrlParameter = string.Empty;
});


// Razor Pages ve MVC desteði
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IRegisterService, RegisterService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IAppointmentService, AppointmentService>();
builder.Services.AddTransient<IServiceService, ServiceService>();
builder.Services.AddTransient<IEmployeeService, EmployeeService>();
builder.Services.AddTransient<ITestimonialService, TestimonialService>();
builder.Services.AddHttpClient<IAIRecommendationService, AIRecommendationService>();



// CORS yapýlandýrmasý
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "BarberShop API",
        Version = "v1",
        Description = "BarberShop için REST API dokümantasyonu",
    });
});





var app = builder.Build();

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "BarberShop API v1");
        c.RoutePrefix = "swagger"; // Swagger'ý kök dizinde göster
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();

app.UseCors("AllowAll");

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication(); // Kimlik doðrulama
app.UseAuthorization();  // Yetkilendirme

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
