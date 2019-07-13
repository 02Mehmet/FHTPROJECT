using DataAccessLayer.Context;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace WebAPI.Repository
{
    public class EmployeeRepository
    {
        public readonly EmployeeContext employeeContext = new EmployeeContext();
        public readonly List<Employee> CachedEmployees = new List<Employee>();

        public EmployeeRepository()
        {
            CachedEmployees.AddRange(employeeContext.Employees.ToList());
        }

        public List<Employee> GetEmployees()
        {
            return DatabaseService.AuthenticationAuth.Employees.ToList();
        }

        public bool CreateNewEmployee(Employee emp)
        {
            try
            {
                var employee = new Employee()
                {
                    EmployeeName = emp.EmployeeName,
                    EmployeeType = emp.EmployeeType,
                    MonthlySalary = emp.MonthlySalary,
                    AnnualSalary = emp.AnnualSalary
                };
                employeeContext.Employees.Add(employee);
                employeeContext.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.InnerException);
                return false;
            }
        }

        public bool UpdateEmployee(Employee emp)
        {
            try
            {
                var employee = employeeContext.Employees.SingleOrDefault(b => b.AuthenticationID == emp.AuthenticationID);

                if (employee != null)
                {
                    employeeContext.Employees.Attach(emp);
                    employeeContext.Entry(emp).State = EntityState.Modified;
                    employeeContext.SaveChanges();
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.InnerException);
                return false;
            }
        }
    }
}