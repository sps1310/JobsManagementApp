using Departments.DTO;
using Departments.Repository;
using JobsDataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Departments.Manager
{
    public interface IDepartmentsManager
    {
        List<DepartmentDTO> GetAllDepartments();
        int CreateDepartment(DepartmentDTO department);
        bool UpdateDepartment(int id, DepartmentDTO department);
        DepartmentDTO GetDepartment(int id);
    }

    public class DepartmentsManager: IDepartmentsManager
    {
        private IDepartmentsRepository _departmentsRepository;

        public DepartmentsManager(IDepartmentsRepository departmentsRepository)
        {
            _departmentsRepository = departmentsRepository;
        }

        public static IDepartmentsManager NewDepartmentsManager 
        {
            get
            {
                return new DepartmentsManager(DepartmentsRepository.NewDepartmentsRepository);
            }
        }

        public List<DepartmentDTO> GetAllDepartments()
        {
            try
            {
                List<DepartmentDTO> departments = new List<DepartmentDTO>();
                var data = _departmentsRepository.GetAllDepartments();
                if (data != null)
                {
                    departments = (from dept in data
                                   select new DepartmentDTO()
                                   {
                                       Id = dept.DEPARTMENTID,
                                       Title = dept.TITLE,
                                   }).ToList();
                }
                return departments;
            }
            catch(Exception)
            {
                throw;
            }
        }

        public int CreateDepartment(DepartmentDTO department)
        {
            try
            {
                DEPARTMENT departmentRequest = new DEPARTMENT()
                {
                    TITLE = department.Title
                };

                var departmentId = _departmentsRepository.CreateDepartment(departmentRequest);

                return departmentId;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateDepartment(int id, DepartmentDTO department)
        {
            try
            {
                DEPARTMENT departmentRequest = new DEPARTMENT()
                {
                    DEPARTMENTID = department.Id,
                    TITLE = department.Title
                };

                var success = _departmentsRepository.UpdateDepartment(id, departmentRequest);
                return success;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DepartmentDTO GetDepartment(int id)
        {
            try
            {
                DepartmentDTO department = new DepartmentDTO();
                var data = _departmentsRepository.GetDepartment(id);
                if (data != null)
                {
                    department = new DepartmentDTO()
                    {
                        Id = data.DEPARTMENTID,
                        Title = data.TITLE,
                    };
                }
                return department;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
