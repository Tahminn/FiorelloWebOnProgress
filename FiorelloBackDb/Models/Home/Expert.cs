using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiorelloBackDb.Models.Home
{
    public class Expert:BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Specialization { get; set; }
        public string Image { get; set; }
        public int FlwExpertSectionId { get; set; }
        public FlwExpertSection FlwExpertSection { get; set; }
    }
}
