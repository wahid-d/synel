using System;

namespace mvc.Models
{
    public class EmployeeDto
    {
        public string PayrollNumber { get; set; }

        public string Forename { get; set; }

        public string Surname { get; set; }

        public DateTime Birthdate { get; set; }

        public string Phone { get; set; }

        public string Mobile { get; set; }

        public string Address { get; set; }

        public string Address2 { get; set; }

        public string Postcode { get; set; }

        public string HomeEmail { get; set; }

        public DateTime StartDate { get; set; }

        [Obsolete("Used for only model binding")]
        public EmployeeDto() { }

        public EmployeeDto(Employee employee)
        {
            PayrollNumber = employee.PayrollNumber;
            Forename = employee.Forename;
            Surname = employee.Surname;
            Birthdate = employee.Birthdate;
            Phone = employee.Phone;
            Mobile = employee.Mobile;
            Address = employee.Address;
            Address2 = employee.Address2;
            Postcode = employee.Postcode;
            HomeEmail = employee.HomeEmail;
            StartDate = employee.StartDate;
        }

        public EmployeeDto(string payrollNumber, string forename, string surname, DateTime birthdate, string phone, string mobile, string address, string address2, string postcode, string homeEmail, DateTime startDate)
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
