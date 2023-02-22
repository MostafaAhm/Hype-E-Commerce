using Product.Application.Resources.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Resources.Product.Get
{
    public class GetProductsRequestDto : BasePaginationDto
    {
        public string? Name { get; set; }
        public int? Size { get; set; }
        public Guid? ProductTypeId { get; set; }
    }
}
