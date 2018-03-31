using System;
using System.Collections.Generic;
using System.Text;

namespace JarvisEdge.Models
{
    public class Extra
    {
        private ICollection<PropertyExtra> properties;

        public Extra()
        {
            this.properties = new HashSet<PropertyExtra>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool Deleted { get; set; }
        public ICollection<PropertyExtra> Properties { get => properties; set => properties = value; }
    }
}
