using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace POSSystemWithInventory.EntityModel
{
    public class SalesInvoice:BaseClass
    {
        public SalesInvoice()
        {
            SalesInvoiceDetails = new List<SalesInvoiceDetail>();
        }
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public int? CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal SubTotal { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal Discount { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal GrandTotal { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal PaidAmount { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal Dues { get; set; }
        public string PaymentMode { get; set; }
        public int? VatAndTaxId { get; set; }
        public VatAndTax VatAndTax { get; set; }
        public List<SalesInvoiceDetail> SalesInvoiceDetails { get; set; }
    }
}
