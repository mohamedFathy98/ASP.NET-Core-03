using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    public interface IEmployeeReposItory : IGenaricRepository<Employee>
    {
        public IEnumerable<Employee> GetAll(string Address);
    }
}
