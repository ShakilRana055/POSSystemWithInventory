using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POSSystemWithInventory.EntityModel
{
    public class CompanyInformation:BaseClass
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Slogan { get; set; }
        public string LogoUrl { get; set; }
        public string CompanyType { get; set; }
        public string CurrencyType { get; set; }
        public string CurrencyImage { get; set; }
    }
}
