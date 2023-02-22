using Product.Application.Contracts.Persistence;
using Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Product.Application.Contracts.Persistence.Repositories;
using Product.Infrastructure.Persistence;

namespace Product.Infrastructure.Repositories
{
    public class APILogHistoryRepository : BaseRepository<APILogHistory>, IAPILogHistoryRepository
    {
        public APILogHistoryRepository(AppDbContext context) 
            : base(context)
        {
        }
    }
}
