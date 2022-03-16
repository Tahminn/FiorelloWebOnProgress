using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiorelloBackDb.Models.Home
{
    public class About: BaseEntity
    {
        public string Image { get; set; }
        public string Header { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public ICollection<AboutList> AboutLists { get; set; }
    }
}
