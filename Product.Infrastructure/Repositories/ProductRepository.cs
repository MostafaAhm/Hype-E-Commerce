using Product.Application.Contracts.Persistence.Repositories;
using Product.Application.Resources.Product.Get;
using Product.Domain.Entities;
using Product.Infrastructure.Persistence;
using System.Linq.Expressions;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Product.Application.Resources.Common;

namespace Product.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Products>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }
        public IQueryable<Products> GetProducts(GetProductsRequestDto req)
        {
            Expression<Func<Products, bool>> predicate = c => true;

            if (!string.IsNullOrEmpty(req.Name))
            {
                predicate = predicate.And(p => p.Name == req.Name);
            }if (req.Size != null)
            {
                predicate = predicate.And(p => p.Size == req.Size);
            }if (req.ProductTypeId != null)
            {
                predicate = predicate.And(p => p.ProductTypeId == req.ProductTypeId);
            }
            return GetMany(predicate)
                .Include("ProductType");
        }
        public PagedList<Products> GetPagedListproducts(GetProductsRequestDto req)
        {
            return PagedList<Products>.ToPagedList(GetProducts(req).OrderByDescending(x => x.CreatedDate), req.PageNumber, req.PageSize);
        }

    }
}
