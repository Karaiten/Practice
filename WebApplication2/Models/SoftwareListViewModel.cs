using System.Collections.Generic;
using System.Web.Mvc;

namespace SoftwareStore.Models
{
    public class SoftwareListViewModel
    {
        public IEnumerable<Software> Softwares { get; set; }
        public SelectList Categories { get; set; }
        public SelectList LicenseTypes { get; set; }
        public PageInfo PageInfo { get; set; }  
        public int? CurrentCategoryId { get; set; }
        public string CurrentLicenseType { get; set; }
    }
}
