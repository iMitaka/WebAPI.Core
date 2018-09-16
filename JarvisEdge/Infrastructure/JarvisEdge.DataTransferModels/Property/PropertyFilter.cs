using System;
using System.Collections.Generic;
using System.Text;

namespace JarvisEdge.DataTransferModels.Property
{
    public class PropertyFilter
    {
        public int? PropertyTypeId { get; set; }
        public int? OfferTypeId { get; set; }
        public int? PriceFrom { get; set; }
        public int? PriceTo { get; set; }
        public int? BedroomsFrom { get; set; }
        public int? BedroomsTo { get; set; }
        public int? AreaFrom { get; set; }
        public int? AreaTo { get; set; }
        public int? CurrencyId { get; set; }
        public int? TownId { get; set; }
        public int? NeighbourhoodId { get; set; }
        public int? CountryId { get; set; }
        public string address { get; set; }
    }
}
