using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using EmployeesMicroService.Models;
using EmployeesMicroService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EmployeesMicroService.Controllers
{
    [Authorize]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeService _service;
        private readonly ILogger<EmployeesController> _logger;

        public EmployeesController(IEmployeeService service, ILogger<EmployeesController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet ("api/employees")]
        [ProducesResponseType (StatusCodes.Status200OK, Type = typeof (IEnumerable<Employee>))]
        [ProducesResponseType (StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllEmployees ()
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userName = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;

            _logger.LogInformation($"{userName} - Getting Employees list", null);

            try
            {
                var employees = _service.GetAll (0, 100);
                return Ok (employees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{userName} - Error during query to get employees information");
                throw;
            }
        }

        [HttpGet ("api/employees/{EmpNo}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Employee))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetEmployee (int empNo)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userName = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;

            _logger.LogInformation($"{userName} - Calling method GetEmployee with param {empNo}", null);

            try
            {
                var employee = _service.GetEmployee (empNo);
                if (employee != null)
                {
                    return Ok (
                        new
                        {
                            FirstName = employee.FirstName,
                            LastName = employee.LastName,
                            BirthDate = employee.BirthDate,
                            HireDate = employee.HireDate,
                            Gender = employee.Gender
                        }
                    );
                }

                return BadRequest ("Employee Id was not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError (ex, $"{userName} - Error during query to get employee information", empNo);
                throw;
            }
        }

        [Authorize]
        [HttpPost ("api/employees")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult SaveEmployee ([FromBody] Employee employee)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userName = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;

            try
            {
                _logger.LogInformation ($"{userName} - Inserting new employee register");
                var added =_service.SaveEmployee (employee);

                if (added)
                    return Ok ();
                else
                    return BadRequest ();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{userName} - Error during add new Employee {employee.EmpNo} to database");
                throw;
            }
        }

        [HttpPut ("api/employees/{empNo}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult SaveEmployee ([FromBody] Employee employee, int empNo)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userName = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;

            try
            {
                _logger.LogInformation($"{userName} - Updating employee number {empNo}");
                var updated = _service.UpdateEmployee (empNo, employee);

                if (updated)
                    return Ok ();
                else 
                    return BadRequest ("Employee Id not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{userName} - Error during Employee update {employee.EmpNo}");
                throw;
            }
        }

        [Authorize]
        [HttpDelete ("api/employees/{empNo}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteEmployee (int empNo)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userName = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;

            try
            {
                _logger.LogInformation($"{userName} - Deleting employee number {empNo}");
                var deleted = _service.DeleteEmployee (empNo);

                if (deleted)
                    return Ok ();
                else 
                    return BadRequest("Employee Id not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{userName} - Error during delete Employee {empNo}");
                throw;
            }
        }
    }

}
