using DataAccess.Contracts;
using DataStructure.Models;
using Microsoft.AspNetCore.Mvc;
using TO_DO_List.Attributes;

namespace TO_DO_List.Controllers
{
    public class ActivityController : Controller
    {
        private readonly IBaseRepository baseRepository;
        public ActivityController(IBaseRepository baseRepository)
        {
            this.baseRepository = baseRepository;
        }

        [Authenticate]
        [HttpGet]
        public IActionResult Index()
        {
            return View(baseRepository.GetAll<Activity>());
        }

        [Authenticate]
        [HttpGet]
        public IActionResult Details(int id)
        {
            return View(baseRepository.GetByID<Activity>(id));
        }

        [Authenticate]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authenticate]
        [HttpPost]
        public IActionResult Create(Activity activity)
        {
            if (!ModelState.IsValid)
            {
                return View(activity);
            }

            baseRepository.Create<Activity>(activity);

            return RedirectToAction(nameof(Index));
        }

        [Authenticate]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(baseRepository.GetByID<Activity>(id));
        }

        [Authenticate]
        [HttpPost]
        public IActionResult Edit(Activity activity)
        {
            if (!ModelState.IsValid)
            {
                return View(activity);
            }

            baseRepository.Update<Activity>(activity);

            return RedirectToAction(nameof(Index));
        }

        [Authenticate]
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return View(baseRepository.GetByID<Activity>(id.Value));
        }

        [Authenticate]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            baseRepository.SoftDelete<Activity>(baseRepository.GetByID<Activity>(id));
            return RedirectToAction(nameof(Index));
        }
    }
}
