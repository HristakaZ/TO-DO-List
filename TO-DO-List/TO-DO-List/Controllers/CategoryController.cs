using DataAccess.Contracts;
using DataStructure.Models;
using Microsoft.AspNetCore.Mvc;
using TO_DO_List.Attributes;

namespace TO_DO_List.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IBaseRepository baseRepository;

        public CategoryController(IBaseRepository baseRepository)
        {
            this.baseRepository = baseRepository;
        }

        [HttpGet]
        [Authenticate]
        public IActionResult Index()
        {
            return View(baseRepository.GetAll<Category>());
        }

        [Authenticate]
        [HttpGet]
        public IActionResult Details(int id)
        {
            return View(baseRepository.GetByID<Category>(id));
        }

        [Authenticate]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authenticate]
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            baseRepository.Create<Category>(category);

            return RedirectToAction(nameof(Index));
        }

        [Authenticate]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            return View(baseRepository.GetByID<Category>(id));
        }

        [Authenticate]
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }

            baseRepository.Update<Category>(category);
            return RedirectToAction(nameof(Index));
        }

        [Authenticate]
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return View(baseRepository.GetByID<Category>(id.Value));
        }

        [Authenticate]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            baseRepository.Delete<Category>(baseRepository.GetByID<Category>(id));
            return RedirectToAction(nameof(Index));
        }
    }
}
