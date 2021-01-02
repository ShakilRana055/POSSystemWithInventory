using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POSSystemWithInventory.EntityModel
{
    public class Supplier:BaseClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string NID { get; set; }
        public string PhotoUrl { get; set; }
    }
}
