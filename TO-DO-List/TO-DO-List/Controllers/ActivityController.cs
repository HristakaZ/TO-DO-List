using Microsoft.AspNetCore.Mvc;

namespace TO_DO_List.Controllers
{
    public class ActivityController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
