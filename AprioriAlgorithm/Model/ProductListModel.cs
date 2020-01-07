using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AprioriAlgorithm.Model
{
    public class ProductList
    {
        public int Count { get; set; }
        public List<string> Products { get; set; }

        public ProductList(List<string> Products, int Count)
        {
            this.Products = Products;
            this.Count = Count;
        }
    }
}
