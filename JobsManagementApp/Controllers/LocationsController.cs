using Locations.DTO;
using Locations.Manager;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JobsManagementApp.Controllers
{
    public class LocationsController : ApiController
    {
        private ILocationsManager _locationsManager;

        public LocationsController()
        {
            _locationsManager = LocationsManager.NewLocationsManager;
        }

        // GET: api/v1/Locations
        /// <summary>
        /// Method to fetch all the location records
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage Get()
        {
            var locations = _locationsManager.GetAllLocations();
            if (locations != null)
            {
                if (locations.Any())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, locations);
                }
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Locations found");
        }

        // GET: api/v1/Locations/{id}
        /// <summary>
        /// Method to get a location's details based on id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HttpResponseMessage Get(int id)
        {
            var location = _locationsManager.GetLocation(id);
            if (location != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, location);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Department found");
        }

        // POST: api/v1/Locations
        /// <summary>
        /// Method to add/create new location
        /// </summary>
        /// <param name="location"></param>
        /// <returns></returns>
        public HttpResponseMessage Post([FromBody] LocationDTO location)
        {
            int id = _locationsManager.CreateLocation(location);
            return Request.CreateResponse(HttpStatusCode.Created, Url.Link("DefaultApi", new { controller = "Locations", id = id }));
        }

        // PUT: api/v1/Locations/3
        /// <summary>
        /// Method to update an existing location details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="location"></param>
        /// <returns></returns>
        public HttpResponseMessage Put(int id, [FromBody] LocationDTO location)
        {
            bool success = (id > 0 && location != null) ? _locationsManager.UpdateLocation(id, location) : false;

            if (success)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "OK");
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Update failed");
        }
    }
}
