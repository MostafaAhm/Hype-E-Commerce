using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Resources.Common
{
    public class BasePaginationDto
    {
        const int maxPageSize = 50000;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 100;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value > maxPageSize ? maxPageSize : value;
            }
        }
    }
}
