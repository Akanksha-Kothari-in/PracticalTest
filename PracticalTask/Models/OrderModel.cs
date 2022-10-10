using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PracticalTask.Models
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public string Customer { get; set; }
        public string Product { get; set; }
        public string Remarks { get; set; }
    }
}