using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace POSSystemWithInventory.EntityModel
{
    public class Inventory:BaseClass
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public Product Product { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal AvailableQuantity { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal SellPrice { get; set; }
    }
}
