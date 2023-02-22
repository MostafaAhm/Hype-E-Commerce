using Product.Application.Contracts.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Contracts.Persistence
{
    public interface IUnitOfWork
    {
        IProductRepository Products { get; }
        IAPILogHistoryRepository APILogHistory { get; }

        Task SaveChangesAsync();
        void Complete();
    }
}
