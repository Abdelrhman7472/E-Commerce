﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ProductModels
{
    public class ProductSpecificationsParameters
    {
        private const int MAXPAGESIZE = 10;
        private const int DEFAULTPAGESIZE = 5;
        public int? TypeId { get; set; }
        public int? BrandId { get; set; }

        public ProductSortingOptions? Sort { get; set; }

        public int PageIndex { get; set; } = 1;

        private int _pageSize = DEFAULTPAGESIZE;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MAXPAGESIZE ? MAXPAGESIZE : value;
        }
        public string? Search { get; set; }

    }
    public enum ProductSortingOptions
    {
        nameasc,
        namedesc,
        priceasc,
        pricedesc,
    }
}
