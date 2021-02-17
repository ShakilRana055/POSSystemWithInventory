using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace POSSystemWithInventory.Models
{
    public class SalesInvoiceVM
    {
        public SalesInvoiceVM()
        {
            SalesInvoiceDetails = new List<SalesInvoiceDetailVM>();
        }
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public int? CustomerId { get; set; }

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
        public bool IsBonusPointTaken { get; set; }
        public SalesInvoiceDetailVM SalesInvoiceDetail { get; set; }
        public List<SalesInvoiceDetailVM> SalesInvoiceDetails { get; set; }

        public List<SelectListItem> CustomerItem { get; set; }
        public List<SelectListItem> ProductItem { get; set; }
        public List<SelectListItem> VatItem { get; set; }
    }
}
