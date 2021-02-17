using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace POSSystemWithInventory.Models
{
    public class VatAndTaxVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Column(TypeName = "decimal(16,2)")]
        public decimal Amount { get; set; }
    }
}
