using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POSSystemWithInventory.Models
{
    public class SupplierVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string NID { get; set; }
        public string PhotoUrl { get; set; }
        public string CompanyName { get; set; }
        public string Designation { get; set; }
        public string Phone { get; set; }
        public IFormFile Photo { get; set; }
       
    }
}
