using EmployeeManagement.Models;
using EmployeeManagement.Repository;

namespace EmployeeManagement.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync() =>
            await _employeeRepository.GetAllAsync();

        public async Task<Employee> GetEmployeeByIdAsync(int id) =>
            await _employeeRepository.GetByIdAsync(id);

        public async Task<Employee> GetEmployeeByEmailAsync(string email)
        {
            var employee = await _employeeRepository.GetByEmailAsync(email);

            return employee;
        }

        public async Task AddEmployeeAsync(Employee employee) =>
            await _employeeRepository.AddAsync(employee);

        public async Task UpdateEmployeeAsync(Employee employee) =>
            await _employeeRepository.UpdateAsync(employee);

        public async Task DeleteEmployeeAsync(int id) =>
            await _employeeRepository.DeleteAsync(id);

        public async Task<Employee> LoginCheckAsync(string email, string password)
        {
            return await _employeeRepository.LoginCheckAsync(email, password);
        }
    }
 
}
