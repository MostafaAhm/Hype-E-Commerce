using Microsoft.Extensions.Logging;
using Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastructure.Persistence
{
    public class AppContextSeed
    {
        public static async Task SeedAsync(AppDbContext context, ILogger<AppContextSeed> logger)
        {
            if (!context.ProductTypes.Any())
            {
                await context.AddRangeAsync(GetProductTypesToSeed());
                await context.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(AppDbContext).Name);
            }

            if(!context.Products.Any())
            {
                await context.AddRangeAsync(GetProductsToSeed());
                await context.SaveChangesAsync();
                logger.LogInformation("Seed database associated with context {DbContextName}", typeof(AppDbContext).Name);
            }

        }
        

        private static IEnumerable<ProductType> GetProductTypesToSeed()
        {
            return new List<ProductType>
            {
                new ProductType
                {
                    Id = Guid.Parse("E4D97379-E577-4AAB-8811-4E84ACEED512"),
                    Name = "phone"
                },
                new ProductType
                {
                    Id = Guid.Parse("80A146BC-7AE6-4E16-85EB-72DBA8AB3EB4"),
                    Name = "monitor"
                }
            };
        }
        private static IEnumerable<Products> GetProductsToSeed()
        {
            return new List<Products>
            {
                new Products
                {
                    Name = "iphone",
                    Size = 5,
                    ProductTypeId = Guid.Parse("DB329788-1477-4957-8F4F-5D796EAAEE1E")
                },
                new Products
                {
                    Name = "tcl",
                    Size = 3,
                    ProductTypeId = Guid.Parse("A4F5EC2B-55D6-4668-BA74-9C019669A9A5")
                }
            };
        }
    }
}
