using System;
using mvc.Models;

namespace mvc.Utils.Extensions
{
    public static class ToEmployeeDtoExtension
    {
        public static EmployeeDto ToEmployeeDto(this Employee employee)
        {
            return new EmployeeDto(employee);
        }
    }
}
