using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftwareStore.Models
{
    public class SoftwareIndexViewModel
    {
        public IEnumerable<Software> Softwares { get; set; }
        public PageInfo PageInfo { get; set; }
    }
}
