using Microsoft.EntityFrameworkCore;


namespace CommonLibrary.MODEL
{
    public class ProductDbContext : DbContext
    {
        public DbSet<Category> categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> products { get; set; }

        // Bejelentkezés - regisztráció
        public DbSet<User> Users { get; set; }
        public DbSet<LoginLog> LoginLogs { get; set; }
        public DbSet<Page> Pages { get; set; }
        public DbSet<UserAccess> UserAccesses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS; Database=Cukraszda; User=sa; Password=1A2w3e4F; TrustServerCertificate=True;");
        }
    }
}
