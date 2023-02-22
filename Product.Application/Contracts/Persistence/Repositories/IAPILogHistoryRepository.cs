using Product.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Contracts.Persistence.Repositories
{
    public interface IAPILogHistoryRepository : IBaseRepository<APILogHistory>
    {
    }
}
