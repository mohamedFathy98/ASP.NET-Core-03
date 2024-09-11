using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
    internal interface IEmployeeReposItory
    {
        int Create(Employee entity);
        int Delete(Employee entity);
        Employee? Get(int id);
        IEnumerable<Employee> GetAll();
        int Update(Employee entity);
    }
}
