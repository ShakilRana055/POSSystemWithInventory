using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace POSSystemWithInventory.Models
{
    public class SalesInvoiceDetailVM
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public int? SalesInvoiceId { get; set; }
        public int? ProductId { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal Quantity { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal Price { get; set; }
        [Column(TypeName = "decimal(16,2)")]
        public decimal UnitPrice { get; set; }
    }
}
