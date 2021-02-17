using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace POSSystemWithInventory.Models
{
    public class PurchaseProductDetailVM
    {
        public int Id { get; set; }
        public int? PurchaseProductId { get; set; }
        public string InvoiceNumber { get; set; }
        public int? ProductId { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal Quantity { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal UnitPrice { get; set; }
        public int? VatAndTaxId { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal BasePrice { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal SellPrice { get; set; }
    }
}
