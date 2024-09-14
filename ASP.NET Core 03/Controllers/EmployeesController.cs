using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Repositories;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASP.NET_Core_03.Controllers
{
    public class EmployeesController : Controller
    {


        private readonly IEmployeeReposItory _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        
        public EmployeesController(IEmployeeReposItory employeeRepository, IDepartmentRepository departmentRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
        }
        public IActionResult Index()
        {
            //ViewData => Dictionary<String.Object>
            // ViewData["Message"] = new Employee { Name="Loolz"};
            //C# Feature ViewBag
            ViewBag.Message = "Created Successfuly";

            var employeeviewmodel = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            return View(employeeviewmodel);
        }
        [IgnoreAntiforgeryToken]
        public IActionResult Create()
        {
            var depatrtments = _departmentRepository.GetAll();
            SelectList listItems = new SelectList(depatrtments,"Id","Name");
            ViewBag.Departments = listItems;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            //server side Validation
            if (!ModelState.IsValid) return View(employee);
            _employeeRepository.Create(employee);
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(int? id) => EmployeeContollorModeler(id, nameof(Details));

        public IActionResult Edit(int? id) => EmployeeContollorModeler(id, nameof(Edit));


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, Employee employeeVM)
        {
            if (id != employeeVM.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var employee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                    if (_employeeRepository.Update(employeeVM) > 0)
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
            return View(employeeVM);
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
            if (viewName == nameof(Edit))
            {
                var depatrtments = _departmentRepository.GetAll();
                SelectList listItems = new SelectList(depatrtments, "Id", "Name");
                ViewBag.Departments = listItems;
            }
            if (!id.HasValue) return BadRequest();
            var employee = _employeeRepository.Get(id.Value);
            if (employee is null) return NotFound();
            //var employeeVM = new EmployeeViewModel
            //{
            //    Address = employee.Address,
            //    Department = employee.Department,
            //    Age = employee.Age,
            //    DepartmentId = employee.DepartmentId,
            //    Email = employee.Email,
            //    Id = employee.Id,
            //    IsActive = employee.IsActive,
            //    Name = employee.Name,
            //    Phone = employee.Phone,
            //    Salary = employee.Salary

            //};
            var employeeVM = _mapper.Map<EmployeeViewModel>(employee);

            return View(viewName, employee);
        }
    }
}
