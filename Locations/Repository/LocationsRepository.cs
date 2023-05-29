using JobsDataModel;
using System.Collections.Generic;

namespace Locations.Repository
{
    public interface ILocationsRepository
    {
        IEnumerable<LOCATION> GetAllLocations();
        int CreateLocation(LOCATION location);
        bool UpdateLocation(int id, LOCATION location);
        LOCATION GetLocation(int id);
        bool IsLocationExist(int id);
    }

    public class LocationsRepository: ILocationsRepository
    {
        private JobsDBContext _dbContext;

        public static ILocationsRepository NewLocationsRepository
        {
            get
            {
                return new LocationsRepository();
            }
        }

        public IEnumerable<LOCATION> GetAllLocations()
        {
            using (_dbContext = new JobsDBContext())
            {
                return _dbContext.GetAll<LOCATION>();
            }
        }

        public int CreateLocation(LOCATION location)
        {
            using (_dbContext = new JobsDBContext())
            {
                _dbContext.Insert<LOCATION>(location);
                return location.LOCATIONID;
            }
        }

        public bool UpdateLocation(int id, LOCATION location)
        {
            bool success = false;

            if (location != null)
            {
                using (_dbContext = new JobsDBContext())
                {
                    var locationRecord = _dbContext.GetByID<LOCATION>(id);
                    if (locationRecord != null)
                    {
                        locationRecord.TITLE = !string.IsNullOrEmpty(location.TITLE) ? location.TITLE : locationRecord.TITLE;
                        locationRecord.STATE = !string.IsNullOrEmpty(location.STATE) ? location.STATE : locationRecord.STATE;
                        locationRecord.CITY = !string.IsNullOrEmpty(location.CITY) ? location.CITY : locationRecord.CITY;
                        locationRecord.COUNTRY = !string.IsNullOrEmpty(location.COUNTRY) ? location.COUNTRY : locationRecord.COUNTRY;
                        locationRecord.ZIP = (location.ZIP != 0) ? location.ZIP : locationRecord.ZIP;
                        
                        _dbContext.Update<LOCATION>(locationRecord);
                        success = true;
                    }
                }
            }

            return success;
        }

        public LOCATION GetLocation(int id)
        {
            using (_dbContext = new JobsDBContext())
            {
                return _dbContext.GetByID<LOCATION>(id);
            }
        }

        public bool IsLocationExist(int id)
        {
            using (_dbContext = new JobsDBContext())
            {
                return _dbContext.Exists<LOCATION>(id);
            }
        }
    }
}
