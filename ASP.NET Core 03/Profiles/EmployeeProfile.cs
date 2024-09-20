namespace ASP.NET_Core_03.Profiles
{
    public class EmployeeProfile : Profile
    {

        public EmployeeProfile()

        {
            CreateMap<Employee, EmployeeViewModel>().ReverseMap();
        }
    }
}
