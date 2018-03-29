using System;
using System.Collections.Generic;
using System.Text;

namespace JarvisEdge.Models
{
    public class Property
    {
        private ICollection<Photo> photos;

        public Property()
        {
            this.photos = new HashSet<Photo>();
        }

        public int Id { get; set; }
        public bool Deleted { get; set; }
        public bool IsVisible { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? TownId { get; set; }
        public Town Town { get; set; }
        public int? CountryId { get; set; }
        public Country Country { get; set; }
        public int? NeighborhoodId { get; set; }
        public Neighborhood Neighborhood { get; set; }
        public int? OfferTypeId { get; set; }
        public OfferType OfferType { get; set; }
        public int? PropertyTypeId { get; set; }
        public PropertyType PropertyType { get; set; }
        public int? PropertyStatusId { get; set; }
        public PropertyStatus PropertyStatus { get; set; }
        public bool HasEscalator { get; set; }
        public string BedroomsCount { get; set; }
        public string BathroomsCount { get; set; }
        public string Area { get; set; }
        public string Price { get; set; }
        public string Year { get; set; }
        public string Address { get; set; }
        public string OwnerName { get; set; }
        public string OwnerPhone { get; set; }
        public bool IsVIP { get; set; }
        public string Code { get; set; }
        public string Floor { get; set; }
        public string AllFloorsCount { get; set; }
        public bool HasParking { get; set; }
        public ICollection<Photo> Photos { get => photos; set => photos = value; }
    }
}
