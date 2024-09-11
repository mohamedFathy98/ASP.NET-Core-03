using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_03.Controllers
{
    public class DepartmentController : Controller
    {
      private IGenaricRepository<Department> _Rep;

        public DepartmentController(IGenaricRepository<Department> rep)
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


        public IActionResult Details(int? id)=> DepartmentControlHandler(id, nameof(Details));
       
        
      
        public IActionResult Edit(int? id) => DepartmentControlHandler(id, nameof(Edit));




        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Department department)
        {
            if (id != department.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    _Rep.Update(department);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(department);
        }
        public IActionResult Delete(int? id) => DepartmentControlHandler(id , nameof(Delete));

        //[HttpPost]
        //public IActionResult Delete([FromRoute] int id, Department department)
        //{
        //    if (id != department.Id) return BadRequest();
        //    if (ModelState.IsValid) 
        //    {
        //        try
        //        {
        //            _Rep.Delete(department);
        //            return RedirectToAction(nameof(Index));
        //        }
        //        catch (Exception ex)
        //        {
        //            ModelState.AddModelError("", ex.Message);
        //        }
        //    }
        //    return View(department);
        //}

        [HttpPost , ActionName("Delete")]
        public IActionResult ConfirmDelete(int? id) 

        {
            if (!id.HasValue) return BadRequest();
            var department = _Rep.Get(id.Value);
            if (department is null) return NotFound();

            try {
                _Rep.Delete(department);
                return RedirectToAction(nameof(Index));
                    }
            catch (Exception ex) 
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            
            }
            return View(department);
        }

        private IActionResult DepartmentControlHandler(int? id , string viewName)
        {

            if (!id.HasValue) return BadRequest();
            var department = _Rep.Get(id.Value);
            if (department is null) return NotFound();
            return View(viewName, department);
        }
    }
}
