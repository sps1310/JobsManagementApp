using Jobs.DTO;
using Jobs.Manager;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace JobsModuleTest
{
    [TestClass]
    public class JobsTest
    {
        private IJobsManager _jobsManager;

        public JobsTest()
        {
            _jobsManager = JobsManager.NewJobsManager;
        }

        [TestMethod]
        public void GetJobDetails()
        {
            int id = 3;
            var job = _jobsManager.GetJobDetails(id);
            Assert.IsNotNull(job);
        }

        [TestMethod]
        public void CreateJob()
        {
            JobDTO job = new JobDTO()
            {
                Title = "Software Developer",
                Description = "Job Description - Software Developer",
                LocationId = 1,
                DepartmentId = 2,
                ClosingDate = DateTime.Now.AddMonths(5).AddDays(3),
            };
            var id = _jobsManager.CreateJob(job);
            Assert.IsTrue(id != 0);
        }

        [TestMethod]
        public void UpdateJob()
        {
            int id = 2;
            JobDTO job = new JobDTO()
            {
                Description = "Testing Junior Job Description 123",
                LocationId = 2
            };
            var success = _jobsManager.UpdateJob(id, job);
            Assert.IsTrue(success);
        }

        [TestMethod]
        public void GetJobList()
        {
            var request = new GetJobListRequestDTO()
            {
                Q = "test",
                PageNo = 1,
                PageSize = 10,
                LocationId = 0,
                DepartmentId = 0
            };

            var jobs = _jobsManager.GetJobList(request);
            Assert.IsNotNull(jobs);
        }
    }
}
