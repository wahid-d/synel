using System;
using System.ComponentModel.DataAnnotations;

namespace mvc.Models
{
    public class Employee
    {

        [Key]
        public Guid ID { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(10)]
        public string PayrollNumber { get; set; }

        [Required]
        [MaxLength(100)]
        public string Forename { get; set; }

        [Required]
        [MaxLength(100)]
        public string Surname { get; set; }

        [Required]
        public DateTime Birthdate { get; set; }

        [Phone]
        [Required]
        public string Phone { get; set; }

        [Phone]
        [Required]
        public string Mobile { get; set; }

        [Required]
        [MaxLength(255)]
        public string Address { get; set; }

        [MaxLength(255)]
        public string Address2 { get; set; }

        [Required]
        [MaxLength(25)]
        public string Postcode { get; set; }

        [EmailAddress]
        public string HomeEmail { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Obsolete("Used for only model binding.")]
        public Employee() { }

        public Employee(string payrollNumber, string forename, string surname, DateTime birthdate, string phone, string mobile, string address, string address2, string postcode, string homeEmail, DateTime startDate)
        {
            PayrollNumber = payrollNumber;
            Forename = forename;
            Surname = surname;
            Birthdate = birthdate;
            Phone = phone;
            Mobile = mobile;
            Address = address;
            Address2 = address2;
            Postcode = postcode;
            HomeEmail = homeEmail;
            StartDate = startDate;
        }
    }
}