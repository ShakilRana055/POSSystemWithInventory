using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace POSSystemWithInventory.EntityModel
{
    public class InvoiceDetails:BaseClass
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public int? InvoiceId { get; set; }
        public virtual Invoice Invoice { get; set; }
        public int? ProductId { get; set; }
        public virtual Product Product { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal Quantity { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal Amount { get; set; }
    }
}
