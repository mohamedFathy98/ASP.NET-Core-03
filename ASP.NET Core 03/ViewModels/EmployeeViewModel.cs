
namespace ASP.NET_Core_03.ViewModels
{
    public class EmployeeViewModel
    {

        public int Id { get; set; }
        [StringLength(maximumLength: 30, MinimumLength = 2)]
        public string Name { get; set; }
        [Range(18, 60)]
        public int Age { get; set; }
        public string Address { get; set; }
        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Phone]
        public string Phone { get; set; }
        public bool IsActive { get; set; }
        public IFormFile? Image { get; set; }
        public string? ImageName { get; set; }
        public Department? Department { get; set; }
        public int? DepartmentId { get; set; }
    }
}
