using Microsoft.AspNetCore.Mvc;

namespace TO_DO_List.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
