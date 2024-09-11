using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Repositories
{
    public class EmployeeReposItory : GenaricRepository<Employee>, IEmployeeReposItory
    {
        public EmployeeReposItory(DataContext context) : base(context)
        {

        }
        public IEnumerable<Employee> GetAll(string Address)
        {
            return _dbSet.Where(e => e.Address.ToLower() == Address.ToLower()).ToList();
        }
    }
}