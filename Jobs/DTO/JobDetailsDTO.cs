using Departments.DTO;
using Locations.DTO;
using System;

namespace Jobs.DTO
{
    public class JobDetailsDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime PostedDate { get; set; }
        public DateTime ClosingDate { get; set; }
        public DepartmentDTO Department { get; set; }
        public LocationDTO Location { get; set; }
    }
}
