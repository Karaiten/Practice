using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftwareStore.Models
{
    public class Software
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Developer { get; set; }
        public string Version { get; set; }
        public int Price { get; set; }
        public string LicenseType { get; set; }

        public int? CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }

}
