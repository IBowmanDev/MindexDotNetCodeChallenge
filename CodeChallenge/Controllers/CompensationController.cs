using CodeChallenge.DTOs;
using CodeChallenge.Filters;
using CodeChallenge.Models;
using CodeChallenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;

namespace CodeChallenge.Controllers
{
    [ApiController]
    [Route("api/compensation")]
    public class CompensationController : ControllerBase
    {
        private readonly ICompensationService _compensationService;
        private readonly IEmployeeService _employeeService;
        private readonly ILogger _logger;

        public CompensationController(ILogger<CompensationController> logger, 
            ICompensationService compensationService,
            IEmployeeService employeeService)
        {
            _compensationService = compensationService;
            _logger = logger;
            _employeeService = employeeService;
        }


        [HttpGet("{id}", Name = "getCompensationByEmployeeId")]
        public IActionResult GetCompensationByEmployeeId(string id)
        {
            if (id == null)
                return NotFound();

            _logger.LogDebug($"Received compensation get request for '{id}'");

            var compensation = _compensationService.GetByEmployeeId(id);

            if (compensation == null)
                return NotFound();

            return Ok(compensation);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Compensation compensation)
        {
            if (compensation == null || String.IsNullOrEmpty(compensation.EmployeeId))
                return NoContent();

            var employee = _employeeService.GetById(compensation.EmployeeId);

            _logger.LogDebug($"Received compensation create request for '{compensation.EmployeeId}'");

            var newCompensation = _compensationService.Create(compensation);

            if (newCompensation == null)
            {
                _logger.LogError($"Compensation creation failed for requestPayload: {JsonConvert.SerializeObject(compensation)}");
                return BadRequest();
            }

            return CreatedAtRoute("getCompensationByEmployeeId", new { id = newCompensation.Employee.EmployeeId }, newCompensation);
        }
    }
}
