using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop
{
    class Product
    {
        public int id { get; }
        private static int counter = 0;
        public string name { get; }
        public string? description { get; set; }
        public double price { get; set; }
        public Product(string _name)
        {
            id = counter++;
            name = _name;
        }
    }
}
