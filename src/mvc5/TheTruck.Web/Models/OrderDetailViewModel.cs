using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheTruck.Web.Models
{
    public class OrderDetailViewModel
    {
        public int ProductName { get; set; }
        public string ProductImage { get; set; }
        public int ProductQuantity { get; set; }
        public Decimal ProductUnitPrice { get; set; }
        public Decimal ProductTotalPrice { get; set; }
    }
}