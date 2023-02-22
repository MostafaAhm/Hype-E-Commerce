using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product.Application.Resources.Common;

namespace Product.Application.Resources.Product.Create
{
    public class CreateProductResponseDto : BaseResponse
    {
        public Guid ProductId { get; set; }
    }
}
