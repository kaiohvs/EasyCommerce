using EasyCommerce.Autentication;
using EasyCommerce.Data;
using EasyCommerce.Interfaces;
using EasyCommerce.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configuração do DbContext com SQL Server
        builder.Services.AddDbContext<BD_Context>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        // Registro de Serviços Genéricos
        builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));

        // Configuração do Identity
        builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {            
            options.SignIn.RequireConfirmedAccount = false;
        })
            .AddEntityFrameworkStores<BD_Context>()
            .AddDefaultTokenProviders();

        builder.Services.Configure<IdentityOptions>(options =>
        {
            options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier;
            options.ClaimsIdentity.UserNameClaimType = ClaimTypes.Name;
            options.ClaimsIdentity.EmailClaimType = ClaimTypes.Email;
        });

        builder.Services.AddScoped<SignInManager<ApplicationUser>, CustomSignInManager>();


        // Configuração do ApplicationUserClaimsPrincipalFactory
        builder.Services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ApplicationUserClaimsPrincipalFactory>();

        builder.Services.AddAuthorization();
        builder.Services.AddControllers();
        builder.Services.AddRazorPages();

        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.LoginPath = "/Identity/Account/Login";
            options.LogoutPath = "/Identity/Account/Logout";
            options.AccessDeniedPath = "/Identity/Account/AccessDenied";
        });


        // Configuração do serviço SeedData
        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
                var context = services.GetRequiredService<BD_Context>();
                await SeedData.Initialize(services, userManager, roleManager);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao criar usuários padrão: " + ex.Message);
            }
        }



        // Configuração do Pipeline de Requisições HTTP
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        // Autenticação e Autorização
        app.UseAuthentication();
        app.UseAuthorization();     

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.MapRazorPages();

        app.Run();
    }
}