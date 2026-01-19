using BulkyWeb.Data;
using BulkyWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public CategoryController(ApplicationDbContext dbContext)
        {
          _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            // Retrieve all categories from the database
            List<Category> categories = _dbContext.Categories.ToList();

            return View(categories);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category category)
        {

            // Server-side validation
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The Display Order cannot exactly match the Name.");
            }

            //Validate if exists in database display order
            var existingCategory = _dbContext.Categories
                .FirstOrDefault(c => c.DisplayOrder == category.DisplayOrder);
            if (existingCategory != null)
            {
                ModelState.AddModelError("DisplayOrder", "A category with this Display Order already exists.");
            }

            if (ModelState.IsValid)
            {
                _dbContext.Categories.Add(category);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }
    }
}
