using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POSSystemWithInventory.EntityModel
{
    public class Brand:BaseClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string LogoUrl { get; set; }
        public string Description { get; set; }
    }
}
