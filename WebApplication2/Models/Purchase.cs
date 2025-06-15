using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SoftwareStore.Models
{
    public class Purchase
    {
        public int PurchaseId { get; set; }
        // ім’я та прізвище покупця
        public string Person { get; set; }
        // адреса покупця
        public string Address { get; set; }
        // ID програмного забезпечення
        public int SoftwareId { get; set; }
        // дата покупки
        public DateTime Date { get; set; }
    }
}