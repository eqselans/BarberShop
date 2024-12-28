using BarberShop.Data;
using BarberShop.Models;
using BarberShop.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

var builder = WebApplication.CreateBuilder(args);

// Veritaban� ba�lant�s�n� yap�land�r
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Identity hizmetlerini ekle
builder.Services.AddIdentity<User,Role>(options =>
{
    // �ifre politikalar�n� yap�land�rma
    options.Password.RequireDigit = false; // En az bir rakam gerektirir
    options.Password.RequiredLength = 3; 
    options.Password.RequireNonAlphanumeric = false; 
    options.Password.RequireUppercase = false; 
    options.Password.RequireLowercase = false;
    options.Password.RequiredUniqueChars = 2; 

    // Kullan�c� ad� ve email politikalar�
    options.User.RequireUniqueEmail = true; // E-posta adresi benzersiz olmal�d�r
})
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.AccessDeniedPath = "/Account/AccessDenied"; // Yetkisiz eri�im sayfas�
    options.LoginPath = "/Account/Login"; // Giri� yapma sayfas�
    options.ReturnUrlParameter = string.Empty;
});


// Razor Pages ve MVC deste�i
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IRegisterService, RegisterService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IAppointmentService, AppointmentService>();
builder.Services.AddTransient<IServiceService, ServiceService>();
builder.Services.AddTransient<IEmployeeService, EmployeeService>();
builder.Services.AddTransient<ITestimonialService, TestimonialService>();
builder.Services.AddHttpClient<IAIRecommendationService, AIRecommendationService>();



// CORS yap�land�rmas�
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
        Description = "BarberShop i�in REST API dok�mantasyonu",
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
        c.RoutePrefix = "swagger"; // Swagger'� k�k dizinde g�ster
    });
}

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRouting();

app.UseCors("AllowAll");

app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication(); // Kimlik do�rulama
app.UseAuthorization();  // Yetkilendirme

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
