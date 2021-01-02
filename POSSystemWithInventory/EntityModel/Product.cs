using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace POSSystemWithInventory.EntityModel
{
    public class Product:BaseClass
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public string PhotoUrl { get; set; }
        public string Description { get; set; }
        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }
        public int? UnitId { get; set; }
        public virtual Unit Unit { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal WarningQuantity { get; set; }
    }
}
