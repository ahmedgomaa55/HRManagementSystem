using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HRMangmentSystem.DataAccessLayer.CustomValidators;
using HRMangmentSystem.DataAccessLayer.Models;

namespace HRManagementSystem.DataAccessLayer.Models
{
    public class Employee
    {
        [Required(ErrorMessage = "Name is required.")]
        [MinLength(2, ErrorMessage = "Name must be at least 2 characters.")]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string Phone { get; set; }

        [MaxLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Gender is required.")]
        [RegularExpression("^[MFmf]$", ErrorMessage = "Gender must be 'M' or 'F'.")]
        public char Gender { get; set; }

        [Required(ErrorMessage = "Date of birth is required.")]
        [MinimumAge(21, ErrorMessage = "You must be at least 21 years old.")]
        public DateOnly DateOfBirth { get; set; }

        [MaxLength(50, ErrorMessage = "Nationality cannot exceed 50 characters.")]
        public string Nationality { get; set; }
        [Key]
        [StringLength(14, MinimumLength = 14, ErrorMessage = "National ID must be exactly 14 characters.")]
        public string NationalId { get; set; }

        [Required(ErrorMessage = "Hire date is required.")]
        [MinimumHireDate(ErrorMessage = "Hire date must be on or after January 1, 2008.")]
        public DateOnly HireDate { get; set; }

        [DataType(DataType.Currency)]
        [Range(0, double.MaxValue, ErrorMessage = "Salary must be a positive value.")]
        public double Salary { get; set; }

        [Required(ErrorMessage = "Attendance time is required.")]
        public TimeOnly AttendanceTime { get; set; }

        [Required(ErrorMessage = "Departure time is required.")]
        public TimeOnly DepartureTime { get; set; }

        public bool IsDeleted { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
