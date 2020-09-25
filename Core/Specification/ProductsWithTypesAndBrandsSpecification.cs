using System;
using System.Linq.Expressions;
using Core.Entities;

namespace Core.Specification
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification(ProductsSpecParams prodParams): 
        base(x => 
        (!prodParams.BrandId.HasValue || x.ProductBrandId == prodParams.BrandId) && 
        (!prodParams.TypeId.HasValue || x.ProductTypeId == prodParams.TypeId) &&
        (String.IsNullOrWhiteSpace(prodParams.search) || x.Name.Contains(prodParams.search)))
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            AddOrderBy(x => x.Name);
            ApplyPaging(prodParams.PageSize * (prodParams.PageIndex-1), prodParams.PageSize);

            if(!String.IsNullOrWhiteSpace(prodParams.Sort))
            {
                switch (prodParams.Sort)
                {
                    case "priceAsc":
                    AddOrderBy(p => p.Price);
                    break;
                    case "priceDesc":
                    AddOrderByDesc(p => p.Price);
                    break;
                    default: 
                    AddOrderBy(n => n.Name);
                    break;
                }
            }
        }

        public ProductsWithTypesAndBrandsSpecification(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}