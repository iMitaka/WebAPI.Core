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

        public int? CreateProperty(string name, string username)
        {
            var user = data.ApplicationUsers.All().FirstOrDefault(x => x.UserName.ToLower() == username.ToLower());

            if (name != null && name.Length >= 1)
            {
                var property = new Models.Property() { Title = name };
                var code = 10000 + data.Properties.All().Count() + 1;
                property.Code = code.ToString();
                property.ApplicationUser = user;
                this.data.Properties.Add(property);
                data.SaveChanges();
                return property.Id;
            }

            return null;
        }

        public IQueryable<PropertyGetModel> GetProperties(PropertyFilter filter, int page, int totalCount, string type)
        {
            var property = data.Properties.All()
            .Include(x => x.Extras)
            .Include(x => x.Photos)
            .Where(x => !x.Deleted).OrderByDescending(x => x.IsVIP).ThenByDescending(x => x.Id).AsQueryable();

            if (filter != null)
            {
                if (filter.TownId > 0)
                {
                    property = property.Where(x => x.TownId == filter.TownId);
                }
                if (filter.NeighbourhoodId > 0)
                {
                    property = property.Where(x => x.NeighborhoodId == filter.NeighbourhoodId);
                }
                if (filter.OfferTypeId > 0)
                {
                    property = property.Where(x => x.OfferTypeId == filter.OfferTypeId);
                }
                if (filter.PropertyTypeId > 0)
                {
                    property = property.Where(x => x.PropertyTypeId == filter.PropertyTypeId);
                }
                if (filter.CurrencyId > 0)
                {
                    property = property.Where(x => x.CurencyId == filter.CurrencyId);
                }
                if (filter.PriceFrom > 0)
                {
                    property = property.Where(x => x.Price >= filter.PriceFrom);
                }
                if (filter.PriceTo > 0)
                {
                    property = property.Where(x => x.Price <= filter.PriceTo);
                }
                if (filter.BedroomsFrom > 0)
                {
                    property = property.Where(x => x.BedroomsCount >= filter.BedroomsFrom);
                }
                if (filter.BedroomsTo > 0)
                {
                    property = property.Where(x => x.BedroomsCount <= filter.BedroomsTo);
                }
                if (filter.AreaFrom > 0)
                {
                    property = property.Where(x => x.Area >= filter.AreaFrom);
                }
                if (filter.AreaTo > 0)
                {
                    property = property.Where(x => x.Area <= filter.AreaTo.Value);
                }
                if (filter.CountryId > 0)
                {
                    property = property.Where(x => x.CountryId == filter.CountryId.Value);
                }
                if (filter.address != null && filter.address.Length >= 1)
                {
                    property = property.Where(x => x.Address.ToLower().Contains(filter.address.ToLower()) || x.OwnerPhone.ToLower().Contains(filter.address.ToLower()));
                }
            }

            if (type != "admin")
            {
                property = property.Where(x => x.IsVisible);
            }

            var totalPropertyCount = property.Count();

            var result = property.Skip(totalCount * (page - 1)).Take(totalCount)
            .Select(x => new PropertyGetModel()
            {
                Id = x.Id,
                PropertyStatus = x.PropertyStatus != null ? x.PropertyStatus.Name : string.Empty,
                Address = x.Address,
                AllFloorsCount = x.AllFloorsCount,
                Area = x.Area != null ? x.Area.Value.ToString() : string.Empty,
                BathroomsCount = x.BathroomsCount != null ? x.BathroomsCount.Value.ToString() : string.Empty,
                BedroomsCount = x.BedroomsCount != null ? x.BedroomsCount.Value.ToString() : string.Empty,
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
                Price = x.Price != null ? x.Price.Value.ToString() : string.Empty,
                PropertyType = x.PropertyType != null ? x.PropertyType.Name : string.Empty,
                Title = x.Title,
                Town = x.Town != null ? x.Town.Name : string.Empty,
                Year = x.Year,
                TotalCount = totalPropertyCount,
                ApartamentType = x.ApartamentType != null ? x.ApartamentType.Name : string.Empty,
                createdBy = x.ApplicationUserId != null ? x.ApplicationUser.FirstName + ' ' + x.ApplicationUser.LastName : string.Empty,
                createrPhone = x.ApplicationUserId != null ? x.ApplicationUser.Phone : string.Empty,
                Photos = x.Photos.Where(p => !p.Deleted).Select(p => new PhotoGetModel()
                {
                    Id = p.Id,
                    Path = p.Path,
                    OrderNumber = p.Order
                }).OrderBy(p => p.OrderNumber)
            }
            ).AsQueryable();

            if (property != null)
            {
                return result;
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
                Area = x.Area.ToString(),
                BathroomsCount = x.BathroomsCount != null ? x.BathroomsCount.Value.ToString() : string.Empty,
                BedroomsCount = x.BedroomsCount != null ? x.BedroomsCount.Value.ToString() : string.Empty,
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
                Price = x.Price.ToString(),
                PropertyType = x.PropertyType != null ? x.PropertyType.Name : string.Empty,
                Title = x.Title,
                Town = x.Town != null ? x.Town.Name : string.Empty,
                Year = x.Year,
                ApartamentType = x.ApartamentType != null ? x.ApartamentType.Name : string.Empty,
                createdBy = x.ApplicationUserId != null ? x.ApplicationUser.FirstName + ' ' + x.ApplicationUser.LastName : string.Empty,
                createrPhone = x.ApplicationUserId != null ?  x.ApplicationUser.Phone : string.Empty,
                Photos = x.Photos.Where(p => !p.Deleted).Select(p => new PhotoGetModel()
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
               Area = x.Area != null ? x.Area.Value : 0,
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
               Price = x.Price != null ? x.Price.Value : 0,
               PropertyTypeId = x.PropertyTypeId,
               Title = x.Title,
               TownId = x.TownId,
               Year = x.Year,
               ApartamentTypeId = x.ApartamentTypeId,
               Photos = x.Photos.Where(p => !p.Deleted).Select(p => new PhotoGetModel()
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

        public bool EditProperty(PropertyPostModel model, int id, string username)
        {
            var property = data.Properties.All()
             .Include(x => x.Extras)
             .Include(x => x.Photos)
             .Include(x => x.ApplicationUser)
             .FirstOrDefault(x => !x.Deleted && x.Id == id);

            if (property != null)
            {
                if ((property.ApplicationUserId == null && username.ToLower() == "admin") || property.ApplicationUser.UserName.ToLower() == username.ToLower()) {
               
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
                    property.ApartamentTypeId = model.ApartamentTypeId;

                    if (model.ExtrasIds != null && model.ExtrasIds.Count() >= 1)
                    {
                        property.Extras.Clear();
                        data.SaveChanges();
                        var extra = new PropertyExtra();
                        foreach (var extraId in model.ExtrasIds)
                        {
                            property.Extras.Add(new PropertyExtra() { ExtraId = extraId });
                        }
                    }

                    data.SaveChanges();
                    return true;
                }
            }

            return false;
        }

        public bool DeleteProperty(int id, string username)
        {
            var property = data.Properties.All()
                .Include(x => x.ApplicationUser)
                .FirstOrDefault(x => !x.Deleted && x.Id == id);

            if (property != null)
            {
                if ((property.ApplicationUserId == null && username.ToLower() == "admin") || property.ApplicationUser.UserName.ToLower() == username.ToLower())
                {
                    property.Deleted = true;
                    data.SaveChanges();
                    return true;
                }
            }

            return false;
        }
    }
}
