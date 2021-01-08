using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace POSSystemWithInventory.Models
{
    public class PurchaseProductDetailVM
    {
        public PurchaseProductDetailVM()
        {
            IsVatActive = false;
        }
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public int? StockId { get; set; }
        public int? ProductId { get; set; }
        public int? VatId { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal PurchasePrice { get; set; }
        [Column(TypeName = "decimal(16,2)")]
        public decimal BasePrice { get; set; }
        [Column(TypeName = "decimal(16,2)")]
        public decimal SellPrice { get; set; }
        public bool IsBaseAndSellPriceSame { get; set; }
        public bool IsVatActive { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal Quantity { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal Amount { get; set; }
    }
}
