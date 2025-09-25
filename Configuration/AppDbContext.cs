using BTL_QuanLiBanSach.Entities;
using Microsoft.EntityFrameworkCore;

namespace BTL_QuanLiBanSach.Configuration;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Author> Authors { get; set; }
    public virtual DbSet<Publisher> Publishers { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<OrderDetail> OrderDetails { get; set; }
    public virtual DbSet<Account> Accounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .HasMany(p => p.Categories)
            .WithMany(c => c.Products)
            .UsingEntity(j=>j.ToTable("tbl_product_category"));
        modelBuilder.Entity<Product>()
            .HasMany(p => p.Authors)
            .WithMany(a => a.Products)
            .UsingEntity(j=>j.ToTable("tbl_product_author"));
    }
}