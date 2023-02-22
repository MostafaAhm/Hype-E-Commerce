using Product.Application.Contracts.Persistence;
using Product.Application.Contracts.Persistence.Repositories;
using Product.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _context;
        private IAPILogHistoryRepository _apiLogHistory;
        private IProductRepository _products;

        public UnitOfWork(AppDbContext dbContext)
        {
            this._context = dbContext;
        }

        public IAPILogHistoryRepository APILogHistory
        {
            get
            {
                if (_apiLogHistory == null)
                {
                    _apiLogHistory = new APILogHistoryRepository(_context);
                }

                return _apiLogHistory;
            }
        }

        public IProductRepository Products
        {
            get
            {
                if(_products == null)
                {
                    _products = new ProductRepository(_context);
                }
                return _products;
            }
        }


        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Complete()
        {
            _context.SaveChanges();
        }
    }
}
