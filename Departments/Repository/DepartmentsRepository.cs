using JobsDataModel;
using System.Collections.Generic;

namespace Departments.Repository
{
    public interface IDepartmentsRepository
    {
        IEnumerable<DEPARTMENT> GetAllDepartments();
        int CreateDepartment(DEPARTMENT department);
        bool UpdateDepartment(int id, DEPARTMENT department);
        DEPARTMENT GetDepartment(int id);
        bool IsDepartmentExist(int id);
    }

    public class DepartmentsRepository: IDepartmentsRepository
    {
        private JobsDBContext _dbContext;

        public static IDepartmentsRepository NewDepartmentsRepository 
        { 
            get
            {
                return new DepartmentsRepository();
            }
        }

        public IEnumerable<DEPARTMENT> GetAllDepartments()
        {
            using(_dbContext = new JobsDBContext())
            {
                return _dbContext.GetAll<DEPARTMENT>();
            }
        }

        public int CreateDepartment(DEPARTMENT department)
        {
            using (_dbContext = new JobsDBContext())
            {
                _dbContext.Insert<DEPARTMENT>(department);
                return department.DEPARTMENTID;
            }
        }

        public bool UpdateDepartment(int id, DEPARTMENT department)
        {
            bool success = false;

            if (department != null) 
            {
                using (_dbContext = new JobsDBContext())
                {
                    var deptRecord = _dbContext.GetByID<DEPARTMENT>(id);
                    if (deptRecord != null)
                    {
                        deptRecord.TITLE = !string.IsNullOrEmpty(department.TITLE) ? department.TITLE : deptRecord.TITLE;
                        _dbContext.Update<DEPARTMENT>(deptRecord);
                        success = true;
                    } 
                }
            }

            return success;
        }

        public DEPARTMENT GetDepartment(int id)
        {
            using (_dbContext = new JobsDBContext())
            {
                return _dbContext.GetByID<DEPARTMENT>(id);
            }
        }

        public bool IsDepartmentExist(int id)
        {
            using (_dbContext = new JobsDBContext())
            {
                return _dbContext.Exists<DEPARTMENT>(id);
            }
        }
    }
}
