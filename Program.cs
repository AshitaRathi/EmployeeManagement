using EmployeeManagement;
using EmployeeManagement.Data;
using EmployeeManagement.Models;
using EmployeeManagement.Repository;
using EmployeeManagement.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;


public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configure services
        ConfigureServices(builder);

        var app = builder.Build();

        ConfigureMiddleware(app);

        // Run the application
        app.Run();

    }

    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        // Configure database connection
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        builder.Services.AddDbContext<EmployeeManagementDbContext>(options =>
            options.UseSqlServer(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();
        
        // Configure Identity with cookie-based authentication
        builder.Services.AddDefaultIdentity<IdentityUser>(options =>
        {
            options.SignIn.RequireConfirmedAccount = true;
        })
        .AddEntityFrameworkStores<EmployeeManagementDbContext>();

        // Register repositories and services
        RegisterRepositoriesAndServices(builder.Services);

        // Configure JWT Authentication
        ConfigureJwtAuthentication(builder.Services);

        // Configure session
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSession(options =>
        {
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
            options.IdleTimeout = TimeSpan.FromMinutes(30); // Adjust as needed
            options.Cookie.SameSite = SameSiteMode.None;  // This is required for cross-origin cookies
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;  // If using HTTPS
        });

        // Add MVC and Razor Pages support
        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages();

        // Configure authorization
        builder.Services.AddAuthorization(options =>
        {
            options.FallbackPolicy = options.DefaultPolicy;
        });
        builder.Services.AddHttpClient();
    }

    public static void ConfigureMiddleware(WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseStaticFiles();
        app.UseRouting();
        app.UseSession();
        app.Use(async (context, next) =>
        {
            var token = context.Session.GetString("EmployeeToken");

            if (!string.IsNullOrEmpty(token))
            {
                context.Request.Headers["Authorization"] = $"Bearer {token}";
            }

            await next.Invoke();
        });

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}"
        );

        app.MapRazorPages();

    }

    public static void RegisterRepositoriesAndServices(IServiceCollection services)
    {
        // Register Employee-related services
        services.AddScoped<IRepository<Employee>, Repository<Employee>>();
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IEmployeeService, EmployeeService>();

        // Register Team-related services
        services.AddScoped<IRepository<Team>, Repository<Team>>();
        services.AddScoped<ITeamRepository, TeamRepository>();
        services.AddScoped<ITeamService, TeamService>();

        // Register Job-related services
        services.AddScoped<IRepository<Job>, Repository<Job>>();
        services.AddScoped<IJobRepository, JobRepository>();
        services.AddScoped<IJobService, JobService>();

        // Register Token Service
        services.AddScoped<ITokenService, TokenService>();

    }

    public static void ConfigureJwtAuthentication(IServiceCollection services)
    {
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
         .AddJwtBearer(options =>
         {
             options.RequireHttpsMetadata = true;
             options.SaveToken = true;
             options.TokenValidationParameters = new TokenValidationParameters
             {
                 ValidateIssuer = true,
                 ValidateAudience = true,
                 ValidateLifetime = true,
                 ValidIssuer = "https://localhost:7099",
                 ValidAudience = "https://localhost:7099",
                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("YourSuperSecureKeyWith32Characters"))
             };
         });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("EmployeePolicy", policy => policy.RequireClaim("Employee"));
        });
    }

}
