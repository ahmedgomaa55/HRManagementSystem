using HRManagementSystem.DataAccessLayer.Models;

namespace HRMangmentSystem.DataAccessLayer.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
