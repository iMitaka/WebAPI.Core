using System;
using System.Collections.Generic;
using System.Text;

namespace JarvisEdge.Models
{
    public class Town
    {
        private ICollection<Neighborhood> neighborhoods;

        public Town()
        {
            this.neighborhoods = new HashSet<Neighborhood>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? CountryId { get; set; }
        public Country Country { get; set; }
        public bool Deleted { get; set; }
        public ICollection<Neighborhood> Neighborhoods { get => neighborhoods; set => neighborhoods = value; }
    }
}
