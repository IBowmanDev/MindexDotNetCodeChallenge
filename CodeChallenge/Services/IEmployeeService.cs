using CodeChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Services
{
    public interface IEmployeeService
    {
        Employee GetById(String id);
        EmployeeDto GetDtoById(string id);
        EmployeeDto Create(EmployeeDto employeeDto);
        EmployeeDto Replace(Employee originalEmployee, EmployeeDto   newEmployee);
        ReportingStructure GetReportingStructureByEmployeeId(string id);
    }
}
