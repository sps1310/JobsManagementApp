using Jobs.DTO;
using Jobs.Manager;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JobsManagementApp.Controllers
{
    public class JobsController : ApiController
    {
        private IJobsManager _jobsManager;

        public JobsController()
        {
            _jobsManager = JobsManager.NewJobsManager;
        }

        // GET: api/v1/Jobs/{id}
        /// <summary>
        /// Method to get a job's details based on job id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HttpResponseMessage Get(int id)
        {
            var job = _jobsManager.GetJobDetails(id);
            if (job != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, job);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "No Job found");
        }

        // GET: api/v1/Jobs/List
        /// <summary>
        /// Method to get a list of jobs based on search text, pageNo, pageSize, locationid/departmentid
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/v1/Jobs/List")]
        public HttpResponseMessage GetJobList([FromBody] GetJobListRequestDTO request)
        {
            var jobList = _jobsManager.GetJobList(request);
            if (jobList != null)
            {
                if (jobList.Total > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, jobList);
                }
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No Jobs found");
        }

        // POST: api/v1/Jobs
        /// <summary>
        /// Method to add/create a new job
        /// </summary>
        /// <param name="job"></param>
        /// <returns></returns>
        public HttpResponseMessage Post([FromBody] JobDTO job)
        {
            int id = _jobsManager.CreateJob(job);
            return Request.CreateResponse(HttpStatusCode.Created, Url.Link("DefaultApi", new { controller = "Jobs", id = id }));
        }

        // PUT: api/v1/Jobs/5
        /// <summary>
        /// Method to update an existing job details
        /// </summary>
        /// <param name="id"></param>
        /// <param name="job"></param>
        /// <returns></returns>
        public HttpResponseMessage Put(int id, [FromBody] JobDTO job)
        {
            bool success = (id > 0 && job != null) ? _jobsManager.UpdateJob(id, job) : false;

            if (success)
            {
                return Request.CreateResponse(HttpStatusCode.OK, "OK");
            }
            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Update failed");
        }
    }
}
