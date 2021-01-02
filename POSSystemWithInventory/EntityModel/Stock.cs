using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace POSSystemWithInventory.EntityModel
{
    public class Stock:BaseClass
    {
        public Stock()
        {
            StockDetails = new List<StockDetails>();
        }
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public int? SupplierId { get; set; }
        public virtual Supplier Supplier { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal TotalAmount { get; set; }
        public bool InvoiceStatus { get; set; }
        public List<StockDetails> StockDetails { get; set; }
    }
}
