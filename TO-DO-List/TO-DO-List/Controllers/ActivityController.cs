using DataAccess.Contracts;
using DataStructure.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Common;
using TO_DO_List.Attributes;
using TO_DO_List.ViewModels;

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
            return View(baseRepository.GetAll<Activity>().Where(x => x.User.ID == HttpContext.Session.GetInt32("UserID")!.Value));
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
            ActivityViewModel activityViewModel = new ActivityViewModel();
            PopulateActivityViewModel(activityViewModel);

            return View(activityViewModel);
        }

        [Authenticate]
        [HttpPost]
        public IActionResult Create(ActivityViewModel activityViewModel)
        {
            if (!ModelState.IsValid)
            {
                PopulateActivityViewModel(activityViewModel);
                return View(activityViewModel);
            }

            Activity activity = BindActivityViewModelToActivity(activityViewModel);
            baseRepository.Create<Activity>(activity);

            return RedirectToAction(nameof(Index));
        }

        [Authenticate]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ActivityViewModel activityViewModel = new ActivityViewModel();
            PopulateActivityViewModel(activityViewModel);
            BindActivityToActivityViewModel(baseRepository.GetByID<Activity>(id), activityViewModel);
            return View(activityViewModel);
        }

        [Authenticate]
        [HttpPost]
        public IActionResult Edit(ActivityViewModel activityViewModel)
        {
            if (!ModelState.IsValid)
            {
                PopulateActivityViewModel(activityViewModel);
                return View(activityViewModel);
            }

            Activity activity = BindActivityViewModelToActivity(activityViewModel);
            baseRepository.Update<Activity>(activity);

            return RedirectToAction(nameof(Index));
        }

        [Authenticate]
        [HttpPost]
        public IActionResult Delete(int id)
        {
            baseRepository.SoftDelete<Activity>(baseRepository.GetByID<Activity>(id));
            return RedirectToAction(nameof(Index));
        }

        private void PopulateActivityViewModel(ActivityViewModel activityViewModel)
        {
            string[] warningLevelLabels = Enum.GetNames(typeof(DataStructure.Models.WarningLevel));
            List<int> warningLevelValues = Enum.GetValues(typeof(DataStructure.Models.WarningLevel)).Cast<int>().ToList();
            for (int i = 0; i < warningLevelLabels.Length && i < warningLevelValues.Count; i++)
            {
                activityViewModel.WarningLevels.Add(new SelectListItem()
                {
                    Text = warningLevelLabels[i],
                    Value = warningLevelValues[i].ToString()
                });
            }

            List<Category> categories = baseRepository.GetAll<Category>().ToList();
            foreach (Category category in categories)
            {
                activityViewModel.Categories.Add(new SelectListItem()
                {
                    Text = category.Name,
                    Value = category.ID.ToString()
                });
            }
        }

        private Activity BindActivityViewModelToActivity(ActivityViewModel activityViewModel)
        {
            Activity activity = new Activity()
            {
                ID = activityViewModel.ID,
                Name = activityViewModel.Name,
                Description = activityViewModel.Description,
                DueDate = activityViewModel.DueDate,
                WarningLevel = activityViewModel.WarningLevel,
                Category = baseRepository.GetByID<Category>(activityViewModel.CategoryID),
                User = baseRepository.GetByID<User>(HttpContext.Session.GetInt32("UserID")!.Value)
            };

            return activity;
        }

        private void BindActivityToActivityViewModel(Activity activity, ActivityViewModel activityViewModel)
        {
            activityViewModel.ID = activity.ID;
            activityViewModel.Name = activity.Name;
            activityViewModel.Description = activity.Description;
            activityViewModel.DueDate = activity.DueDate;
            activityViewModel.WarningLevel = activity.WarningLevel;
            activityViewModel.CategoryID = activity.Category.ID;
        }
    }
}
