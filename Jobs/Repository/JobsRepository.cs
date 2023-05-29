using Jobs.DTO;
using JobsDataModel;
using System.Collections.Generic;
using System.Linq;
using PagedList;
using System.Net.Http.Headers;

namespace Jobs.Repository
{
    public interface IJobsRepository
    {
        JOB GetJobDetails(int id);
        int CreateJob(JOB job);
        bool UpdateJob(int id, JOB job);
        IEnumerable<JOB> GetAllJobs();
    }

    public class JobsRepository: IJobsRepository
    {
        private JobsDBContext _dbContext;

        public static IJobsRepository NewJobsRepository 
        { 
            get
            {
                return new JobsRepository();
            }
        }

        public JOB GetJobDetails(int id)
        {
            using (_dbContext = new JobsDBContext())
            {
                return _dbContext.GetByID<JOB>(id);
            }
        }

        public int CreateJob(JOB job)
        {
            using (_dbContext = new JobsDBContext())
            {
                _dbContext.Insert<JOB>(job);
                return job.JOBID;
            }
        }

        public bool UpdateJob(int id, JOB job)
        {
            bool success = false;

            if (job != null) 
            {
                using (_dbContext = new JobsDBContext())
                {
                    var jobRecord = _dbContext.GetByID<JOB>(id);
                    if (jobRecord != null)
                    {
                        jobRecord.TITLE = !string.IsNullOrEmpty(job.TITLE) ? job.TITLE : jobRecord.TITLE;
                        jobRecord.DESCRIPTION = !string.IsNullOrEmpty(job.DESCRIPTION) ? job.DESCRIPTION : jobRecord.DESCRIPTION;
                        jobRecord.DEPARTMENTID = (job.DEPARTMENTID != 0) ? job.DEPARTMENTID : jobRecord.DEPARTMENTID;
                        jobRecord.LOCATIONID = (job.LOCATIONID != 0) ? job.LOCATIONID : jobRecord.LOCATIONID;
                        jobRecord.CLOSINGDATE = job.CLOSINGDATE != null ? job.CLOSINGDATE : jobRecord.CLOSINGDATE;

                        _dbContext.Update<JOB>(jobRecord);
                        success = true;
                    } 
                }
            }

            return success;
        }

        public IEnumerable<JOB> GetAllJobs()
        {
            using (_dbContext = new JobsDBContext())
            {
                return _dbContext.GetAll<JOB>();
            }
        }
    }
}
