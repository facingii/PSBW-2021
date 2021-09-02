using System;
using System.Linq;
using EFFundamentals.Models;

namespace EFFundamentals
{
    class Program
    {
        static void Main(string[] args)
        {
            // creating context
            // the context may be instanced as any other object in the language
            var context = new employeesContext ();

            // **************************** read *******************************
            // through context we can iterate over dbset (entities) exposed 
            /*foreach (var row in context.Departments)
            {
                Console.WriteLine(row.DeptName);
            }*/

            // **************************** save *******************************
            // to append a new element into a dbset utilize add method passing an object
            // related to dbset inner type
            /*context.Departments.Add (

                new Department
                {
                     DeptNo = "1111",
                     DeptName = "VOLKSWAGEN"
                }

            );*/

            // EF remains all changes in memory and towards persist them in real DB
            // saveChanges method must be called
            //context.SaveChanges ();

            // **************************** search *******************************
            // DBSet expose some methods that permit perform some operations over entities,
            // Find method receive an array of primary keys formatted as object array and returns
            // an entity related with that keys
            //var department = context.Departments.Find (new object [] { "1111" });
            //if (department != null)
            //Console.WriteLine (department.DeptName);

            // **************************** delete *******************************
            // Other recurrent action performed with entities is delete it, to do so
            // DbSet count with Remove method, this method receive as parameter the entity that
            // want be deleted
            //var department = context.Departments.Find (new object [] { "1111" });
            //if (department != null)
            //    context.Departments.Remove (department);

            // Predicate
            /*var result = ValidateName (
                (name) =>
                {
                    if (!name.EsVacio())
                    {
                        return "Name is missing";
                    }

                    return $"Your name is {name}";


                }, "John Snow");

            Console.WriteLine (result);

            Action<string> myAction = (name) => {
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine ($"{name} - {i}");
                }
            };

            myAction ("Daenarys");*/

            // FLOW API

            // SELECT count (emp_no) AS rows FROM employees;
            //var rows = context.Employees.Count ();
            //Console.WriteLine (rows);

            // SELECT * FROM employees WHERE last_name LIKE '%sm%';
            // Empployees.Where
            var employees = context.Employees.Where (emp => emp.LastName.Contains ("sm")).Select (emp => "{emp.FirstName} {emp.LastName}");

            foreach (var e in employees)
            {
                Console.WriteLine (e);
            }

            Console.ReadKey ();

        }

        static string ValidateName (Func<string, string> func, string name)
        {
            return func (name);
        }

    }

    /// <summary>
    /// Method extensions
    /// </summary>
    public static class MyExtensions
    {
        /// <summary>
        /// Devulve verdadero si la cadena no es nula o vacía, falso en caso contrario
        /// </summary>
        /// <param name="source">La cadena de entrada</param>
        /// <returns>Verdadero o Falso</returns>
        public static bool EsVacio (this string source)
        {
            return source != null && source.Length > 0;
        }
    }


}
