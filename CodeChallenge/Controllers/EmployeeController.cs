﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CodeChallenge.Services;
using CodeChallenge.Models;
using CodeChallenge.Helpers.Mappers;
using Newtonsoft.Json;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IEmployeeService _employeeService;

        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeService employeeService)
        {
            _logger = logger;
            _employeeService = employeeService;
        }

        [HttpPost]
        public IActionResult CreateEmployee([FromBody] EmployeeDto employeeDto)
        {
            _logger.LogDebug($"Received employee create request for '{employeeDto.FirstName} {employeeDto.LastName}'");

            _employeeService.Create(employeeDto);

            if(employeeDto == null)
            {
                _logger.LogError($"Employee creation failed for requestPayload: {JsonConvert.SerializeObject(employeeDto)}");
                return BadRequest();
            }

            return CreatedAtRoute("getEmployeeById", new { id = employeeDto.EmployeeId }, employeeDto);
        }

        [HttpGet("{id}", Name = "getEmployeeById")]
        public IActionResult GetEmployeeById(String id)
        {
            _logger.LogDebug($"Received employee get request for '{id}'");

            var employee = _employeeService.GetById(id);

            if (employee == null)
                return NotFound();

            var employeeDto = EmployeeMapper.MapToDto(employee);
            return Ok(employeeDto);
        }

        [HttpPut("{id}")]
        public IActionResult ReplaceEmployee(String id, [FromBody]Employee newEmployee)
        {
            _logger.LogDebug($"Recieved employee update request for '{id}'");

            var existingEmployee = _employeeService.GetById(id);
            if (existingEmployee == null)
                return NotFound();

            _employeeService.Replace(existingEmployee, newEmployee);

            return Ok(newEmployee);
        }

        [HttpGet("report/{id}", Name = "getReportingStructureById")]
        public IActionResult GetReportingStructureById(String id)
        {
            _logger.LogDebug($"Received employee reporting structure request for '{id}'");

            var reportingStructure = _employeeService.GetReportingStructureById(id);

            if (reportingStructure == null)
                return NotFound();

            var reportingStructureDto = ReportingStructureMapper.MapToDto(reportingStructure);
            return Ok(reportingStructureDto);
        }
    }
}
