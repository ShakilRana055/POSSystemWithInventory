using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace POSSystemWithInventory.Models
{
    public class SalesInvoiceDetailVM
    {
        public SalesInvoiceDetailVM()
        {
            SalesInvoiceDetail = new List<SalesInvoiceDetailVM>();
        }
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

        [Column(TypeName = "decimal(16,2)")]
        public decimal Count { get; set; }
        public string ProductName { get; set; }
        public string PhotoUrl { get; set; }
        public List<SalesInvoiceDetailVM> SalesInvoiceDetail { get; set; }
    }
}
