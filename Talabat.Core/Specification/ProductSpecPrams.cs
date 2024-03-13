﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Specification
{
    public class ProductSpecPrams
    {

        private const int MaxPageSize = 10;

        private int pageSize = 5;

        public int PageSize
        {
            get { return pageSize; }

            set { pageSize = value > MaxPageSize ? MaxPageSize : value; }
        }
        public int PageIndex { get; set; } = 1;

        public string? sort { get; set; }
        public int? BrandId { get; set; }
        public int? TypeId { get; set; }

        private string? Search { get; set; }
        public string? SearchByName {
            get { return Search; }
            set { Search = value.ToLower(); } 
        }    

    }
}
