using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftwareStore.Models
{
    public class PageInfo
    {
        public int PageNumber { get; set; }     // поточна сторінка
        public int PageSize { get; set; }       // кількість на сторінці
        public int TotalItems { get; set; }     // всього елементів
        public int TotalPages => (int)Math.Ceiling((decimal)TotalItems / PageSize);
    }
}
