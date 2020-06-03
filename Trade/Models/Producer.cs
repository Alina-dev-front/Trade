using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Trade.Repositories;

namespace Trade.Models
{
    public class Producer
    {
        public string ProducerName { get; set; }
        public List<Product> ProductList { get; set; }

        public Producer()
        {
        }
    }
}
