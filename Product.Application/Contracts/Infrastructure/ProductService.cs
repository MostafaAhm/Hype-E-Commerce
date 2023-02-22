using Product.Application.Resources.Product.Create;
using Product.Application.Resources.Product.Get;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Contracts.Infrastructure
{
    public interface IProductService
    {
        Task<CreateProductResponseDto> CreateProduct(CreateProductRequestDto dto);
        GetProductsResponseDto GetProducts(GetProductsRequestDto dto);
    }
}
