using JobsDataModel;
using Locations.DTO;
using Locations.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Locations.Manager
{
    public interface ILocationsManager
    {
        List<LocationDTO> GetAllLocations();
        int CreateLocation(LocationDTO location);
        bool UpdateLocation(int id, LocationDTO location);
        LocationDTO GetLocation(int id);
    }

    public class LocationsManager: ILocationsManager
    {
        private ILocationsRepository _locationsRepository;

        public LocationsManager(ILocationsRepository locationsRepository)
        {
            _locationsRepository = locationsRepository;
        }

        public static ILocationsManager NewLocationsManager
        {
            get
            {
                return new LocationsManager(LocationsRepository.NewLocationsRepository);
            }
        }

        public List<LocationDTO> GetAllLocations()
        {
            try
            {
                List<LocationDTO> locations = new List<LocationDTO>();
                var data = _locationsRepository.GetAllLocations();
                if (data != null)
                {
                    locations = (from location in data
                                 select new LocationDTO()
                                 {
                                     Id = location.LOCATIONID,
                                     Title = location.TITLE,
                                     City = location.CITY,
                                     State = location.STATE,
                                     Country = location.COUNTRY,
                                     Zip = location.ZIP
                                 }).ToList();
                }
                return locations;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int CreateLocation(LocationDTO location)
        {
            try
            {
                LOCATION locationRequest = new LOCATION()
                {
                    TITLE = location.Title,
                    CITY = location.City,
                    STATE = location.State,
                    COUNTRY = location.Country,
                    ZIP = location.Zip
                };

                var locationId = _locationsRepository.CreateLocation(locationRequest);

                return locationId;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateLocation(int id, LocationDTO location)
        {
            try
            {
                LOCATION locationRequest = new LOCATION()
                {
                    LOCATIONID = location.Id,
                    TITLE = location.Title,
                    CITY = location.City,
                    STATE = location.State,
                    COUNTRY = location.Country,
                    ZIP = location.Zip
                };

                var success = _locationsRepository.UpdateLocation(id, locationRequest);
                return success;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public LocationDTO GetLocation(int id)
        {
            try
            {
                LocationDTO location = new LocationDTO();
                var data = _locationsRepository.GetLocation(id);
                if (data != null)
                {
                    location = new LocationDTO()
                    {
                        Id = data.LOCATIONID,
                        Title = data.TITLE,
                        City = data.CITY,
                        State = data.STATE,
                        Country = data.COUNTRY,
                        Zip = data.ZIP
                    };
                }
                return location;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
