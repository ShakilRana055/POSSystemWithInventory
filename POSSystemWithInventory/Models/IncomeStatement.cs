using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace POSSystemWithInventory.Models
{
    public class IncomeStatement
    {
        public int Id { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal TotalSalesAmount { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal TotalPurchaseAmount { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal AccountPayable { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal AccountReceivable { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal FinalStatement { get; set; }
    }
}
