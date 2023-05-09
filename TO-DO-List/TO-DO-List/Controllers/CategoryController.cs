using DataAccess.Contracts;
using DataStructure.Models;
using Microsoft.AspNetCore.Mvc;
using TO_DO_List.Attributes;
using TO_DO_List.ViewModels;

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
            return View(baseRepository.GetAll<Category>().Where(x => x.User!.ID == HttpContext.Session.GetInt32("UserID")!.Value));
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
        public IActionResult Create(CategoryViewModel categoryViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryViewModel);
            }

            Category category = new Category()
            {
                Name = categoryViewModel.Name,
                User = baseRepository.GetByID<User>(HttpContext.Session.GetInt32("UserID")!.Value)
            };
            baseRepository.Create<Category>(category);

            return RedirectToAction(nameof(Index));
        }

        [Authenticate]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Category category = baseRepository.GetByID<Category>(id);
            return View(new CategoryViewModel()
            {
                ID = category.ID,
                Name = category.Name
            });
        }

        [Authenticate]
        [HttpPost]
        public IActionResult Edit(CategoryViewModel categoryViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryViewModel);
            }
            Category category = baseRepository.GetByID<Category>(categoryViewModel.ID);
            category.Name = categoryViewModel.Name;
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
