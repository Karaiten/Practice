using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System;
using System.Collections.Generic;
using System.Web;
using System.Data.Entity;

namespace SoftwareStore.Models
{
    public class SoftwareContext : DbContext
    {
        public DbSet<Software> Softwares { get; set; }
        public DbSet<Purchase> Purchases { get; set; }

        public DbSet<Category> Categories { get; set; }
    }

}
