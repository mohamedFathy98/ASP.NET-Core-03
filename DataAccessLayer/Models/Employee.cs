using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        
        public int Age { get; set; }
        public decimal Salary { get; set; }

        public string Address { get; set; }
        public string Email { get; set; }

        public string Phone { get; set; }
        public bool IsActive { get; set; }
    }
}
