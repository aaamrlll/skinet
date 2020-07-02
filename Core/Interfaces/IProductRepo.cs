using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepo
    {
        Task<IReadOnlyList<Product>> GetProductsAsync();
        Task<Product> GetProductAsync(int Id);
        Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
        Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();
    }
}