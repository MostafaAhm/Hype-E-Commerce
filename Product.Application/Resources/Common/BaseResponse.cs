using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Application.Resources.Common
{
    public class BaseResponse
    {
        public bool? IsSuccess { get; set; }
        public int? StatusCode { get; set; }
        public string? ResponseMessage { get; set; }
    }
}
