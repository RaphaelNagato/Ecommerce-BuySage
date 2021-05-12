using Core.Entities;

namespace Core.Specifications
{
    public class ProductWithFiltersForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFiltersForCountSpecification(ProductSpecParams productSpecParams)
        : base(p =>
           (!productSpecParams.BrandId.HasValue || p.ProductBrandId == productSpecParams.BrandId) &&
           (!productSpecParams.TypeId.HasValue || p.ProductTypeId == productSpecParams.TypeId)
        )
        {
            
        }
    }
}