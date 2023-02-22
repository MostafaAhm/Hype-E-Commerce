using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Resources.Product.Create
{
    public class CreateProductRequestDto
    {
        public string Name { get; set; }
        public int Size { get; set; }
        public Guid ProductTypeId { get; set; }
    }
}
