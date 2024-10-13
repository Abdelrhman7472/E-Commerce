﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public record CustomerBasketDTO
    {
        public string Id { get; init; }

        public IEnumerable<BasketItemDTO> Items { get; init; }
    }
}
