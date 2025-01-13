namespace EmployeeManagement.Models
{
    public class Job : BaseEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Employee>? Employees { get; set; }
    }
}
