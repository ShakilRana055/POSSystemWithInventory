using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POSSystemWithInventory.Models
{
    public class SettingVM
    {
        public int Id { get; set; }
        public List<SelectListItem> CustomerItem { get; set; }
        public List<SelectListItem> SupplierItem { get; set; }
        public List<int> Information { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
