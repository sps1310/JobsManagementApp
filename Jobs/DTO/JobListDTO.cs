using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jobs.DTO
{
    public class JobListDTO
    {
        public int Total { get; set; }
        public List<JobDataDTO> Data { get; set; }
    }
}
