using System;
using System.Linq;
using System.Collections;
using EmployeesMicroService.Models;
using EmployeesMicroService.Services;
using Microsoft.Extensions.Logging;

namespace EmployeesMicroService.Bussiness
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ILogger<EmployeeService> _looger;
        private readonly employeesContext _context;

        public EmployeeService (ILogger<EmployeeService> logger, employeesContext context)
        {
            _context = context;
            _looger = logger;
        }

        public IEnumerable GetAll (int index = 0, int take = 50)
        {
            try
            {
                _looger.LogInformation ($"Fetching information for Employee from {index} to {take}");
                return _context.Employees.Skip (index).Take (take).Select (e => new {
                    EmpNo = e.EmpNo,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    Gender = e.Gender,
                    BithDate = e.BirthDate,
                    HireDate = e.HireDate
                });
            }
            catch (Exception ex)
            {
                _looger.LogError (ex, $"An error ocurred in method {nameof (GetAll)}", new { index, take });
                throw;
            }
        }

        public Employee GetEmployee (int empNo)
        {
            try
            {
                _looger.LogInformation ($"Getting information for employee with number {empNo}");
                return _context.Employees.Where (e => e.EmpNo == empNo).FirstOrDefault ();
            }
            catch (Exception ex)
            {
                _looger.LogError (ex, $"An error ocurred in method {nameof (GetEmployee)}", new { empNo });
                throw;
            }
        }

        public bool SaveEmployee (Employee employee)
        {
            try
            {
                _looger.LogInformation ($"Adding new employee to database");
                _context.Employees.Add (employee);
                _context.SaveChanges ();
                return true;
            }
            catch (Exception ex)
            {
                _looger.LogError (ex, $"An error ocurred in method {nameof (SaveEmployee)}", new { employee });
                throw;
            }
        }

        public bool UpdateEmployee (int empNo, Employee employee)
        {
            try
            {
                _looger.LogInformation ($"Update record for the employee number {empNo}");
                var savedEmp = _context.Employees.Where(e => e.EmpNo == empNo).FirstOrDefault ();

                if (savedEmp != null)
                {
                    savedEmp.FirstName = savedEmp.FirstName.Equals (employee.FirstName) ? employee.FirstName : savedEmp.FirstName;
                    savedEmp.LastName = savedEmp.LastName.Equals (employee.LastName) ? employee.LastName : savedEmp.LastName;
                    savedEmp.Gender = employee.Gender;
                    savedEmp.BirthDate = employee.BirthDate;
                    savedEmp.HireDate = employee.HireDate;

                    _context.SaveChanges ();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _looger.LogError (ex, $"An error ocurred in method {nameof (UpdateEmployee)}", new { empNo, employee });
                throw;
             }
        }

        public bool DeleteEmployee(int empNo)
        {
            try
            {
                _looger.LogInformation ($"Delete employee with number {empNo}");
                var savedEmp = _context.Employees.Where(e => e.EmpNo == empNo).FirstOrDefault ();

                if (savedEmp != null)
                {
                    _context.Employees.Remove (savedEmp);
                    _context.SaveChanges ();

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _looger.LogError(ex, $"An error ocurred in method {nameof (DeleteEmployee)}", new { empNo });
                throw;
            }
        }

    }
}
