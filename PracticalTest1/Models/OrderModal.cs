using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PracticalTest1.Models
{
    public class OrderModal
    {
        public int OrderId { get; set; }
        public string Customer { get; set; }
        public string Product { get; set; }
        public string Remarks { get; set; }

    }
}