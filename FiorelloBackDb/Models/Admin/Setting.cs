using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiorelloBackDb.Models.Admin
{
    public class Setting
    {
        public int Id { get; set; }
        public int LoadTake { get; set; }
        public int ProductTake { get; set; }
    }
}
