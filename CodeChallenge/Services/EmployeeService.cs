using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using CodeChallenge.Repositories;

using CodeChallenge.Data;
using CodeChallenge.Helpers.Mappers;

namespace CodeChallenge.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(ILogger<EmployeeService> logger, IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public EmployeeDto Create(EmployeeDto employeeDto)
        {
            if(employeeDto != null)
            {
                // check for existing employee
                var existingEmployee = _employeeRepository.GetById(employeeDto.EmployeeId);

                // employee already exists, needs design choice on whether to automatically replace existing here
                if (existingEmployee != null) 
                    return null;

                var employee = EmployeeMapper.MapFromDto(employeeDto, _employeeRepository);

                _employeeRepository.Add(employee);
                _employeeRepository.SaveAsync().Wait();

                employeeDto = EmployeeMapper.MapToDto(employee);

                return employeeDto;
            }

            return null;
        }

        public Employee GetById(string id)
        {
            if(!String.IsNullOrEmpty(id))
            {
                return _employeeRepository.GetById(id);
            }

            return null;
        }

        public EmployeeDto GetDtoById(string id)
        {
            if (!String.IsNullOrEmpty(id))
            {
                var employee = _employeeRepository.GetById(id);
                if(employee != null)
                {
                    var employeeDto = EmployeeMapper.MapToDto(employee);
                    return employeeDto;
                }

                // if this is reached, no employee was found with given id
            }

            return null;
        }

        public EmployeeDto Replace(Employee originalEmployee, EmployeeDto newEmployeeDto)
        {
            if(originalEmployee != null)
            {
                _employeeRepository.Remove(originalEmployee);
                if (newEmployeeDto != null)
                {
                    // ensure the original has been removed, otherwise EF will complain another entity w/ same id already exists
                    _employeeRepository.SaveAsync().Wait();

                    var newEmployee = EmployeeMapper.MapFromDto(newEmployeeDto, _employeeRepository);
                    if (newEmployee == null)
                        return null;

                    _employeeRepository.Add(newEmployee);
                    // overwrite the new id with previous employee id
                    newEmployee.EmployeeId = originalEmployee.EmployeeId;
                }
                _employeeRepository.SaveAsync().Wait();
            }

            return newEmployeeDto;
        }

        public ReportingStructure GetReportingStructureById(string id)
        {
            var employee = GetById(id);
            if (employee == null)
            {
                return null;
            }

            var numberOfReports = GetTotalNumberOfReports(employee.EmployeeId);
            return new ReportingStructure
            {
                Employee = employee,
                NumberOfReports = numberOfReports
            };
        }

        private int GetTotalNumberOfReports(string employeeId)
        {
            var employee = GetById(employeeId);

            if (employee?.DirectReports == null) return 0;

            int totalReports = employee.DirectReports.Count;
            foreach (var directReport in employee.DirectReports)
            {
                totalReports += GetTotalNumberOfReports(directReport.EmployeeId);
            }

            return totalReports;
        }
    }
}
