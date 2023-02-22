﻿using Product.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.Entities
{
    public class APILogHistory : EntityBase
    {
        public string? Method { get; set; }
        public string? Schema { get; set; }
        public string? Host { get; set; }
        public string? Path { get; set; }
        public string? QueryString { get; set; }
        public string? RequestBody { get; set; }
        public string? ResponseBody { get; set; }
        public int StatusCode { get; set; }
    }
}
