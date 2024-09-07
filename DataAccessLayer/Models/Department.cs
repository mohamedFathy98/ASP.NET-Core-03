using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Department
    {
        public int Id { get; set; } //pk

        public int Code { get; set; }

        public string Name { get; set; }
        [Display(Name = "Created At")] 
        public DateTime DateOfCreation { get; set; }
    }
}
