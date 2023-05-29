using Departments.DTO;
using Departments.Manager;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JobsManagementApp.Controllers
{
    public class DepartmentsController : ApiController
    {
        private IDepartmentsManager _departmentsManager;

        public DepartmentsController()
        {
            _departmentsManager = DepartmentsManager.NewDepartmentsManager;
        }

        // GET: api/v1/Departments
        /// <summary>
        /// Method to fetch all the department records
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage Get()
        {
            var departments = _departmentsManager.GetAllDepartments();
            if (departments != null)
            {
                if (departments.Any())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, departments);
                }
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Departments found");
        }

        // GET: api/v1/Departments/{id}
        /// <summary>
        /// Method to get a department's details based on id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HttpResponseMessage Get(int id)
        {
            var department = _departmentsManager.GetDepartment(id);
            if (department != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, department);
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Department found");
        }

        // POST: api/v1/Departments
        /// <summary>
        /// Method to add/create new department
        /// </summary>
        /// <param name="department"></param>
        /// <returns></returns>
        public HttpResponseMessage Post([FromBody] DepartmentDTO department)
        {
            int id = _departmentsManager.CreateDepartment(department);
            return Request.CreateResponse(HttpStatusCode.Created, Url.Link("DefaultApi", new { controller = "Departments", id = id }));
        }

        // PUT: api/v1/Departments/3
        /// <summary>
        /// Method to update an existing department details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="department"></param>
        /// <returns></returns>
        public HttpResponseMessage Put(int id, [FromBody] DepartmentDTO department)
        {
            bool success = (id > 0 && department != null) ? _departmentsManager.UpdateDepartment(id, department) : false;

            if (success)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "OK");
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Update failed");
        }
    }
}
