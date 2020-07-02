using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Core.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Data
{
    public class StoreContextSeed
    {
        public static async Task SeedDataAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                CheckForData<ProductBrand>(context, !context.ProductBrands.Any(), "../Infrastructure/Data/SeedData/brands.json");
                CheckForData<ProductType>(context, !context.ProductBrands.Any(), "../Infrastructure/Data/SeedData/types.json");
                CheckForData<Product>(context, !context.ProductBrands.Any(), "../Infrastructure/Data/SeedData/products.json");

                await context.SaveChangesAsync();
            }
            catch (Exception e)
            {
               var logger = loggerFactory.CreateLogger<StoreContextSeed>();
               logger.LogError(e.Message);

            }
        }

        private static void CheckForData<T>(StoreContext context, bool Any, string url) where T : class
        {
            if (Any)
            {
                var data = File.ReadAllText(url);
                var items = JsonSerializer.Deserialize<List<T>>(data);
                context.AddRange(items);
            }
        }
    }
}