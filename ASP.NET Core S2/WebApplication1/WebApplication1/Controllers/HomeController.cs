using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //ContentResult contentResult = new ContentResult();
            //contentResult.Content = "Hello From Index";
            //contentResult.ContentType = "text/html";

            //return contentResult;

            return View();
        }

        public IActionResult AboutUs()
        {


            return View();
        }
        public IActionResult Privacy()
        {


            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
    }
}
