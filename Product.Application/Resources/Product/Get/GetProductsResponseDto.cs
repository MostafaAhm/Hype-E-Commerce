using Product.Application.Resources.Common;
using Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Resources.Product.Get
{
    public class GetProductsResponseDto : BaseResponse
    {
        public IEnumerable<ProductDto> Data { get; set; }
    }
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public string ProductName { get; set; }
    }
}
