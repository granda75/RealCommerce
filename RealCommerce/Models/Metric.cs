using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealCommerce.Models
{
    public class Metric
    {
        public double Value { get; set; }
        public string Unit { get; set; }
        public int UnitType { get; set; }
    }
}