using BusinessLogicLayer.Repositories;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_03.Controllers
{
    public class DepartmentController : Controller
    {
        IDepartmentRepository _Rep;

        public DepartmentController(IDepartmentRepository rep)
        {
            _Rep = rep;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //retrieve All departments
            var department = _Rep.GetAll();

            return View(department); //All dep
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Department department)
        {
            //server side validation
            if (!ModelState.IsValid) return View(department);
            _Rep.Create(department);
            return RedirectToAction(nameof(Index));
        }


        public IActionResult Details(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var department = _Rep.Get(id.Value);
            if (department == null) return NotFound();
            return View(department);
        }


    }
}
