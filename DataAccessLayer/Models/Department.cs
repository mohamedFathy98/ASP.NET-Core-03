using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    internal class Department
    {
        public int Id { get; set; } //pk

        public int Code { get; set; }

        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; }
    }
}
