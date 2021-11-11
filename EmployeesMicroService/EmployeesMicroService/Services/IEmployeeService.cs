using System;
using System.Collections;
using EmployeesMicroService.Models;

namespace EmployeesMicroService.Services
{
    public interface IEmployeeService
    {
        IEnumerable GetAll (int index, int take);
        Employee GetEmployee (string id);
        bool SaveEmployee (Employee employee);
        bool UpdateEmployee (string id, Employee employee);
        bool DeleteEmployee (int empNo);
    }
}
