using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POSSystemWithInventory.EntityModel
{
    public class BaseClass
    {
        public BaseClass()
        {
            CreatedDate = DateTime.Now.ToShortDateString();
            IsActive = true;
            IsDeleted = false;
            IsUpdated = false;
        }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsUpdated { get; set; }
        public string CreatedDate { get; set; }
        public string UpdatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}
