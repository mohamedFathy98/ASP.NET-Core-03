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
		public async Task<IEnumerable<Employee>> GetAllAsync(string name)
	  => await _dbSet.Where(e => e.Name.ToLower().Contains(name.ToLower())).Include(e => e.Department).ToListAsync();


		public async Task<IEnumerable<Employee>> GetAllwithDepartmentAsync()
		=> await _dbSet.Include(e => e.Department).ToListAsync();
	}
}