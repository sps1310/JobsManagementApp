using Locations.DTO;
using Locations.Manager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LocationsModuleTest
{
    [TestClass]
    public class LocationsTest
    {
        private ILocationsManager _locationsManager;

        public LocationsTest()
        {
            _locationsManager = LocationsManager.NewLocationsManager;
        }

        [TestMethod]
        public void GetAllLocations()
        {
            var locations = _locationsManager.GetAllLocations();
            Assert.IsNotNull(locations);
        }

        [TestMethod]
        public void CreateLocation()
        {
            LocationDTO location = new LocationDTO()
            {
                Title = "India Office",
                City = "Pune",
                State = "Maharashtra",
                Country = "India",
                Zip = 82637
            };
            var id = _locationsManager.CreateLocation(location);
            Assert.IsTrue(id != 0);
        }

        [TestMethod]
        public void UpdateLocation()
        {
            int id = 3;
            LocationDTO location = new LocationDTO()
            {
                Id = 3,
                Title = "Testing Office",
            };
            var success = _locationsManager.UpdateLocation(id, location);
            Assert.IsTrue(success);
        }

        [TestMethod]
        public void GetLocation()
        {
            var location = _locationsManager.GetLocation(3);
            Assert.IsNotNull(location);
        }
    }
}
