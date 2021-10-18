using System;
using System.Collections;
using EmployeesMicroService.Models;

namespace EmployeesMicroService.Services
{
    public interface IEmployeeService
    {
        IEnumerable GetAll (int index, int take);
        Employee GetEmployee (int empNo);
        bool SaveEmployee (Employee employee);
        bool UpdateEmployee (int empNo, Employee employee);
        bool DeleteEmployee (int empNo);
    }
}
