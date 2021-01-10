using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace POSSystemWithInventory.EntityModel
{
    public class PurchaseProductDetail:BaseClass
    {
        public int Id { get; set; }
        public int? PurchaseProductId { get; set; }
        public PurchaseProduct PurchaseProduct { get; set; }
        public string InvoiceNumber { get; set; }
        public int? ProductId { get; set; }
        public Product Product { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal Quantity { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal UnitPrice { get; set; }
        public int? VatAndTaxId { get; set; }
        public VatAndTax VatAndTax { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal BasePrice { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal SellPrice { get; set; }
    }
}
