﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BLL.DTO
{
    public class OrderDetailDTO
    {
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
    }
}