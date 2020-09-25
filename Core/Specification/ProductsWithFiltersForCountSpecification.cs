using System;
using Core.Entities;

namespace Core.Specification
{
    public class ProductsWithFiltersForCountSpecification: BaseSpecification<Product>
    {
        public ProductsWithFiltersForCountSpecification(ProductsSpecParams prodParams) :
        base(x =>
        (String.IsNullOrWhiteSpace(prodParams.search) || x.Name.Contains(prodParams.search)) &&
        (!prodParams.BrandId.HasValue || x.ProductBrandId == prodParams.BrandId) &&
        (!prodParams.TypeId.HasValue || x.ProductTypeId == prodParams.TypeId))
        {

        }
    }
}