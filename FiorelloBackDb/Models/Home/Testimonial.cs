using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiorelloBackDb.Models.Home
{
    public class Testimonial:BaseEntity
    {
        public string FullName { get; set; }
        public string Specialization { get; set; }
        public string Statement { get; set; }
        public string Image { get; set; }
    }
}
