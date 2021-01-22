using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace POSSystemWithInventory.EntityModel
{
    public class Customer:BaseClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string NID { get; set; }
        public string PhotoUrl { get; set; }
        public string CompanyName { get; set; }
        public string Phone { get; set; }
        public string Designation { get; set; }
        public string Profession { get; set; }

        [Column(TypeName = "decimal(16,2)")]
        public decimal BonusPoint { get; set; }
    }
}
