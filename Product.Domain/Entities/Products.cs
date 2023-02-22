using Product.Domain.Common;

namespace Product.Domain.Entities
{
    public class Products : EntityBase
    {
        public string Name { get; set; }
        public int Size { get; set; }
        public Guid ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }
    }
}
