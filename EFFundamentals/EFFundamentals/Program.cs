using System;
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
            var department = context.Departments.Find (new object [] { "1111" });
            if (department != null)
                context.Departments.Remove (department);

            Console.ReadKey ();
        }
    }
}
