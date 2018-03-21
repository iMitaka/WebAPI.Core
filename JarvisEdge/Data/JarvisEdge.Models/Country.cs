using System;
using System.Collections.Generic;
using System.Text;

namespace JarvisEdge.Models
{
    public class Country
    {
        private ICollection<Town> towns;

        public Country()
        {
            this.towns = new HashSet<Town>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool Deleted { get; set; }
        public ICollection<Town> Towns { get => towns; set => towns = value; }
    }
}
