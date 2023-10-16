using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModel
{
    public class ProductViewModel
    {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public int CategoryID { get; set; }
            public string CategoryName { get; set; }
            public int Quantity { get; set; }
            public double Price { get; set; }
            public List<string> Images { get; set; }
    }
}
