using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace POSSystemWithInventory.EntityModel
{
    public class Invoice:BaseClass
    {
        public Invoice()
        {
            InvoiceDetails = new List<InvoiceDetails>();
        }
        public int Id { get; set; }
        public int InvoiceNumber { get; set; }
        public int? CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal SubTotal { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal GrandTotal { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal Discount { get; set; }
        public int? VatAndTaxId { get; set; }
        public virtual VatAndTax VatAndTax { get; set; }
        public string PaymentMode { get; set; }
        public List<InvoiceDetails> InvoiceDetails { get; set; }
    }
}
