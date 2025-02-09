using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using StoreApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Data
{
    public class AppDbContext: DbContext
    {
        private readonly IConfiguration _configuration;

        public AppDbContext(DbContextOptions<AppDbContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public DbSet<ProductModel> Products { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<StockModel> Stocks { get; set; }
        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<OrderItemModel> OrderItems { get; set; }
        public DbSet<CustomerModel> Customers { get; set; }
        public DbSet<StatusModel> Statuses { get; set; }
        public DbSet<ProductCategoryModel> ProductCategories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration["ConnectionStrings:DefaultConnection"], options =>
            {
                options.EnableRetryOnFailure(
                maxRetryCount: 5, // Número máximo de intentos
                maxRetryDelay: TimeSpan.FromSeconds(10), // Tiempo máximo entre intentos
                errorNumbersToAdd: null);
            });
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategoryModel>().ToTable("ProductCategories");
            modelBuilder.Entity<ProductModel>().ToTable("Products");
            modelBuilder.Entity<OrderModel>().ToTable("Orders");
            modelBuilder.Entity<OrderItemModel>().ToTable("OrderItems");
            modelBuilder.Entity<CustomerModel>().ToTable("Customers");
            modelBuilder.Entity<StatusModel>().ToTable("Statuses");
            modelBuilder.Entity<StockModel>().ToTable("Stock");
            modelBuilder.Entity<CategoryModel>().ToTable("Categories");

            // ProductCategoryModel: Composite Key
            modelBuilder.Entity<ProductCategoryModel>()
                .HasIndex(pc => new { pc.ProductId, pc.CategoryId })
                .IsUnique();

            // ProductModel: Unique Name
            modelBuilder.Entity<ProductModel>()
                .HasIndex(p => p.Name)
                .IsUnique();

            // StockModel: Composite Key
            modelBuilder.Entity<StockModel>()
                .HasIndex(s => new { s.ProductId, s.Id })
                .IsUnique();

            // CategoryModel: Unique Name
            modelBuilder.Entity<CategoryModel>()
                .HasIndex(c => c.Name)
                .IsUnique();

            // StatusModel: Unique Name
            modelBuilder.Entity<StatusModel>()
                .HasIndex(s => s.Name)
                .IsUnique();

            // CustomerModel: Unique Email
            modelBuilder.Entity<CustomerModel>()
                .HasIndex(c => c.Email)
                .IsUnique();

            // Relationships

            // ProductCategoryModel -> ProductModel
            modelBuilder.Entity<ProductCategoryModel>()
                .HasOne(pc => pc.Product)
                .WithMany(p => p.ProductCategories)
                .HasForeignKey(pc => pc.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // ProductCategoryModel -> CategoryModel
            modelBuilder.Entity<ProductCategoryModel>()
                .HasOne(pc => pc.Category)
                .WithMany(c => c.ProductCategories)
                .HasForeignKey(pc => pc.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // OrderItemModel -> ProductModel
            modelBuilder.Entity<OrderItemModel>()
                .HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            // OrderItemModel -> OrderModel
            modelBuilder.Entity<OrderItemModel>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            // OrderModel -> CustomerModel
            modelBuilder.Entity<OrderModel>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            // OrderModel -> StatusModel
            modelBuilder.Entity<OrderModel>()
                .HasOne(o => o.Status)
                .WithMany(s => s.Orders)
                .HasForeignKey(o => o.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            // StockModel -> ProductModel
            modelBuilder.Entity<StockModel>()
                .HasOne(s => s.Product)
                .WithOne(p => p.Stock)
                .HasForeignKey<StockModel>(s => s.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }

    }
}
