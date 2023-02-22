using Product.Application.Resources.Common;
using Product.Application.Resources.Product.Get;
using Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Product.Application.Contracts.Persistence.Repositories
{
    public interface IProductRepository : IBaseRepository<Products>
    {
        IQueryable<Products> GetProducts(GetProductsRequestDto req);
        PagedList<Products> GetPagedListproducts(GetProductsRequestDto req);
    }
}
