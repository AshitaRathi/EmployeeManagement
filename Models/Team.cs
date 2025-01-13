namespace EmployeeManagement.Models
{
    public class Team : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Employee>? Employees { get; set; }
    }
}
