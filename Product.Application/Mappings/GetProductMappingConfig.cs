using Mapster;
using Product.Application.Resources.Product.Get;
using Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Mappings
{
    public class GetProductMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Products, ProductDto>()
                .Map(dest => dest.ProductName, src => src.ProductType.Name);
        }
    }
}
