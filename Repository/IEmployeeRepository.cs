using EmployeeManagement.Models;

namespace EmployeeManagement.Repository
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<Employee> GetByEmailAsync(string email);
        Task<Employee> LoginCheckAsync(string email, string password);
    }
}
