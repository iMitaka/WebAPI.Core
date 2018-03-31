using JarvisEdge.Data.Repositories;
using JarvisEdge.DataTransferModels.Photo;
using JarvisEdge.DataTransferModels.Property;
using JarvisEdge.Models;
using JarvisEdge.ServiceInterfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JarvisEdge.Services
{
    public class PropertyService : IPropertyService
    {
        private readonly IUowData data;

        public PropertyService(IUowData data)
        {
            this.data = data;
        }

        public int? CreateProperty(string name)
        {
            if (name != null && name.Length >= 1)
            {
                var property = new Models.Property() { Title = name };
                var code = 10000 + data.Properties.All().Count() + 1;
                property.Code = code.ToString();
                this.data.Properties.Add(property);
                data.SaveChanges();
                return property.Id;
            }

            return null;
        }

        public IQueryable<PropertyGetModel> GetProperties()
        {
            var property = data.Properties.All()
            .Include(x => x.Extras)
            .Include(x => x.Photos)
            .Where(x => !x.Deleted)
            .Select(x => new PropertyGetModel()
            {
                Id = x.Id,
                PropertyStatus = x.PropertyStatus != null ? x.PropertyStatus.Name : string.Empty,
                Address = x.Address,
                AllFloorsCount = x.AllFloorsCount,
                Area = x.Area,
                BathroomsCount = x.BathroomsCount,
                BedroomsCount = x.BedroomsCount,
                BuildingType = x.BuildingType != null ? x.BuildingType.Name : string.Empty,
                Code = x.Code,
                Country = x.Country != null ? x.Country.Name : string.Empty,
                Curency = x.Curency != null ? x.Curency.Name : string.Empty,
                Description = x.Description,
                Extras = x.Extras.Select(e => e.Extra.Name),
                Floor = x.Floor,
                IsVIP = x.IsVIP,
                IsVisible = x.IsVisible,
                Neighborhood = x.Neighborhood != null ? x.Neighborhood.Name : string.Empty,
                OfferType = x.OfferType != null ? x.OfferType.Name : string.Empty,
                OwnerName = x.OwnerName,
                OwnerPhone = x.OwnerPhone,
                Price = x.Price,
                PropertyType = x.PropertyType != null ? x.PropertyType.Name : string.Empty,
                Title = x.Title,
                Town = x.Town != null ? x.Town.Name : string.Empty,
                Year = x.Year,
                Photos = x.Photos.Select(p => new PhotoGetModel()
                {
                    Id = p.Id,
                    Path = p.Path,
                    OrderNumber = p.Order
                }).OrderBy(p => p.OrderNumber)
            }
            );

            if (property != null)
            {
                return property;
            }

            return null;
        }

        public PropertyGetModel GetProperty(int id)
        {
            var property = data.Properties.All()
            .Include(x => x.Extras)
            .Include(x => x.Photos)
            .Where(x => !x.Deleted && x.Id == id)
            .Select(x => new PropertyGetModel()
            {
                Id = x.Id,
                PropertyStatus = x.PropertyStatus != null ? x.PropertyStatus.Name : string.Empty,
                Address = x.Address,
                AllFloorsCount = x.AllFloorsCount,
                Area = x.Area,
                BathroomsCount = x.BathroomsCount,
                BedroomsCount = x.BedroomsCount,
                BuildingType = x.BuildingType != null ? x.BuildingType.Name : string.Empty,
                Code = x.Code,
                Country = x.Country != null ? x.Country.Name : string.Empty,
                Curency = x.Curency != null ? x.Curency.Name : string.Empty,
                Description = x.Description,
                Extras = x.Extras.Select(e => e.Extra.Name),
                Floor = x.Floor,
                IsVIP = x.IsVIP,
                IsVisible = x.IsVisible,
                Neighborhood = x.Neighborhood != null ? x.Neighborhood.Name : string.Empty,
                OfferType = x.OfferType != null ? x.OfferType.Name : string.Empty,
                OwnerName = x.OwnerName,
                OwnerPhone = x.OwnerPhone,
                Price = x.Price,
                PropertyType = x.PropertyType != null ? x.PropertyType.Name : string.Empty,
                Title = x.Title,
                Town = x.Town != null ? x.Town.Name : string.Empty,
                Year = x.Year,
                Photos = x.Photos.Select(p => new PhotoGetModel()
                {
                    Id = p.Id,
                    Path = p.Path,
                    OrderNumber = p.Order
                }).OrderBy(p => p.OrderNumber)
            }
            ).FirstOrDefault();

            if (property != null)
            {
                return property;
            }

            return null;
        }

        public PropertyPostModel GetPropertyForEdit(int id)
        {
            var property = data.Properties.All()
           .Include(x => x.Extras)
           .Include(x => x.Photos)
           .Where(x => !x.Deleted && x.Id == id)
           .Select(x => new PropertyPostModel()
           {
               Address = x.Address,
               PropertyStatusId = x.PropertyStatusId,
               AllFloorsCount = x.AllFloorsCount,
               Area = x.Area,
               BathroomsCount = x.BathroomsCount,
               BedroomsCount = x.BedroomsCount,
               BuildingTypeId = x.BuildingTypeId,
               Code = x.Code,
               CountryId = x.CountryId,
               CurencyId = x.CurencyId,
               Description = x.Description,
               ExtrasIds = x.Extras.Select(e => e.Extra.Id),
               Floor = x.Floor,
               IsVIP = x.IsVIP,
               IsVisible = x.IsVisible,
               NeighborhoodId = x.NeighborhoodId,
               OfferTypeId = x.OfferTypeId,
               OwnerName = x.OwnerName,
               OwnerPhone = x.OwnerPhone,
               Price = x.Price,
               PropertyTypeId = x.PropertyTypeId,
               Title = x.Title,
               TownId = x.TownId,
               Year = x.Year,
               Photos = x.Photos.Select(p => new PhotoGetModel()
               {
                   Id = p.Id,
                   Path = p.Path,
                   OrderNumber = p.Order
               }).OrderBy(p => p.OrderNumber)
           }
           ).FirstOrDefault();


            if (property != null)
            {
                return property;
            }

            return null;
        }

        public bool EditProperty(PropertyPostModel model, int id)
        {
            var property = data.Properties.All()
                .Include(x => x.Extras)
                .Include(x => x.Photos)
                .FirstOrDefault(x => !x.Deleted && x.Id == id);

            if (property != null)
            {
                property.Address = model.Address;
                property.AllFloorsCount = model.AllFloorsCount;
                property.Area = model.Area;
                property.BathroomsCount = model.BathroomsCount;
                property.BedroomsCount = model.BedroomsCount;
                property.BuildingTypeId = model.BuildingTypeId;
                property.CountryId = model.CountryId;
                property.CurencyId = model.CurencyId;
                property.Description = model.Description;
                property.Floor = model.Floor;
                property.IsVIP = model.IsVIP;
                property.IsVisible = model.IsVisible;
                property.NeighborhoodId = model.NeighborhoodId;
                property.OfferTypeId = model.OfferTypeId;
                property.OwnerName = model.OwnerName;
                property.OwnerPhone = model.OwnerPhone;
                property.Price = model.Price;
                property.PropertyStatusId = model.PropertyStatusId;
                property.PropertyTypeId = model.PropertyTypeId;
                property.Title = model.Title;
                property.TownId = model.TownId;
                property.Year = model.Year;

                if (model.ExtrasIds != null && model.ExtrasIds.Count() >= 1)
                {
                    property.Extras.Clear();
                    var extra = new PropertyExtra();
                    foreach (var extraId in model.ExtrasIds)
                    {
                        extra.PropertyId = property.Id;
                        extra.ExtraId = extraId;
                        property.Extras.Add(extra);
                    }
                }

                data.SaveChanges();
                return true;
            }

            return false;
        }

        public bool DeleteProperty(int id)
        {
            var property = data.Properties.All()
                .FirstOrDefault(x => !x.Deleted && x.Id == id);

            if (property != null)
            {
                property.Deleted = true;
                data.SaveChanges();
                return true;
            }

            return false;
        }
    }
}
