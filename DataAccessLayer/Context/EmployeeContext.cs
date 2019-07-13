using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Context
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext() : base("EmployeeAuth")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<EmployeeContext>());
        }

        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

        }
    }
}
