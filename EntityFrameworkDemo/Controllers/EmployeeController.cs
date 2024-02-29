using EntityFrameworkDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EntityFrameworkDemo.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            // Create an instance of the Entity Framework context
            EntityFrameworkDemoEntities entities = new EntityFrameworkDemoEntities();

            // Get the list of employees from the database
            List<Employee> employees = entities.Employees.ToList();

            // Pass the list of employees to the view
            return View(employees);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            // Create an instance of the Entity Framework context
            EntityFrameworkDemoEntities entities = new EntityFrameworkDemoEntities();

            // Add the new employee to the Employees table
            entities.Employees.Add(employee);

            // Save the changes to the database
            entities.SaveChanges();

            // Redirect to the Index action
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int employeeId)
        {
            // Create an instance of the Entity Framework context
            EntityFrameworkDemoEntities entities = new EntityFrameworkDemoEntities();

            // Get the employee with the specified ID
            var empData = entities.Employees.Where(e => e.EmployeeId == employeeId).FirstOrDefault();

            // Pass the employee to the view
            return View(empData);
        }

        [HttpPost]
        public ActionResult Edit(Employee employee)
        {
            // Create an instance of the Entity Framework context
            EntityFrameworkDemoEntities entities = new EntityFrameworkDemoEntities();

            // Get the employee with the specified ID
            Employee employeeToUpdate = entities.Employees.Where(e => e.EmployeeId == employee.EmployeeId).FirstOrDefault();

            // Update the employee's properties
            employeeToUpdate.EmpName = employee.EmpName;
            employeeToUpdate.EmpEmail = employee.EmpEmail;
            employeeToUpdate.EmpGender = employee.EmpGender;
            employeeToUpdate.EmpSalary = employee.EmpSalary;

            // Save the changes to the database
            entities.SaveChanges();

            // Redirect to the Index action
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int employeeId)
        {
            // Create an instance of the Entity Framework context
            EntityFrameworkDemoEntities entities = new EntityFrameworkDemoEntities();

            // Get the employee with the specified ID
            Employee employeeToDelete = entities.Employees.Where(e => e.EmployeeId == employeeId).FirstOrDefault();

            if (employeeToDelete != null)
            {
                // Remove the employee from the Employees table
                entities.Employees.Remove(employeeToDelete);

                // Save the changes to the database
                entities.SaveChanges();

                return View(employeeToDelete);
            }
            else
            {
                return HttpNotFound();
            }
        }
    }
}