using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Supplier_SRM_MVC.Entities
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<AppUser> AppUsers { get; set; } = null!;
        public DbSet<Admin> Admins { get; set; } = null!;
        public DbSet<Supplier> Suppliers { get; set; } = null!;
        public DbSet<HistorySubmissionsForSupplier> HistorySubmissions { get; set; } = null!;
        public DbSet<Contract> Contracts { get; set; } = null!;
        public DbSet<FileData> FilesData { get; set; } = null!;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
