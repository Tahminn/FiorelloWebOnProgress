using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiorelloBackDb.Models.Home
{
    public class Product:BaseEntity
    {
        public string Name { get; set; }        
        public decimal Price { get; set; }
        public int UnitsSold { get; set; } = 0;
        public int UnitsQuantity { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }
    }
}
