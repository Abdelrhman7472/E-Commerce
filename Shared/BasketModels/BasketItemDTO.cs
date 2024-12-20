﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.BasketModels
{
    public record BasketItemDTO
    {
        public int Id { get; init; }

        public string ProductName { get; init; }
        public string PictureUrl { get; init; }
        [Range(1, double.MaxValue)]
        public decimal Price { get; init; }

        [Range(1, 99)]
        public int Quantity { get; init; }
    }
}
