using System;
using System.Collections.Generic;
using System.Text;

namespace JarvisEdge.Models
{
    public class Extra
    {
        private ICollection<Property> properties;

        public Extra()
        {
            this.properties = new HashSet<Property>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool Deleted { get; set; }
        public ICollection<Property> Properties { get => properties; set => properties = value; }
    }
}
