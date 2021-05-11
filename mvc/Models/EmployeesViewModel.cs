using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace mvc.Models
{
    public class EmployeesViewModel
    {
        public List<EmployeeDto> Employees { get; set; }

        public bool HasError { get; set; }

        public string Message { get; set; } = string.Empty;

        public int TotalPages { get; set; }

        public int CurrentPage { get; set; }

        public IFormFile EmployeesFile { get; set; }
    }
}
