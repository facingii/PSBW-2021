using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFFundamentals.Models;

namespace EFFundamentals
{
    class Program
    {
        static void Main (string [] args)
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

            // this example shows how to pass a function as parameter of another function
            //var result = ValidateName (
            //    (name) =>
            //    {
            //        if (!name.EsVacio())
            //        {
            //            return "Name is missing";
            //        }

            //        return $"Your name is {name}";


            //    }, "John Snow");

            //Console.WriteLine (result);

            // Actions otherwise than Functions no returns any value
            //Action<string> myAction = (name) => {
            //    for (int i = 0; i < 10; i++)
            //    {
            //        Console.WriteLine ($"{name} - {i}");
            //    }
            //};

            //myAction ("Daenarys");

            // LINQ exposes several methods that allow us make some operations over the data
            // that operations usually requires pass the function that will be applied to
            // 
            // SELECT count (emp_no) AS rows FROM employees;
            // SQL representation of Count method
            //
            //var rows = context.Employees.Count ();
            //Console.WriteLine (rows);

            // SELECT * FROM employees WHERE last_name LIKE '%sm%';
            // iSQL representation for Where method
            //
            // in this case after filter Employees entity, each result is reduced (transform) into a string
            //var employees = context.Employees.Where(emp => emp.LastName.Contains("sm")).Select(emp => $"{emp.FirstName} {emp.LastName}");

            //foreach (var e in employees)
            //{
            //    Console.WriteLine(e);
            //}

            // in the list of methods available with LINQ exists two of them with the purpose of recover only the first element
            // in the sequence, First and FirstOrDefault, its central difference is that First method returns an
            // exception if there is no data in the resultset and FirstOrDefault returns null if case
            //
            // var employee = context.Employees.Where(emp => emp.EmpNo == -1).First (); // exception is thrown
            //var employee = context.Employees.Where(emp => emp.EmpNo == -1).FirstOrDefault ();

            //if (employee != null) 
            //    Console.WriteLine ($"{employee.EmpNo} {employee.FirstName}");

            // besides procedural option, LINQ permits a more expressivity form of use it
            //
            //var employees2 = from employee
            //                 in context.Employees
            //                 where employee.LastName.Contains ("sm")
            //                 select
            //                     new {
            //                         Key = employee.EmpNo,
            //                         FullName = $"{employee.FirstName} {employee.LastName}"
            //                     };

            //foreach (var e in employees2)
            //{
            //    Console.WriteLine (e.FullName);
            //}

            // as First and FirstOrDefault methods commented previously are available as is, to use them
            // after a LINQ expression it must be closed by parenthesis 
            //var employee = (from e in context.Employees where e.EmpNo == 9999 select e).FirstOrDefault ();

            #region add employess with all this relations

            //context.Employees.Add (
            //    new Employee()
            //    {
            //        EmpNo = 10,
            //        FirstName = "AAA",
            //        LastName = "BBB",
            //        BirthDate = new DateTime(1980, 12, 12),
            //        Gender = "M",
            //        HireDate = DateTime.Now,

            //        Titles = new List<Title>() {
            //            new Title () {
            //                 EmpNo = 10,
            //                 Title1 = "Senior Developer",
            //                 FromDate = DateTime.Now,
            //                 ToDate = DateTime.Now.AddDays (365)
            //            },
            //            new Title () {
            //                 EmpNo = 10,
            //                 Title1 = "Junior Developer",
            //                 FromDate = DateTime.Now,
            //                 ToDate = DateTime.Now.AddDays (365)
            //            }
            //        },

            //        Salaries = new List<Salary>() {
            //            new Salary () {
            //                EmpNo = 10,
            //                FromDate = DateTime.Now,
            //                Salary1  = 1000,
            //                ToDate = DateTime.Now.AddDays (365)
            //            }
            //        },

            //        DeptEmps = new List<DeptEmp>() {
            //            new DeptEmp () {
            //                DeptNo = "1111", //depto.DeptNo,
            //                FromDate = DateTime.Now,
            //                ToDate = DateTime.Now.AddDays (365),
            //                EmpNo = 10
            //            }
            //        },

            //        DeptManagers = new List<DeptManager>() {
            //            new DeptManager () {
            //                DeptNo = "1111", //depto.DeptNo,
            //                EmpNo = 10,
            //                FromDate = DateTime.Now,
            //                ToDate = DateTime.Now.AddDays (365)
            //            }
            //        }
            //    }); ;

            //context.SaveChanges();

            //Console.WriteLine ("Información almacenada");
            //Console.ReadKey();

            #endregion

            #region joins

            // SELECT employees.first_name from employees inner join titles on employees.emp_no = titles.emp_no;

            //var result1 = from e in context.Employees
            //              join t in context.Titles on e.EmpNo equals t.EmpNo
            //              select new
            //              {
            //                  nombreCompleto = e.FirstName + " " + e.LastName,
            //                  titulo = t.Title1,
            //                  contratado = t.FromDate
            //              };

            //foreach (var item in result1)
            //{
            //    Console.WriteLine(item.nombreCompleto);
            //}

            //next example shows the join of three tables two of them are directly related but three one no

            // SELECT employees.first_name FROM employees INNER JOIN
            // (SELECT dept_manager.emp_no FROM dept_manager INNER JOIN departments ON
            // dept_manager.dept_no = departments.dept_no WHERE departments.dept_no = 'd005') deptos ON employees.emp_no = deptos.emp_no

            //var result2 = from e in context.Employees
            //              join x in (from dm in context.DeptManagers
            //                         join d in context.Departments on dm.DeptNo equals d.DeptNo
            //                         where d.DeptNo == "d009"
            //                         select dm) on e.EmpNo equals x.EmpNo
            //              select new
            //              {
            //                  nombreCompleto = $"{e.FirstName} {e.LastName}"
            //              };

            //foreach (var item in result2)
            //{
            //    Console.WriteLine(item.nombreCompleto);
            //}


            // this other shows the join of three related tables
            //var result3 = from e in context.Employees
            //              join dm in context.DeptManagers on e.EmpNo equals dm.EmpNo
            //              join t in context.Titles on e.EmpNo equals t.EmpNo
            //              select new
            //              {
            //                  HiredOn = e.HireDate,
            //                  FullName = e.FirstName + " " + e.LastName,
            //                  Depto = dm.DeptNo,
            //                  t.Title1
            //              };

            //foreach (var item in result3)
            //{
            //    Console.WriteLine("{0} fue contratado en {1} bajo el título {2} para el departamento {3}",
            //        item.FullName, item.HiredOn, item.Title1, item.Depto);
            //}

            //Console.ReadLine ();

            #endregion

            #region trasacctions

            var transaction = context.Database.BeginTransaction();

            try
            {
                var d = new Department();
                d.DeptNo = "4444";
                d.DeptName = "Physics";

                context.Departments.Add(d);

                context.Employees.Add(
                    new Employee
                    {
                        EmpNo = 110,
                        FirstName = "Tyrion",
                        LastName = "Lannister",
                        Gender = "M",
                        BirthDate = DateTime.Now,
                        HireDate = DateTime.Now
                    }
                );

                context.DeptManagers.Add(
                    new DeptManager
                    {
                        DeptNo = "4444",
                        EmpNo = 110,
                        FromDate = DateTime.Now,
                        ToDate = DateTime.Now
                    }
                );


                context.SaveChanges();
                transaction.Commit();
                Console.WriteLine("Información almacenada!");
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine("Error ocurrido.");
                Console.WriteLine(ex.InnerException.Message);
            }


            transaction.Dispose();

            Console.ReadLine();
            #endregion
        }

        static string ValidateName (Func<string, string> func, string name)
        {
            return func (name);
        }

    }

    /// <summary>
    /// Method extensions example
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
