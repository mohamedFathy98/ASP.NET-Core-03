using BusinessLogicLayer.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_03.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepository _Rep;
        public DepartmentController(IDepartmentRepository departmentRepository)
        {
            _Rep = departmentRepository;
        }
        [HttpGet]
        public IActionResult Index()
        {
            //retrieve All departments
            var department = _Rep.GetAll();

            return View(department); //All dep
        }
    }
}
