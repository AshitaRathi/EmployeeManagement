using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Models
{
    public class Employee : BaseEntity
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
        [Required]
        public int TeamId { get; set; }
        [ForeignKey("TeamId")]
        public Team? Team { get; set; }
        [Required]
        public int JobId { get; set; }
        [ForeignKey("JobId")]
        public Job? Job { get; set; }
    }
}