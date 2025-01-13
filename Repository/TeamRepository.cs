using EmployeeManagement.Data;
using EmployeeManagement.Models;

namespace EmployeeManagement.Repository
{
    public class TeamRepository : Repository<Team>, ITeamRepository
    {
        public TeamRepository(EmployeeManagementDbContext context) : base(context) { }

    }
}
