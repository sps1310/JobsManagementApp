using Departments.DTO;
using Departments.Manager;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DepartmentsModuleTest
{
    [TestClass]
    public class DepartmentsTest
    {
        private IDepartmentsManager _departmentsManager;

        public DepartmentsTest()
        {
            _departmentsManager = DepartmentsManager.NewDepartmentsManager;
        }

        [TestMethod]
        public void GetAllDepartments()
        {
            var departments = _departmentsManager.GetAllDepartments();
            Assert.IsNotNull(departments);
        }

        [TestMethod]
        public void CreateDepartment()
        {
            DepartmentDTO department = new DepartmentDTO()
            {
                Title = "Quality Assurance"
            };
            var id = _departmentsManager.CreateDepartment(department);
            Assert.IsTrue(id != 0);
        }

        [TestMethod]
        public void UpdateDepartment()
        {
            int id = 3;
            DepartmentDTO department = new DepartmentDTO()
            {
                Id = 3,
                Title = "Testing Department"
            };
            var success = _departmentsManager.UpdateDepartment(id, department);
            Assert.IsTrue(success);
        }

        [TestMethod]
        public void GetDepartment()
        {
            var department = _departmentsManager.GetDepartment(3);
            Assert.IsNotNull(department);
        }
    }
}
