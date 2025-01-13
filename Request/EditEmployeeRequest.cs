namespace EmployeeManagement.Request
{
    public class EditEmployeeRequest
    {
        public int Id { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public DateOnly HireDate { get; set; }
        public int TotalExperience { get; set; }
        public int TeamId { get; set; }
        public int JobId { get; set; }
    }
}
