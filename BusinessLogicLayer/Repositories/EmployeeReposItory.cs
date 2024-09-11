using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Repositories
{
    internal class EmployeeReposItory : IEmployeeReposItory
    {
        private DataContext _context;

        public EmployeeReposItory(DataContext context)
        {
            _context = context;
        }

        public int Create(Employee entity)
        {
            _context.Employees.Add(entity);
            return _context.SaveChanges();
        }

        public int Delete(Employee entity)
        {
            _context.Employees.Remove(entity);
            return _context.SaveChanges();

        }

        public Employee? Get(int id)=> _context.Employees.Find(id);
        

        public IEnumerable<Employee> GetAll()=> _context.Employees.ToList();
       

        public int Update(Employee entity)
        {
            _context.Employees.Update(entity);
            return _context.SaveChanges();

        }
    }
}

 
