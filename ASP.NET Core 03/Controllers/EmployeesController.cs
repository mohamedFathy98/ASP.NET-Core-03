using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Repositories;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASP.NET_Core_03.Controllers
{
	[Authorize]
	public class EmployeesController : Controller
    {


        //private readonly IEmployeeReposItory _employeeRepository;
        //private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public EmployeesController(IUnitOfWork unitOfWork, IMapper mapper)
        {

            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }


		//public EmployeesController(IEmployeeReposItory employeeRepository, IDepartmentRepository departmentRepository)
		//{
		//    _employeeRepository = employeeRepository;
		//    _departmentRepository = departmentRepository;
		//}

		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> Index(string? searchValue)
        {
            //ViewData => Dictionary<String.Object>
            // ViewData["Message"] = new Employee { Name="Loolz"};
            //C# Feature ViewBag
            //ViewBag.Message = "Created Successfuly";
            var employees = Enumerable.Empty<Employee>();
            if (string.IsNullOrWhiteSpace(searchValue))
				employees = await _unitOfWork.Employees.GetAllwithDepartmentAsync();
			else employees = await _unitOfWork.Employees.GetAllAsync(searchValue);


			var employeeviewmodel = _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);
            return View(employeeviewmodel);
        }

		public async Task<IActionResult> Create()
		{
			var departments = await _unitOfWork.Departments.GetAllAsync();
			SelectList listItems = new SelectList(departments, "Id", "Name");
			ViewBag.Departments = listItems;
			return View();
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(EmployeeViewModel employeeVM)
		{


			if (!ModelState.IsValid) return View(employeeVM);
			if (employeeVM.Image is not null)
				employeeVM.ImageName = DocumentSetting.UoloadFile(employeeVM.Image, "Image");
			var employee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
			await _unitOfWork.Employees.AddAsync(employee);
			await _unitOfWork.SavaChanagesAsync();
			return RedirectToAction(nameof(Index));
		}
		public async Task<IActionResult> Details(int? id) => await EmployeeContollorModeler(id, nameof(Details));

		public async Task<IActionResult> Edit(int? id) => await EmployeeContollorModeler(id, nameof(Edit));


		[HttpPost]
        [ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit([FromRoute] int id, EmployeeViewModel employeeVM)
		{
			if (id != employeeVM.Id) return BadRequest();
			if (ModelState.IsValid)
			{
				try
				{
					if (employeeVM.Image is not null)
						employeeVM.ImageName = DocumentSetting.UoloadFile(employeeVM.Image, "Image");
					var employee = _mapper.Map<EmployeeViewModel, Employee>(employeeVM);
					_unitOfWork.Employees.Update(employee);
					if (await _unitOfWork.SavaChanagesAsync() > 0)
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
		public async Task<IActionResult> Delete(int? id) => await EmployeeContollorModeler(id, nameof(Delete));


		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ConfirmDelete(int? id)
		{
			if (!id.HasValue) return BadRequest();
			var employee = await _unitOfWork.Employees.GetAsync(id.Value);
			if (employee is null) return NotFound();

			{
				try
				{

					_unitOfWork.Employees.Delete(employee);
					if (await _unitOfWork.SavaChanagesAsync() > 0 && employee.ImageName is not null)
					{
						DocumentSetting.DeleteFile("Image", employee.ImageName);
					}

					return RedirectToAction(nameof(Index));
				}
				catch (Exception ex)
				{
					ModelState.AddModelError("", ex.Message);
				}
			}
			return View(employee);
		}
		private async Task<IActionResult> EmployeeContollorModeler(int? id, string viewName)
		{
			if (viewName == nameof(Edit))
			{
				var departments = await _unitOfWork.Departments.GetAllAsync();
				SelectList listItems = new SelectList(departments, "Id", "Name");
				ViewBag.Departments = listItems;
			}
			if (!id.HasValue) return BadRequest();
			var employee = await _unitOfWork.Employees.GetAsync(id.Value);
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
			return View(viewName, employeeVM);
		}
	}
}
