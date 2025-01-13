using EmployeeManagement.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagement.Request
{
    public class EditJobRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
