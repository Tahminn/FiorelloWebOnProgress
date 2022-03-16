using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiorelloBackDb.Models.Home
{
    public class AboutList:BaseEntity
    {
        public string Text { get; set; }
        public int AboutId { get; set; }
        public About About { get; set; }
    }
}
