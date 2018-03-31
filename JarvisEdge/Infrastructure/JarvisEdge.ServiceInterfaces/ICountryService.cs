using JarvisEdge.DataTransferModels.Country;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JarvisEdge.ServiceInterfaces
{
    public interface ICountryService
    {
        bool CreateCountry(string name);
        IQueryable<CountryGetModel> GetCountries();
        bool DeleteCountry(int id);
    }
}
