using EmployeeManagement.Data;
using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Repository
{
    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private readonly EmployeeManagementDbContext _context;

        public EmployeeRepository(EmployeeManagementDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<Employee> GetByEmailAsync(string email)
        {
            return await _context.Employees.FirstOrDefaultAsync(e => e.Email == email);
        }

        public async Task<Employee> LoginCheckAsync(string email, string password)
        {
            return await _context.Employees.FirstOrDefaultAsync(e => e.Email == email && e.Password == password);
        }
    }
}
