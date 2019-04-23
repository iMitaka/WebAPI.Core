using System;
using System.Collections.Generic;
using System.Text;

namespace JarvisEdge.Models
{
    public class Neighborhood
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? TownId { get; set; }
        public Town Town { get; set; }
        public bool Deleted { get; set; }
    }
}
