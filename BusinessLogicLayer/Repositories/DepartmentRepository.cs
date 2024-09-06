using DataAccessLayer.Data;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Repositories
{
    internal class DepartmentRepository
    {
         /**
          * Get , Get All , Create , Update , Delete
         */
        // private DataContext dataContext = new DataContext(); //hard coded Dependancy 
        //Dependancy Injection
        //Method Injection => Method([fromServices]DataContext dataContext}
        //proberty injection =>
        //[fromServices]


        private readonly DataContext _dataContext;

        //Ctor Injection
        public DepartmentRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }




        //public Department Get(int id)
        //{
        //    _dataContext.Departments.FirstOrDefault(id);
        //}

    }
}
