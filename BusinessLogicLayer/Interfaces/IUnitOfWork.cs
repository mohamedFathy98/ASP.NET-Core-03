using BusinessLogicLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IUnitOfWork
    {
        public IEmployeeReposItory Employees { get; }
        public IDepartmentRepository Departments { get; }

		public Task<int> SavaChanagesAsync();
	}
}
