using CodeChallenge.Models;
using System.Collections.Generic;
using System.Linq;

namespace CodeChallenge.Helpers.Mappers
{
    public class EmployeeMapper
    {
        public static EmployeeDto MapToDto(Employee employee)
        {
            return new EmployeeDto
            {
                EmployeeId = employee.EmployeeId,
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Position = employee.Position,
                Department = employee.Department,
                DirectReports = employee.DirectReports?.Select(e => e.EmployeeId).ToList()
            };
        }

        public static Employee MapFromDto(EmployeeDto employeeDto, List<Employee> allEmployees)
        {
            var directReports = allEmployees.Where(e => employeeDto.DirectReports.Contains(e.EmployeeId)).ToList();

            return new Employee
            {
                EmployeeId = employeeDto.EmployeeId,
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                Position = employeeDto.Position,
                Department = employeeDto.Department,
                DirectReports = directReports
            };
        }
    }
}
