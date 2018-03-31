using System;
using System.Collections.Generic;
using System.Text;

namespace JarvisEdge.Models
{
    public class PropertyExtra
    {
        public int PropertyId { get; set; }
        public Property Property { get; set; }
        public int ExtraId { get; set; }
        public Extra Extra { get; set; }
    }
}
