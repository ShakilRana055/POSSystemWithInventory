using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace POSSystemWithInventory.EntityModel
{
    public class SalesInvoiceDetail:BaseClass
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public int? SalesInvoiceId { get; set; }
        public SalesInvoice SalesInvoice { get; set; }
        public int? ProductId { get; set; }
        public Product Product { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal Quantity { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal UnitPrice { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal Price { get; set; }
    }
}
