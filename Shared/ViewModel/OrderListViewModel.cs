using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.ViewModel
{
    public class OrderListViewModel
    {
        public int OrderId { get; set; }
        public string Date { get; set; }
        public string Customer { get; set; }

        public string JsonProduct { get; set; }
    }

    public class ProductModel 
    {
        public string Product { get; set; }
        public string Quantity { get; set; }
        public string Rate { get; set; }
        public string Amount { get; set; }
    }

}
