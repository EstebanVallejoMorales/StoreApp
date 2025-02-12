
using Microsoft.EntityFrameworkCore;
using StoreApp.Data;
using StoreApp.Entities;
using StoreApp.Mappers.Profiles;
using StoreApp.Presenters.Presenters;
using StoreApp.Presenters.ViewModels;
using StoreApp.Repositories;
using StoreApp.UseCases.Catalog;
using StoreApp.UseCases.Interfaces;

namespace StoreApp.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration["ConnectionStrings:DefaultConnection"];
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));
            builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

            // Dependency injection

            // Repositories
            builder.Services.AddScoped<IRepository<Category>, CategoryRepository>();
            builder.Services.AddScoped<IRepository<Customer>, CustomerRepository>();
            builder.Services.AddScoped<IRepository<Order>, OrderRepository>();
            builder.Services.AddScoped<IRepository<OrderItem>, OrderItemRepository>();
            builder.Services.AddScoped<IRepository<ProductCategory>, ProductCategoryRepository>();
            builder.Services.AddScoped<IRepository<Product>, ProductRepository>();
            builder.Services.AddScoped<IRepository<Status>, StatusRepository>();
            builder.Services.AddScoped<IRepository<Stock>, StockRepository>();

            // Use Cases
            builder.Services.AddScoped<GetAllProductsUseCase<Product, ProductViewModel>>();

            // Presenters
            builder.Services.AddScoped<IPresenter<Product, ProductViewModel>, ProductPresenter>();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: "_corsConfiguration",
                                  builder =>
                                  {
                                      builder
                                             .AllowAnyOrigin()
                                             .AllowAnyMethod()
                                             .AllowAnyHeader();
                                  });
            });

            builder.Services.AddAutoMapper(typeof(MappingProfile));

            // Add services to the container.

            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Automatically apply migrations on startup
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                dbContext.Database.Migrate();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("_corsConfiguration");

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
