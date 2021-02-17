using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace POSSystemWithInventory.EntityModel
{
    public class PurchaseProduct:BaseClass
    {
        public PurchaseProduct()
        {
            PurchaseProductDetail = new List<PurchaseProductDetail>();
        }
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public int? SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal GrandTotal { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal SubTotal { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal Discount { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal PaidAmount { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal Dues { get; set; }
        public string PaymentMode { get; set; }
        public List<PurchaseProductDetail> PurchaseProductDetail { get; set; }
        
    }
}
