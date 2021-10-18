using System.Collections.Generic;
using EmployeesMicroService.Models;

namespace EmployeesMicroService.Helpers
{
    public interface IUserService
    {
        User Authenticate (string username, string password);
        IEnumerable<User> GetAll ();
    }
}
