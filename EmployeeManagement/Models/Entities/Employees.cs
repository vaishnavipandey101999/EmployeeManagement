using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Entities
{
    public class Employees
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        public string Department { get; set; }

        public DateTime DateOfJoining { get; set; }


    }
}
