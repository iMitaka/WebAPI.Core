using System;
using System.Collections.Generic;
using System.Text;

namespace JarvisEdge.Models
{
    public class Property
    {
        private ICollection<Photo> photos;
        private ICollection<PropertyExtra> extras;

        public Property()
        {
            this.photos = new HashSet<Photo>();
            this.extras = new HashSet<PropertyExtra>();
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
        public int? BedroomsCount { get; set; }
        public int? BathroomsCount { get; set; }
        public int? Area { get; set; }
        public int? Price { get; set; }
        public string Year { get; set; }
        public string Address { get; set; }
        public string OwnerName { get; set; }
        public string OwnerPhone { get; set; }
        public bool IsVIP { get; set; }
        public string Code { get; set; }
        public string Floor { get; set; }
        public string AllFloorsCount { get; set; }
        public int? BuildingTypeId { get; set; }
        public BuildingType BuildingType { get; set; }
        public int? CurencyId { get; set; }
        public Curency Curency { get; set; }
        public int? ApartamentTypeId { get; set; }
        public ApartamentType ApartamentType { get; set; }
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public ICollection<Photo> Photos { get => photos; set => photos = value; }
        public ICollection<PropertyExtra> Extras { get => extras; set => extras = value; }
    }
}
