using BusinessLogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Repositories
{
    public class UintOfWork : IUnitOfWork
    {
        private readonly Lazy<IEmployeeReposItory> _employeeRepository;
        private readonly Lazy<IDepartmentRepository> _departmentRepository;
        private readonly DataContext _dataContext;
        public UintOfWork(DataContext dataContext)
        {
            _employeeRepository = new Lazy<IEmployeeReposItory>(() => new EmployeeReposItory(dataContext));
            _departmentRepository = new Lazy<IDepartmentRepository>(() => new DepartmentRepository(dataContext));
            _dataContext = dataContext;
        }

        public IEmployeeReposItory Employees => _employeeRepository.Value;

        public IDepartmentRepository Departments => _departmentRepository.Value;




		public async Task<int> SavaChanagesAsync() => await _dataContext.SaveChangesAsync();

	}
}
