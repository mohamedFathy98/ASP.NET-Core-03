using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_03.Controllers
{
    public class DepartmentController : Controller
    {
        //private IGenaricRepository<Department> _Rep;
        private IDepartmentRepository _Rep;

        public DepartmentController(IDepartmentRepository rep)
        {
            _Rep = rep;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //retrieve All departments
            var department = _Rep.GetAllAsync();

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
            _Rep.AddAsync(department);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int? id)=> await DepartmentControlHandler(id, nameof(Details));
       
        public async Task<IActionResult> Edit(int? id) => await DepartmentControlHandler(id, nameof(Edit));
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
		public async Task<IActionResult> Delete(int? id) => await DepartmentControlHandler(id , nameof(Delete));

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
        public async Task<IActionResult> ConfirmDelete(int? id) 

        {
            if (!id.HasValue) return BadRequest();
            var department = await _Rep.GetAsync(id.Value);
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

        private async Task<IActionResult> DepartmentControlHandler(int? id , string viewName)
        {

            if (!id.HasValue) return BadRequest();
            var department = await _Rep.GetAsync(id.Value);
            if (department is null) return NotFound();
            return View(viewName, department);
        }
    }
}
