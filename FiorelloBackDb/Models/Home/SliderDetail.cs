using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiorelloBackDb.Models.Home
{
    public class SliderDetail:BaseEntity
    {
        public string Header { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
