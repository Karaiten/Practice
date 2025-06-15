using System.Collections.Generic;

namespace SoftwareStore.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // навігаційна властивість
        public virtual ICollection<Software> Softwares { get; set; }

        public Category()
        {
            Softwares = new List<Software>();
        }
    }
}
