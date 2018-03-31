using JarvisEdge.DataTransferModels.Photo;
using System;
using System.Collections.Generic;
using System.Text;

namespace JarvisEdge.DataTransferModels.Property
{
    public class PropertyGetModel
    {
        public int Id { get; set; }
        public bool IsVisible { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Town { get; set; }
        public string Country { get; set; }
        public string Neighborhood { get; set; }
        public string OfferType { get; set; }
        public string PropertyType { get; set; }
        public string PropertyStatus { get; set; }
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
        public string BuildingType { get; set; }
        public string Curency { get; set; }
        public IEnumerable<string> Extras { get; set; }
        public IEnumerable<PhotoGetModel> Photos { get; set; } 
    }
}
