using BlazorApp2.Components;
using BlazorApp2.Data;
using BlazorApp2.Services;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var groqApiKey = builder.Configuration["Groq:ApiKey"];
//
// =====================
// Services
// =====================

// Blazor Server
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Authorization
builder.Services.AddAuthorization();

// HttpClient
builder.Services.AddHttpClient();

//
// =====================
// Application Services
// =====================
builder.Services.AddScoped<ExpiryDiscountService>();
builder.Services.AddScoped<RecipeService>();
builder.Services.AddScoped<ProductDbService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddSingleton<OrderService>();
builder.Services.AddSingleton<CartService>();
builder.Services.AddScoped<EmailService>();

// Add Controllers (for AuthController)
builder.Services.AddControllers();

//
// =====================
// Entity Framework + Identity
// =====================

// Identity DB (SQLite)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=Data/grocery.db"));

// Identity configuration
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true; // enforce unique emails
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

//
// =====================
// Authentication (Cookies + Google)
// =====================
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie(options =>
{
    options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
    options.Cookie.SameSite = SameSiteMode.Lax;
})
.AddGoogle(options =>
{
    options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
    // default callback: /signin-google
});

var app = builder.Build();

//
// =====================
// Database Initialization (Products)
// =====================
DatabaseInitializer.Initialize();

//
// =====================
// Middleware Pipeline
// =====================
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// IMPORTANT ORDER
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

//
// =====================
// Map Controllers
// =====================
app.MapControllers(); // <-- THIS LINE IS CRUCIAL for API routes like /api/auth/login

//
// =====================
// Razor Components
// =====================
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

//
// =====================
// Google Login Endpoint
// =====================
app.MapGet("/login/google", async (HttpContext context) =>
{
    await context.ChallengeAsync(
        GoogleDefaults.AuthenticationScheme,
        new AuthenticationProperties
        {
            RedirectUri = "/home"
        });
});

//
// =====================
// Test Endpoint
// =====================
app.MapGet("/test", () => "App is running");

app.Run();
