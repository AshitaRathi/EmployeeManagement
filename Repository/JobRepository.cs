using EmployeeManagement.Data;
using EmployeeManagement.Models;

namespace EmployeeManagement.Repository
{
    public class JobRepository : Repository<Job>, IJobRepository
    {
        public JobRepository(EmployeeManagementDbContext context) : base(context) { }
    }
}
