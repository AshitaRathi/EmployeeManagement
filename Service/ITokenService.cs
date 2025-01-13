using EmployeeManagement.Models;

namespace EmployeeManagement.Service
{
    public interface ITokenService
    {
        public string GenerateToken(Employee employee);
    }
}
