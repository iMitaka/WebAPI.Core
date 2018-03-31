using JarvisEdge.DataTransferModels.Photo;
using System;
using System.Collections.Generic;
using System.Text;

namespace JarvisEdge.DataTransferModels.Property
{
    public class PropertyPostModel
    {
        public bool IsVisible { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int? TownId { get; set; }
        public int? CountryId { get; set; }
        public int? NeighborhoodId { get; set; }
        public int? OfferTypeId { get; set; }
        public int? PropertyTypeId { get; set; }
        public int? PropertyStatusId { get; set; }
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
        public int? BuildingTypeId { get; set; }
        public int? CurencyId { get; set; }
        public IEnumerable<int> ExtrasIds { get; set; }
        public IEnumerable<PhotoGetModel> Photos { get; set; }
    }
}
