
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Supplier_SRM_MVC.Entities;
using Supplier_SRM_MVC.Interfaces;
using Supplier_SRM_MVC.Services;
using Supplier_SRM_MVC.Utilities;

namespace Supplier_SRM_MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddIdentity<AppUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationDbContext>();


            builder.Services.AddControllersWithViews();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IManageService, ManageService>();
            builder.Services.AddScoped<ISupplierService, SupplierService>();
            builder.Services.AddScoped<IContractService, ContractService>();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
            });


            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                InitializeRoles(scope.ServiceProvider).Wait();
            }


            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }

        public static string[] roles = { Roles.Admin, Roles.User, Roles.Supplier };

        public static async Task InitializeRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            // Определите роли, которые хотите создать

            foreach (var role in roles)
            {
                // Проверьте, существует ли уже роль
                if (!await roleManager.RoleExistsAsync(role))
                {
                    // Создайте роль
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
