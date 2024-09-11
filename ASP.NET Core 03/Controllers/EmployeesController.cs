using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NET_Core_03.Controllers
{
    public class EmployeesController : Controller
    {

        private IEmployeeReposItory _employeeRepository;
        public EmployeesController(IEmployeeReposItory employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public IActionResult Index()
        {
            //ViewData => Dictionary<String.Object>
            // ViewData["Message"] = new Employee { Name="Loolz"};
            //C# Feature ViewBag
            //ViewBag.Message = "Created Successfuly";

            var employees = _employeeRepository.GetAll();
            return View(employees);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            if (!ModelState.IsValid) return View(employee);
            _employeeRepository.Create(employee);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int? id) => EmployeeContollorModeler(id, nameof(Details));

        public IActionResult Edit(int? id) => EmployeeContollorModeler(id, nameof(Edit));


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Employee employee)
        {
            if (id != employee.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    if (_employeeRepository.Update(employee) > 0)
                    {
                        TempData["Message"] = "Employee Updated Successfuly";
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(employee);
        }
        public IActionResult Delete(int? id) => EmployeeContollorModeler(id, nameof(Delete));


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmDelete(int? id)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _employeeRepository.Get(id.Value);
            if (employee is null) return NotFound();

            {
                try
                {
                    _employeeRepository.Delete(employee);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                }
            }
            return View(employee);
        }
        private IActionResult EmployeeContollorModeler(int? id, string viewName)
        {
            if (!id.HasValue) return BadRequest();
            var employee = _employeeRepository.Get(id.Value);
            if (employee is null) return NotFound();
            return View(viewName, employee);
        }
    }
}
