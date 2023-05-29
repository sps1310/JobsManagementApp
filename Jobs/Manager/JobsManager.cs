using Departments.Manager;
using Departments.Repository;
using Jobs.DTO;
using Jobs.Repository;
using JobsDataModel;
using Locations.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Departments.DTO;
using Locations.DTO;
using static System.Net.Mime.MediaTypeNames;
using PagedList;

namespace Jobs.Manager
{
    public interface IJobsManager
    {
        JobDetailsDTO GetJobDetails(int id);
        int CreateJob(JobDTO job);
        bool UpdateJob(int id, JobDTO job);
        JobListDTO GetJobList(GetJobListRequestDTO request);
    }

    public class JobsManager: IJobsManager
    {
        private IJobsRepository _jobsRepository;
        private IDepartmentsRepository _departmentsRepository;
        private ILocationsRepository _locationsRepository;

        public JobsManager(IJobsRepository jobsRepository, IDepartmentsRepository departmentsRepository, ILocationsRepository locationsRepository)
        {
            _jobsRepository = jobsRepository;
            _departmentsRepository = departmentsRepository;
            _locationsRepository = locationsRepository;
        }

        public static IJobsManager NewJobsManager 
        {
            get
            {
                return new JobsManager(JobsRepository.NewJobsRepository, DepartmentsRepository.NewDepartmentsRepository, LocationsRepository.NewLocationsRepository);
            }
        }

        public JobDetailsDTO GetJobDetails(int id)
        {
            try
            {
                JobDetailsDTO job = new JobDetailsDTO();
                var data = _jobsRepository.GetJobDetails(id);

                if (data != null)
                {
                    var department = _departmentsRepository.GetDepartment(data.DEPARTMENTID);
                    var location = _locationsRepository.GetLocation(data.LOCATIONID);

                    if (department != null && location != null) 
                    {
                        job.Id = data.JOBID;
                        job.Code = data.CODE;
                        job.Title = data.TITLE;
                        job.Description = data.DESCRIPTION;
                        job.PostedDate = data.POSTEDDATE;
                        job.ClosingDate = Convert.ToDateTime(data.CLOSINGDATE);

                        job.Department = new DepartmentDTO() 
                        { 
                            Id = department.DEPARTMENTID,
                            Title = department.TITLE,
                        };

                        job.Location = new LocationDTO()
                        {
                            Id = location.LOCATIONID,
                            Title = location.TITLE,
                            City = location.CITY,
                            State = location.STATE,
                            Country = location.COUNTRY,
                            Zip = location.ZIP
                        };
                    }
                }

                return job;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int CreateJob(JobDTO job)
        {
            try
            {
                int jobId = 0;
                if (_locationsRepository.IsLocationExist(job.LocationId) && _departmentsRepository.IsDepartmentExist(job.DepartmentId))
                {
                    JOB jobRequest = new JOB()
                    {
                        TITLE = job.Title,
                        DESCRIPTION = job.Description,
                        LOCATIONID = job.LocationId,
                        DEPARTMENTID = job.DepartmentId,
                        CLOSINGDATE = job.ClosingDate
                    };

                    jobId = _jobsRepository.CreateJob(jobRequest);
                }

                return jobId;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateJob(int id, JobDTO job)
        {
            try
            {
                JOB jobRequest = new JOB()
                {
                    TITLE = job.Title,
                    DESCRIPTION = job.Description,
                    LOCATIONID = _locationsRepository.IsLocationExist(job.LocationId) ? job.LocationId : 0,
                    DEPARTMENTID = _departmentsRepository.IsDepartmentExist(job.DepartmentId) ? job.DepartmentId : 0,
                    CLOSINGDATE = job.ClosingDate
                };

                var success = _jobsRepository.UpdateJob(id, jobRequest);
                return success;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public JobListDTO GetJobList(GetJobListRequestDTO request)
        {
            try
            {
                JobListDTO jobs = new JobListDTO();
                var data = _jobsRepository.GetAllJobs();

                if (data != null)
                {
                    var result = data.Where(job => job.TITLE.ToLower().Contains(request.Q));

                    if (request.DepartmentId != null && request.DepartmentId != 0)
                    {
                        result = result.Where(job => job.DEPARTMENTID == request.DepartmentId);
                    }

                    if (request.LocationId != null && request.LocationId != 0)
                    {
                        result = result.Where(job => job.LOCATIONID == request.LocationId);
                    }

                    result = result.ToPagedList(request.PageNo, request.PageSize);

                    jobs.Total = result.Count();
                    jobs.Data = (from job in result
                                 select new JobDataDTO()
                                 {
                                    Id = job.JOBID,
                                    Code = job.CODE,
                                    Title = job.TITLE,
                                    Description = job.DESCRIPTION,
                                    Location = (_locationsRepository.GetLocation(job.LOCATIONID)).TITLE,
                                    Department = (_departmentsRepository.GetDepartment(job.DEPARTMENTID)).TITLE,
                                    PostedDate = job.POSTEDDATE,
                                    ClosingDate = Convert.ToDateTime(job.CLOSINGDATE)
                                 }).ToList();
                }

                return jobs;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
